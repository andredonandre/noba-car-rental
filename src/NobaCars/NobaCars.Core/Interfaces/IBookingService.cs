using NobaCars.Core.Models.Booking;
using NobaCars.Core.Entities;

namespace NobaCars.Core.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetBookings();
        Task CreateBooking(CreateBookingModel booking);
        Task RegisterPickup(int BookingId, DateTime pickupTime);
        Task RegisterDropOff(int BookingId, DateTime dropoffTime, int endMileage);
    }
}