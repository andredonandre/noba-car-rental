using NobaCars.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Helpers
{
    public static class PriceCalculator
    {
        public static double CalculatePrice(Rental rental, CarCategory category) {            
            int baseDayRental = category.BaseDayRentalPrice,
                numberOfDays = rental.NumberOfDays,
                baseKmPrice = category.BaseKmPrice,
                numberOfKilometers = rental.NumberOfKilometers;
            double dayMultiplier = category.DayRateMultipler, distanceMultiplier = category.DistanceRateMultipler;
            return ((baseDayRental * numberOfDays) * dayMultiplier) + ((baseKmPrice * numberOfKilometers) * distanceMultiplier);
        }
    }
}
