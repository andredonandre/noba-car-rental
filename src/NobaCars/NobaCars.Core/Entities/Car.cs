using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name => $"{Brand} {Model} {Year} - {RegistrationNumber}";
        public CarCategory CarCategory { get; set; } = new();
        public string Brand { get; set; }
        public int MileAge { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
    }
}
