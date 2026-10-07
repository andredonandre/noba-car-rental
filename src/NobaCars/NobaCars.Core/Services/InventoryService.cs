using NobaCars.Core.Interfaces;
using NobaCars.Core.Interfaces.Repositories;
using NobaCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Services
{
    public class InventoryService(ICarRepository cars, ICarCategoryRepository categories) : IInventoryService
    {
        public async Task AddACar(Car car)
        {
            await cars.AddAsync(car);
        }

        public async Task AddCarCategory(CarCategory carCategory)
        {
            try
            {
                await categories.AddAsync(carCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Car? GetCarById(int carId)
        {
            return cars.GetById(carId);
        }

        public IEnumerable<CarCategory> GetCarCategories()
        {
            return categories.GetAll();
        }

        public IEnumerable<Car> GetCars()
        {
            return cars.GetAll();
        }
    }
}
