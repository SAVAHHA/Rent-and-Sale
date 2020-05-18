using System;
using System.Collections.Generic;
using System.Text;

namespace SAVAHHArent.Model
{
    public class Car
    {
        public int ID_Car { get; set; }
        public string CarMake { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Colour { get; set; }
        public int NumberOfSeats { get; set; }
        public int Horsepower { get; set; }
        public int Cost { get; set; }
        public string GovNumber { get; set; }
        public int Rent { get; set; }
        public int CostPerDay { get; set; }
        public Uri Photo { get; set; }
    }
}
