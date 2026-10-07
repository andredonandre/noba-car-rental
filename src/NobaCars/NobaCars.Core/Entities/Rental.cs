using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Entities
{
    public class Rental
    {
        public int StartMileage { get; set; } = 0;
        public int EndMileage { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Price { get; set; } = 0;
        public double NumberOfDays => RentalPeriod().TotalDays < 0 ? 0 : RentalPeriod().TotalDays;
        public int NumberOfKilometers => CalculateDistance();

        public Rental(DateTime startDate, int startMileage) {
            StartDate = startDate;
            StartMileage = startMileage;
        }
        private TimeSpan RentalPeriod() {
            if (StartDate == null || EndDate == null) return new TimeSpan(0);
            return (EndDate - StartDate).Value; 
        }
        public int CalculateDistance() {
            var difference = (EndMileage - StartMileage);
            if (difference < 0) return 0;
            return difference;
        }
    }
}
 