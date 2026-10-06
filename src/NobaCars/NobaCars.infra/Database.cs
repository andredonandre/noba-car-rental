using JsonFlatFileDataStore;
using NobaCars.Infra.Entities;

namespace NobaCars.Infra
{
    public class Database
    {
        public DataStore store;
        public Database() {
            store = new DataStore("database.json");
            SeedData();
        }       
        private void SeedData() {
            SeedInventory();
        }
        private void SeedInventory() {
            SeedCarCategories();
            SeedCars();
        }
        private void SeedCarCategories() {
            var collection = store.GetCollection<CarCategory>();
            if (collection.Count > 0) return;
            var smallCar = new CarCategory() { Name = "SmallCar", BaseDayRentalPrice = 300, BaseKmPrice = 0, DayRateMultipler = 1, DistanceRateMultipler = 0 };
            var combi = new CarCategory() { Name = "Combi", BaseDayRentalPrice = 400, BaseKmPrice = 2, DayRateMultipler = 1.3, DistanceRateMultipler = 1 };
            var truck = new CarCategory() { Name = "Truck", BaseDayRentalPrice = 800, BaseKmPrice = 5, DayRateMultipler = 1.5, DistanceRateMultipler = 1.5 };
            var suv = new CarCategory() { Name = "SUV", BaseDayRentalPrice = 600, BaseKmPrice = 3, DayRateMultipler = 1.3, DistanceRateMultipler = 1.2 };
            var carCategories = new List<CarCategory> { smallCar,combi,truck,suv };

            collection.InsertMany(carCategories);
        }
        private void SeedCars() {
            var collection = store.GetCollection<Car>();
            if (collection.Count > 0) return;
            // Read the categories back from the store so the cars get the stored ids
            var categories = store.GetCollection<CarCategory>().AsQueryable().ToList();
            CarCategory? Category(string name) => categories.FirstOrDefault(c => c.Name == name);

            var cars = new List<Car>
            {
                new() { Brand = "Volkswagen", Model = "Polo", Year = 2023, RegistrationNumber = "ABC123", VIN = "WVWZZZAWZPU000001", MileAge = 12_500, CarCategory = Category("SmallCar")! },
                new() { Brand = "Volvo", Model = "V60", Year = 2022, RegistrationNumber = "DEF456", VIN = "YV1ZW25V0N1000002", MileAge = 34_200, CarCategory = Category("Combi")! },
                new() { Brand = "Scania", Model = "P280", Year = 2021, RegistrationNumber = "GHI789", VIN = "YS2P4X20005000003", MileAge = 88_900, CarCategory = Category("Truck")! }
            };

            // Skip any car whose category has been removed from the store
            collection.InsertMany(cars.Where(c => c.CarCategory != null));
        }


    }
}
