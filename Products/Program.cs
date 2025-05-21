using Products.CsvReader;
using Products.Entities;
using Products.Factory;
using Products.Interfaces;
using Products.Middlewares;
using Products.Repositories;
using Products.Services;
using Products.Utils;
using SQLitePCL;

namespace Products
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            

            //utils
            builder.Services.AddTransient<ExceptionLoggerMiddleware>();
            builder.Services.AddTransient<IProblemDetailsFactory, ProblemDetailsFactory>();

            //httpClient
            builder.Services.AddHttpClient();


            builder.Services.AddTransient<IFileDownloader, HttpFileDownloader>();
            builder.Services.AddTransient<IProductService, ProductService>();

            //readers
            builder.Services.AddTransient<ICsvReader<Product>, ProductCsvReader>();
            builder.Services.AddTransient<ICsvReader<Inventory>, InventoryCsvReader>();
            builder.Services.AddTransient<ICsvReader<Price>, PriceCsvReader>();


            // Filters
            builder.Services.AddTransient<IFilterFactory<Product>, ProductCableAndDeliveryFilterFactory>();
            builder.Services.AddTransient<IFilterFactory<Inventory>, InventoryConnectedToProductAndDeliveryFilterFactory>();
            builder.Services.AddTransient<IFilterFactory<Price>, PriceConnectedToInventoryFilterFactory>();
            
            //repositories
            builder.Services.AddTransient<IProductRepository<Product>, ProductRepository>();
            builder.Services.AddTransient<IRepository<Product>>(sp => sp.GetRequiredService<IProductRepository<Product>>());

            builder.Services.AddTransient<IInventoryRepository<Inventory>, InventoryRepository>();
            builder.Services.AddTransient<IRepository<Inventory>>(sp => sp.GetRequiredService<IInventoryRepository<Inventory>>());

            builder.Services.AddTransient<IRepository<Price>, PriceRepository>();

            //import services
            builder.Services.AddTransient<IImportService<Product>, ImportService<Product>>();
            builder.Services.AddTransient<IImportService<Inventory>, ImportService<Inventory>>();
            builder.Services.AddTransient<IImportService<Price>, ImportService<Price>>();

            //database init 
            builder.Services.AddSingleton<DatabaseInitializer>();
            builder.Services.AddSingleton<IDbConnectionFactory>(opt =>
            {
                var connectionString = builder.Configuration.GetConnectionString("SqliteConnection")
                    ?? throw new ArgumentNullException("SqliteConnection");

                return new SqliteConnectionFactory(connectionString);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionLoggerMiddleware>();

            //database init 
            using (var scope = app.Services.CreateScope())
            {
                Batteries.Init(); //for SQLite
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                await dbInitializer.EnsureDatabaseCreatedAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}