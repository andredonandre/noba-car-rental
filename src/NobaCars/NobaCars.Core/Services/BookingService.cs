using NobaCars.Core.Helpers;
using NobaCars.Core.Interfaces;
using NobaCars.Core.Interfaces.Repositories;
using NobaCars.Core.Models.Booking;
using NobaCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Services
{
    public class BookingService(IBookingRepository bookings) : IBookingService
    {
        public async Task CreateBooking(CreateBookingModel bookingDetails) {
            var booking = new Booking() {
                Car = bookingDetails.Car,
                Customer = bookingDetails.Customer,
                StartDate = bookingDetails.PickUp,
                EndDate = bookingDetails.DropOff,
                CreatedOn = DateTime.UtcNow};
            booking?.Rental?.StartDate = booking.StartDate;
            await bookings.AddAsync(booking);
        }
        public IEnumerable<Booking> GetBookings(){
            return bookings.GetAll();
        }

        public async Task RegisterDropOff(int BookingId, DateTime dropOffTime, int endMileage)
        {
            var booking = GetBooking(BookingId);
            booking?.Rental?.EndMileage = endMileage;
            booking?.Rental?.EndDate = dropOffTime;
            booking?.Rental?.Price = PriceCalculator.CalculatePrice(booking.Rental, booking.Car.CarCategory);
            await bookings.UpdateAsync(booking);
        }

        public async Task RegisterPickup(int BookingId, DateTime pickUpTime)
        {
            var booking = GetBooking(BookingId);
            booking.Rental = new Rental(pickUpTime, booking.Car.MileAge);
            await bookings.UpdateAsync(booking);
        }

        private Booking GetBooking(int bookingId) =>
            bookings.GetById(bookingId) ?? throw new InvalidOperationException($"Booking {bookingId} was not found.");
    }
}
