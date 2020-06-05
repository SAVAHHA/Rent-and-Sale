using System;
using System.Collections.Generic;
using System.Text;

namespace SAVAHHArent.Model
{
    public class Rent
    {
        public int TotalCost { get; set; }
        public int IDrent { get; set; }
        public int IDCar { get; set; }
        public string PlaceStart { get; set; }
        public int CostOfDelivery { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int NumberOfDays { get; set; }
        public int CostOfRent { get; set; }
        public string PlaceEnd { get; set; }
        public int CostOfFinish { get; set; }
        public int IDinsurance { get; set; }
        public int CarWasTaken { get; set; }
        public int CarWasDelivered { get; set; }
        public int CarWasReturned { get; set; }
        public int SanctionsWereMade { get; set; }
        public int IDpayment { get; set; }
    }
}
