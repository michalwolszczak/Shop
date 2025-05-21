using Dapper;
using Products.Entities;
using Products.Interfaces;

namespace Products.Repositories
{
    public class InventoryRepository : IInventoryRepository<Inventory>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public InventoryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<HashSet<string>> GetAllSKUs()
        {
            var sql = "SELECT Sku FROM Inventory;";

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync<string>(sql);

            return result.ToHashSet();
        }

        public async Task SaveAsync(IEnumerable<Inventory> entities)
        {
            string sql = @"
            INSERT INTO Inventory (
                ProductId,
                Sku,
                Unit,
                Quantity,
                Shipping,
                ShippingCost
            ) VALUES (
                @ProductId,
                @Sku,
                @Unit,
                @Quantity,
                @Shipping,
                @ShippingCost
            );";

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
