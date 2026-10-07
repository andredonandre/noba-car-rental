using NobaCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Helpers
{
    public static class PriceCalculator
    {
        public static double CalculatePrice(Rental rental, CarCategory category) {            
            int baseDayRental = category.BaseDayRentalPrice,               
                baseKmPrice = category.BaseKmPrice,
                numberOfKilometers = rental.NumberOfKilometers;
            double dayMultiplier = category.DayRateMultipler, 
                distanceMultiplier = category.DistanceRateMultipler, 
                numberOfDays = rental.NumberOfDays;
            var result = ((baseDayRental * numberOfDays) * dayMultiplier) + ((baseKmPrice * numberOfKilometers) * distanceMultiplier);
            return Math.Round(result,2);
        }
    }
}
