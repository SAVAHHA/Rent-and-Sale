using System;
using System.Collections.Generic;
using System.Text;
using SAVAHHArent.Model;
using MySql.Data.MySqlClient;

namespace SAVAHHArent.Data
{
    public static class UserData
    {
        public static IList<User> Users { get; set; }

        static UserData()
        {
            Users = new List<User>();



            string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM Users", connection);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object _userID = mySqlDataReader.GetValue(0);
                    object _name = mySqlDataReader.GetValue(1);
                    object _login = mySqlDataReader.GetValue(2);
                    object _password = mySqlDataReader.GetValue(3);


                    Users.Add(new User { ID_User = Int32.Parse(_userID.ToString()), Name = _name.ToString(), Login = _login.ToString(), Password = _password.ToString() });
                }
            }
                    //Users.Add(new User { ID_User = 1, Name = "Anna", Age = 19, Login = "savahha", Card = "0000 0000 0000 0000", Liecense = "00 0000", Passport = "4515 00000", Password = "1111", Phone = "+79160249905" });
        }
    }
}
