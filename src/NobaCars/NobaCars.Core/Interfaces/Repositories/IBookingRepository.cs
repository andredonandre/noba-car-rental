using NobaCars.Core.Entities;

namespace NobaCars.Core.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking? GetById(int id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
    }
}
