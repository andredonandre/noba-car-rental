using NobaCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Interfaces
{
    public interface IInventoryService
    {
        Task AddACar(Car car);
        Task AddCarCategory(CarCategory carCategory);
        Car? GetCarById(int carId);
        IEnumerable<Car> GetCars();
        IEnumerable<CarCategory> GetCarCategories();
    }
}
