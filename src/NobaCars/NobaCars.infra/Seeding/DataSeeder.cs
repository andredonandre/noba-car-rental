using NobaCars.Core.Entities;
using NobaCars.Core.Interfaces.Repositories;

namespace NobaCars.Infra.Seeding
{
    // Seeds default data through the repository interfaces, so it works for any storage implementation.
    internal sealed class DataSeeder(ICarCategoryRepository categories, ICarRepository cars)
    {
        public async Task SeedAsync()
        {
            await SeedCarCategoriesAsync();
            await SeedCarsAsync();
        }

        private async Task SeedCarCategoriesAsync()
        {
            if (categories.GetAll().Any()) return;
            var smallCar = new CarCategory() { Name = "SmallCar", BaseDayRentalPrice = 300, BaseKmPrice = 0, DayRateMultipler = 1, DistanceRateMultipler = 0 };
            var combi = new CarCategory() { Name = "Combi", BaseDayRentalPrice = 400, BaseKmPrice = 2, DayRateMultipler = 1.3, DistanceRateMultipler = 1 };
            var truck = new CarCategory() { Name = "Truck", BaseDayRentalPrice = 800, BaseKmPrice = 5, DayRateMultipler = 1.5, DistanceRateMultipler = 1.5 };
            var suv = new CarCategory() { Name = "SUV", BaseDayRentalPrice = 600, BaseKmPrice = 3, DayRateMultipler = 1.3, DistanceRateMultipler = 1.2 };

            await categories.AddRangeAsync([smallCar, combi, truck, suv]);
        }

        private async Task SeedCarsAsync()
        {
            if (cars.GetAll().Any()) return;
            // Read the categories back from the store so the cars get the stored ids
            var storedCategories = categories.GetAll().ToList();
            CarCategory? Category(string name) => storedCategories.FirstOrDefault(c => c.Name == name);

            var seedCars = new List<Car>
            {
                new() { Brand = "Volkswagen", Model = "Polo", Year = 2023, RegistrationNumber = "ABC123", VIN = "WVWZZZAWZPU000001", MileAge = 12_500, CarCategory = Category("SmallCar")! },
                new() { Brand = "Volvo", Model = "V60", Year = 2022, RegistrationNumber = "DEF456", VIN = "YV1ZW25V0N1000002", MileAge = 34_200, CarCategory = Category("Combi")! },
                new() { Brand = "Scania", Model = "P280", Year = 2021, RegistrationNumber = "GHI789", VIN = "YS2P4X20005000003", MileAge = 88_900, CarCategory = Category("Truck")! }
            };

            // Skip any car whose category has been removed from the store
            await cars.AddRangeAsync(seedCars.Where(c => c.CarCategory != null));
        }
    }
}
