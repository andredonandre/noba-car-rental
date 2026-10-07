using NobaCars.Core.Entities;
using NobaCars.Core.Interfaces.Repositories;

namespace NobaCars.Infra.Repositories
{
    internal sealed class JsonCarCategoryRepository(Database db) : ICarCategoryRepository
    {
        public IEnumerable<CarCategory> GetAll() =>
            db.Store.GetCollection<CarCategory>().AsQueryable().ToList();

        public Task AddAsync(CarCategory carCategory) =>
            db.Store.GetCollection<CarCategory>().InsertOneAsync(carCategory);

        public Task AddRangeAsync(IEnumerable<CarCategory> carCategories) =>
            db.Store.GetCollection<CarCategory>().InsertManyAsync(carCategories);
    }
}
