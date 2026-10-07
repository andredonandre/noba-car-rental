using NobaCars.Core.Entities;

namespace NobaCars.Core.Interfaces.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car? GetById(int id);
        Task AddAsync(Car car);
        Task AddRangeAsync(IEnumerable<Car> cars);
    }
}
