using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Infra.Entities
{
    public class Booking
    {
        public int BookingNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public required Car Car { get; set; }
        public Customer? Customer { get; set; }
        public Rental? Rental { get; set; }
    }
}
