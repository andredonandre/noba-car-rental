using NobaCars.Core.Interfaces;
using NobaCars.Infra;
using NobaCars.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Services
{
    public class InventoryService(Database db) : IInventoryService
    {
        public async Task AddACar(Car car)
        {            
            var cars = db.store.GetCollection<Car>();
            await cars.InsertOneAsync(car);
        }

        public async Task AddCarCategory(CarCategory carCategory)
        {
            try
            {
                var categories = db.store.GetCollection<CarCategory>();
                await categories.InsertOneAsync(carCategory);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public Car GetCarById(string carId)
        {
            var item = db.store.GetItem<Car>(carId);
            return item;
        }

        public IEnumerable<CarCategory> GetCarCategories()
        {
            var collection = db.store.GetCollection<CarCategory>().AsQueryable();
            return collection;
        }

        public IEnumerable<Car> GetCars()
        {
            var collection = db.store.GetCollection<Car>().AsQueryable();
            return collection;
        }
    }
}
