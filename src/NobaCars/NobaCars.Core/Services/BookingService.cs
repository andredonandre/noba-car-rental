using NobaCars.Core.Helpers;
using NobaCars.Core.Interfaces;
using NobaCars.Core.Models.Booking;
using NobaCars.Infra;
using NobaCars.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Services
{
    public class BookingService(Database db) : IBookingService
    {
        public async Task CreateBooking(CreateBookingModel bookingDetails) {
            var booking = new Booking() {
                BookingNumber = 10000, 
                Car = bookingDetails.Car, 
                Customer = bookingDetails.Customer, 
                StartDate = bookingDetails.PickUp,
                EndDate = bookingDetails.DropOff,
                CreatedOn = DateTime.UtcNow};
            booking?.Rental?.StartDate = booking.StartDate;
            var bookings = db.store.GetCollection<Booking>();
            await bookings.InsertOneAsync(booking);
        }
        public IEnumerable<Booking> GetBookings(){
            var collection = db.store.GetCollection<Booking>().AsQueryable();
            return collection;
        }

        public async Task RegisterDropOff(int BookingId, DateTime dropOffTime, int endMileage)
        {
            var bookings = db.store.GetCollection<Booking>();
            var booking = bookings.AsQueryable().Where(b => b.Id  == BookingId).First();
            booking?.Rental?.EndMileage = endMileage;
            booking?.Rental?.EndDate = dropOffTime;
            booking?.Rental?.Price = PriceCalculator.CalculatePrice(booking.Rental, booking.Car.CarCategory);
            await bookings.UpdateOneAsync(BookingId, booking);
        }

        public async Task RegisterPickup(int BookingId, DateTime pickUpTime)
        {
            var bookings = db.store.GetCollection<Booking>();
            var booking = bookings.AsQueryable().Where(b => b.Id == BookingId).First();
            booking.Rental = new Rental(pickUpTime, booking.Car.MileAge);
            await bookings.UpdateOneAsync(BookingId, booking);
        }
    }
}
