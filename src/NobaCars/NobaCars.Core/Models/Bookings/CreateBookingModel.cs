using NobaCars.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace NobaCars.Core.Models.Booking
{
    public class CreateBookingModel
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int BookingNumber { get; set; } = 0;
        public required Customer Customer { get; set; }
        public required Car Car { get; set; }
        public required DateTime PickUp { get; set; }
        public required DateTime DropOff { get; set; }
    }
}
