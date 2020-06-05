using SAVAHHArent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CarModel", "carmodel")]
    public partial class ConfirmSalePage : ContentPage
    {
        public string _carModel { get; set; }
        public string CarModel
        {
            set
            {
                BindingContext = CarData.Cars.FirstOrDefault(m => m.Model == Uri.UnescapeDataString(value));
                _carModel = Uri.UnescapeDataString(value);
            }
            get
            {
                return _carModel;
            }
        }

        public ConfirmSalePage()
        {
            InitializeComponent();
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Pay", CostLabel.Text, "Ok");
            DateTime nowTime = DateTime.Now;
            string myConnectionString = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("INSERT INTO Payments(Type_of_payment,Sum,ID_User,Date_of_payment) VALUES(@type,@sum,@id,@time)", connection);
            newCommand.Parameters.AddWithValue("@type", "Card");
            newCommand.Parameters.AddWithValue("@sum", CostLabel.Text);
            newCommand.Parameters.AddWithValue("@id", App.ID_inHost);
            newCommand.Parameters.AddWithValue("@time", nowTime);
            newCommand.ExecuteNonQuery();
            await DisplayAlert("Ready", "You've payed", "OK");
            connection.Close();

            int ID_Payment = 0;
            string myConnectionString2 = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection2 = new MySqlConnection(myConnectionString2);
            connection2.Open();
            MySqlCommand newCommand2 = new MySqlCommand("SELECT ID_Payment FROM Payments WHERE ID_User=@idUser and Date_of_payment=@nowTime", connection2);
            newCommand2.Parameters.AddWithValue("@idUser", App.ID_inHost);
            newCommand2.Parameters.AddWithValue("@nowTime", nowTime);
            MySqlDataReader dataReader = newCommand2.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    object idPayment = dataReader.GetValue(0);
                    ID_Payment = Int32.Parse(idPayment.ToString());
                }
            }
            await DisplayAlert(ID_Payment.ToString(), "", "OK");
            connection2.Close();

            string myConnectionString3 = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection3 = new MySqlConnection(myConnectionString3);
            connection3.Open();
            MySqlCommand newCommand3 = new MySqlCommand("INSERT INTO Sales(Date_of_sale,ID_User,ID_Car,ID_Payment) VALUES(@nowTime,@idUser,@idCar,@idPayment)", connection3);
            newCommand3.Parameters.AddWithValue("@nowTime", nowTime);
            newCommand3.Parameters.AddWithValue("@idUser", App.ID_inHost);
            newCommand3.Parameters.AddWithValue("@idCar", IdCarLabel.Text);
            newCommand3.Parameters.AddWithValue("@idPayment", ID_Payment);
            newCommand3.ExecuteNonQuery();
            connection3.Close();

            string myConnectionString4 = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection4 = new MySqlConnection(myConnectionString4);
            connection4.Open();
            MySqlCommand newCommand4 = new MySqlCommand("UPDATE Cars SET Bought=1 WHERE ID_Car=@idCar", connection4);
            newCommand4.Parameters.AddWithValue("@idCar", IdCarLabel.Text);
            newCommand4.ExecuteNonQuery();
            await DisplayAlert("Ready", "You've bought the car", "OK");
            connection4.Close();


            App.Current.MainPage = new ShellPage();
            await Shell.Current.GoToAsync("///sale");
        }
    }
}