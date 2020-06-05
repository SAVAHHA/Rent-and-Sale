using System;
using System.Collections.Generic;
using SAVAHHArent.Model;
using MySql.Data.MySqlClient;
using System.Text;

namespace SAVAHHArent.Data
{
    public static class RentData
    {
        public static IList<Rent> Rents { get; set; }
        public static IList<Rent> CurrentRents { get; set; }
        public static IList<Rent> PreviousRents { get; set; }

        static RentData()
        {
            Rents = new List<Rent>();
            CurrentRents = new List<Rent>();
            PreviousRents = new List<Rent>();

            string myConnectionString = "Server=192.168.111.113;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM Rents WHERE ID_User=@userID", connection);
            newCommand.Parameters.AddWithValue("@userID", App.ID_inHost);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object rentId = mySqlDataReader.GetValue(0);
                    object carId = mySqlDataReader.GetValue(2);
                    object placeStart = mySqlDataReader.GetValue(3);
                    object costOfDelivery = mySqlDataReader.GetValue(4);
                    object dateStart = mySqlDataReader.GetValue(5);
                    object dateEnd = mySqlDataReader.GetValue(6);
                    object numberOfDays = mySqlDataReader.GetValue(7);
                    object costOfRent = mySqlDataReader.GetValue(8);
                    object placeEnd = mySqlDataReader.GetValue(9);
                    object costOfFinish = mySqlDataReader.GetValue(10);
                    object insuranceId = mySqlDataReader.GetValue(11);
                    object payMade = mySqlDataReader.GetValue(12);
                    object sanctionsWereMade = mySqlDataReader.GetValue(16);
                    object paymentId = mySqlDataReader.GetValue(17);

                    Rents.Add(new Rent { IDCar = Int32.Parse(carId.ToString()), IDrent = Int32.Parse(rentId.ToString()), CostOfDelivery = Int32.Parse(costOfDelivery.ToString()), CostOfFinish = Int32.Parse(costOfFinish.ToString()), CostOfRent = Int32.Parse(costOfRent.ToString()), DateEnd = DateTime.Parse(dateEnd.ToString()), DateStart = DateTime.Parse(dateStart.ToString()), IDinsurance = Int32.Parse(insuranceId.ToString()), IDpayment = Int32.Parse(paymentId.ToString()), NumberOfDays = Int32.Parse(numberOfDays.ToString()), PlaceEnd = placeEnd.ToString(), PlaceStart = placeStart.ToString(), SanctionsWereMade = Int32.Parse(sanctionsWereMade.ToString()) });

                }
            }
            connection.Close();

            foreach (var rent in Rents)
            {
                if (rent.IDpayment == 0)
                {
                    CurrentRents.Add(rent);
                }
                else
                {
                    PreviousRents.Add(rent);
                }
            }
        }
    }
}
