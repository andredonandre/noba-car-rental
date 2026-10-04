using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Infra.Entities
{
    public class Rental
    {
        public Guid Id { get; set; }
        public int BookingId { get; set; }
        public int StartMileage { get; set; } = 0;
        public int EndMileage { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public int NumberOfDays => RentalPeriod().Days;
        public int NumberOfKilometers => EndMileage - StartMileage;

        public Rental(DateTime startDate, int startMileage) {
            StartDate = startDate;
            StartMileage = startMileage;
        }
        private TimeSpan RentalPeriod() {
            if (StartDate == null || EndDate == null) throw new ArgumentException("This operation requires both the startdate and enddate");
            return (EndDate - StartDate).Value; 
        }
        public void DropOff(int endMileage) {
            EndDate = DateTime.UtcNow;
            EndMileage = EndMileage;
        }
    }
}
 