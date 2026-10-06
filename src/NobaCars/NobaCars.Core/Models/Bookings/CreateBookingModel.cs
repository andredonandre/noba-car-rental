using NobaCars.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace NobaCars.Core.Models.Booking
{
    public class CreateBookingModel
    {
        public int Id { get; set; }
        public int BookingNumber { get; set; } = 0;
        public Customer Customer { get; set; } = new();
        public Car Car { get; set; } = new();
        public DateTime PickUp { get; set; } = DateTime.UtcNow;
        public DateTime DropOff { get; set; } = DateTime.UtcNow;
    }
}
