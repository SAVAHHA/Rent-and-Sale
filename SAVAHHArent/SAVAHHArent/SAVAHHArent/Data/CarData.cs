using System;
using System.Collections.Generic;
using System.Text;
using SAVAHHArent.Model;
using MySql.Data.MySqlClient;

namespace SAVAHHArent.Data
{
    public static class CarData
    {
        public static IList<Car> Cars { get; private set; }
        public static IList<Car> CarsForRent { get; private set; }

        static CarData()
        {
            Cars = new List<Car>();

            string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True; Connection Timeout=200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM Cars", connection);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object _carID = mySqlDataReader.GetValue(0);
                    object _carMake = mySqlDataReader.GetValue(1);
                    object _model = mySqlDataReader.GetValue(2);
                    object _year = mySqlDataReader.GetValue(3);
                    object _colour = mySqlDataReader.GetValue(4);
                    object _numberOfSeats = mySqlDataReader.GetValue(5);
                    object _horsepower = mySqlDataReader.GetValue(6);
                    object _cost = mySqlDataReader.GetValue(7);
                    object _govNumber = mySqlDataReader.GetValue(8);
                    object _rent = mySqlDataReader.GetValue(9);
                    object _costPerDay = mySqlDataReader.GetValue(10);
                    object _photo = mySqlDataReader.GetValue(11);



                    var car = new Car { ID_Car = Int32.Parse(_carID.ToString()), CarMake = _carMake.ToString(), Model = _model.ToString(), Year = Int32.Parse(_year.ToString()), Colour = _colour.ToString(), NumberOfSeats = Int32.Parse(_numberOfSeats.ToString()), Horsepower = Int32.Parse(_horsepower.ToString()), Cost = Int32.Parse(_cost.ToString()), GovNumber = _govNumber.ToString(), CostPerDay = Int32.Parse(_costPerDay.ToString()), Rent = Int32.Parse(_rent.ToString()), Photo = new Uri(_photo.ToString()) };
                    Cars.Add(car);
                }
            }
            CarsForRent = new List<Car>();
            //  1 - доступна для аренды, 2 - арендована
            foreach (var _car in Cars)
            {
                if (_car.Rent == 1)
                {
                    CarsForRent.Add(_car);
                }
            }
        }
    }
}
