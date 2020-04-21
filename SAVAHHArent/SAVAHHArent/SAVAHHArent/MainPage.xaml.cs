using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace SAVAHHArent
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //public SqlConnection connection = new SqlConnection(@"Data Source=SAVAHHA\SQLEXPRESS;Initial Catalog=Rent_and_Sale;Integrated Security=True");
        MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder()
        {
            Server = "db4free.net",
            Port = 3306,
            Database = "rentsale",    // Имя БД
            UserID = "anaisanais",       // Имя пользователя БД
            Password = "anais321",   // Пароль пользователя БД
            CharacterSet = "utf-8" // Кодировка Базы Данных
        };

        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            var login = loginEntry.Text;
            var password = passwordEntry.Text;
            string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();



            //MySqlCommand newCommand = new MySqlCommand("INSERT INTO Users(Login,Password) VALUES(@login,@password)", connection);

            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM Users WHERE Login=@login", connection);
            newCommand.Parameters.AddWithValue("@login", login);
            //newCommand.Parameters.AddWithValue("@password", password);
            //newCommand.ExecuteNonQuery();
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows) // если есть данные
            {
                // выводим названия столбцов
                //Console.WriteLine("{0}\t{1}\t{2}", mySqlDataReader.GetName(0), mySqlDataReader.GetName(1), mySqlDataReader.GetName(2));

                while (mySqlDataReader.Read()) // построчно считываем данные
                {
                    object id = mySqlDataReader.GetValue(0);
                    object loginGet = mySqlDataReader.GetValue(1);
                    object passwordGet = mySqlDataReader.GetValue(2);

                    if (password == passwordGet.ToString())
                    {
                        await Navigation.PushModalAsync(new Page()); 
                    }
                }

                
            }



            //reader.Close();
            connection.Close();
        }
    }
}
