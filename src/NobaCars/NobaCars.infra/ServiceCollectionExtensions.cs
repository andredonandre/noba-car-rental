using Microsoft.Extensions.DependencyInjection;
using NobaCars.Core.Interfaces.Repositories;
using NobaCars.Infra.Repositories;
using NobaCars.Infra.Seeding;

namespace NobaCars.Infra
{
    public static class ServiceCollectionExtensions
    {
        // Registers the JSON flat-file implementation of the Core repository interfaces.
        // A different storage provider only needs its own extension method like this one.
        public static IServiceCollection AddDataStore(this IServiceCollection services, string filePath = "database.json")
        {
            services.AddSingleton(_ => new Database(filePath));
            services.AddSingleton<IBookingRepository, JsonBookingRepository>();
            services.AddSingleton<ICarRepository, JsonCarRepository>();
            services.AddSingleton<ICarCategoryRepository, JsonCarCategoryRepository>();
            services.AddSingleton<DataSeeder>();
            return services;
        }

        public static Task SeedDataAsync(this IServiceProvider services) =>
            services.GetRequiredService<DataSeeder>().SeedAsync();
    }
}
