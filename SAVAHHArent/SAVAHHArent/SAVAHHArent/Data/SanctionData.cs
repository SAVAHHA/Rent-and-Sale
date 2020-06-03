using System;
using System.Collections.Generic;
using System.Text;
using SAVAHHArent.Model;
using MySql.Data.MySqlClient;

namespace SAVAHHArent.Data
{
    public static class SanctionData
    {
        public static IList<Sanction> Sanctions { get; private set; }

        static SanctionData()
        {
            Sanctions = new List<Sanction>();

            // string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True;Connection Timeout=200";
            string myConnectionString = "Server=172.17.171.49;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM sanctions", connection);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object _sanctionID = mySqlDataReader.GetValue(0);
                    object _name = mySqlDataReader.GetValue(1);
                    object _cost = mySqlDataReader.GetValue(2);

                    var sanction = new Sanction { ID_Sanction = Int32.Parse(_sanctionID.ToString()), Name = _name.ToString(), Cost = Int32.Parse(_cost.ToString()) };
                    Sanctions.Add(sanction);
                }
            }
            connection.Close();
        }
    }
}
