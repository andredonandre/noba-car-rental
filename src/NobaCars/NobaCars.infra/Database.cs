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
            AddCarCategories();
        }
        private void AddCarCategories() {
            var collection = store.GetCollection<CarCategory>();
            if (collection.Count > 0) return;
            var smallCar = new CarCategory() { Name = "SmallCar", BaseDayRentalPrice = 300, BaseKmPrice = 0, DayRateMultipler = 1, DistanceRateMultipler = 0 };
            var combi = new CarCategory() { Name = "Combi", BaseDayRentalPrice = 400, BaseKmPrice = 2, DayRateMultipler = 1.3, DistanceRateMultipler = 1 };
            var truck = new CarCategory() { Name = "Truck", BaseDayRentalPrice = 800, BaseKmPrice = 5, DayRateMultipler = 1.5, DistanceRateMultipler = 1.5 };
            var suv = new CarCategory() { Name = "SUV", BaseDayRentalPrice = 600, BaseKmPrice = 3, DayRateMultipler = 1.3, DistanceRateMultipler = 1.2 };
            var carCategories = new List<CarCategory> { smallCar,combi,truck,suv };
            collection.InsertMany(carCategories);
        }
    }
}
