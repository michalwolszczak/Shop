using Dapper;
using Products.Entities;
using Products.Interfaces;

namespace Products.Repositories
{
    public class PriceRepository : IPriceRepository<Price>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PriceRepository(IDbConnectionFactory connectionFactory)
        {
            _dbConnectionFactory = connectionFactory;
        }

        public async Task SaveAsync(IEnumerable<Price> entities)
        {
            var sql = @"
            INSERT INTO Prices (
                Id, 
                Sku, 
                NettPriceAfterDiscountPerUnit)
            VALUES (
                @Id, 
                @Sku, 
                @NettPriceAfterDiscountPerUnit);";

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
