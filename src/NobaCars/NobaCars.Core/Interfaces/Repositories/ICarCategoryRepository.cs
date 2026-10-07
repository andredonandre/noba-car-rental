using NobaCars.Core.Entities;

namespace NobaCars.Core.Interfaces.Repositories
{
    public interface ICarCategoryRepository
    {
        IEnumerable<CarCategory> GetAll();
        Task AddAsync(CarCategory carCategory);
        Task AddRangeAsync(IEnumerable<CarCategory> carCategories);
    }
}
