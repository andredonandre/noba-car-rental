using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Infra.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int BookingNumber { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required Car Car { get; set; }
        public Customer? Customer { get; set; }
        public Rental? Rental { get; set; }
        public string Status => GetStatus();

        private string GetStatus() {
            if (Rental?.StartDate != null && Rental.EndDate == null) return RentalStatus.Ongoing;
            if (Rental?.StartDate != null && Rental.EndDate != null) return RentalStatus.Complete;
            return RentalStatus.Booked;
        }
    }
}
