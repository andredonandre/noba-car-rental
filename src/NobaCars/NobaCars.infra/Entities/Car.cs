using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Infra.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CarCategory CarCategory { get; set; }
        public string Brand { get; set; }
        public int MileAge { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
    }
}
