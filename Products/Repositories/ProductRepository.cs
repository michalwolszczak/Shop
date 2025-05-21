using Products.Interfaces;
using Dapper;
using Products.Entities;
using Products.Dto;

namespace Products.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<HashSet<int>> GetAllIds()
        {
            var sql = "SELECT Id FROM Products;";

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync<int>(sql);

            return result.ToHashSet();
        }

        public async Task<ProductDto?> GetProductBySku(string sku)
        {
            var sql = @$"
                SELECT 
                    p.Name,
                    p.Ean AS EAN,
                    p.ProducerName,
                    p.Category,
                    p.DefaultImage AS ImageUrl,
                    i.Quantity,
                    i.Unit,
                    pr.NettPrice,
                    i.ShippingCost
                FROM Products p
                JOIN Inventory i ON i.ProductId = p.Id
                LEFT JOIN Prices pr ON pr.Sku = i.Sku
                WHERE p.Sku = '{sku}';";

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync<ProductDto>(sql);

            return result.FirstOrDefault();
        }

        public async Task SaveAsync(IEnumerable<Product> entities)
        {
            string sql = @"
            INSERT INTO Products (
                Id, Sku, Name, Ean, ProducerName, Category,
                IsWire, Shipping, Available, IsVendor, DefaultImage)
            VALUES (
                @Id, @Sku, @Name, @Ean, @ProducerName, @Category,
                @IsWire, @Shipping, @Available, @IsVendor, @DefaultImage);";

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var entity in entities)
                {
                    await connection.ExecuteAsync(sql, entity, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}