using NobaCars.Core.Entities;
using NobaCars.Core.Interfaces.Repositories;

namespace NobaCars.Infra.Repositories
{
    internal sealed class JsonBookingRepository(Database db) : IBookingRepository
    {
        public IEnumerable<Booking> GetAll() =>
            db.Store.GetCollection<Booking>().AsQueryable().ToList();

        public Booking? GetById(int id) =>
            db.Store.GetCollection<Booking>().AsQueryable().FirstOrDefault(b => b.Id == id);

        public Task AddAsync(Booking booking) =>
            db.Store.GetCollection<Booking>().InsertOneAsync(booking);

        public Task UpdateAsync(Booking booking) =>
            db.Store.GetCollection<Booking>().UpdateOneAsync(booking.Id, booking);
    }
}
