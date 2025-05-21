using Dapper;
using Products.Interfaces;

namespace Products.Utils
{
    public class DatabaseInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DatabaseInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task EnsureDatabaseCreatedAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var sql =
            @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY,
                    Sku TEXT NOT NULL UNIQUE,
                    Name TEXT NOT NULL,
                    Ean TEXT,
                    ProducerName TEXT,
                    Category TEXT,
                    IsWire INTEGER NOT NULL,
                    Shipping TEXT,
                    Available INTEGER NOT NULL,
                    IsVendor INTEGER NOT NULL,
                    DefaultImage TEXT
                );

                CREATE TABLE IF NOT EXISTS Inventory (
                    ProductId INTEGER NOT NULL,
                    Sku TEXT NOT NULL PRIMARY KEY,
                    Unit TEXT,
                    Quantity INTEGER,
                    Manufacturer TEXT,
                    Shipping TEXT,
                    ShippingCost REAL,
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                );

                CREATE TABLE IF NOT EXISTS Prices (
                    Id TEXT PRIMARY KEY,
                    Sku TEXT NOT NULL,
                    NettPrice REAL,
                    NettPriceAfterDiscount REAL,
                    VatRate REAL,
                    NettPriceAfterDiscountPerUnit REAL,
                    FOREIGN KEY (Sku) REFERENCES Inventory(Sku)
                );
            ";

            await connection.ExecuteAsync(sql);
        }
    }
}