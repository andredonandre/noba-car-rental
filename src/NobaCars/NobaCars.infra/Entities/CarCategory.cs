using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Infra.Entities
{
    public class CarCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DayRateMultipler { get; set; } = 1;
        public double DistanceRateMultipler { get; set; } = 1;
        public int BaseKmPrice { get; set; }
        public int BaseDayRentalPrice { get; set; }
    }
}
