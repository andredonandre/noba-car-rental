using NobaCars.Core.Entities;
using NobaCars.Core.Interfaces.Repositories;

namespace NobaCars.Infra.Repositories
{
    internal sealed class JsonCarRepository(Database db) : ICarRepository
    {
        public IEnumerable<Car> GetAll() =>
            db.Store.GetCollection<Car>().AsQueryable().ToList();

        public Car? GetById(int id) =>
            db.Store.GetCollection<Car>().AsQueryable().FirstOrDefault(c => c.Id == id);

        public Task AddAsync(Car car) =>
            db.Store.GetCollection<Car>().InsertOneAsync(car);

        public Task AddRangeAsync(IEnumerable<Car> cars) =>
            db.Store.GetCollection<Car>().InsertManyAsync(cars);
    }
}
