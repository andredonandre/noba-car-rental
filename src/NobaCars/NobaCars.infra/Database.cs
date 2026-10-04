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

        }
        private void AddCarCategories() {
            var collection = store.GetCollection<CarCategory>();
            var smallCar = new CarCategory() { Name = "SmallCar", BaseDayRentalPrice = 33, BaseKmPrice = 34, DayRateMultipler = 1 };
            var combi = new CarCategory() { Name = "Combi", BaseDayRentalPrice = 33, BaseKmPrice = 34, DayRateMultipler = 1.3 };
            var truck = new CarCategory() { Name = "Truck", BaseDayRentalPrice = 33, BaseKmPrice = 34, DayRateMultipler = 1.5 };
            var carCategories = new List<CarCategory> { smallCar,combi,truck };
            collection.InsertMany(carCategories);
        }
    }
}
