using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Models
{
    public class AddCarCategoryModel
    {
        public required string Name { get; set; }
        public int DayPriceMultiplier { get; set; } = 1;
        public int DistancePriceMultiplier { get; set; } = 1;
        public int BaseKmPrice { get; set; } = 0;
        public int BaseDayRentalPrice { get; set; } = 0;
    }
}
