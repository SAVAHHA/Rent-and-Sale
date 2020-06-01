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

            string myConnectionString = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("INSERT INTO Payments(Type_of_payment,Sum,ID_User,Date_of_payment) VALUES(@type,@sum,@id,@time)", connection);
            newCommand.Parameters.AddWithValue("@type", "Card");
            newCommand.Parameters.AddWithValue("@sum", CostLabel.Text);
            newCommand.Parameters.AddWithValue("@id", App.ID_inHost);
            newCommand.Parameters.AddWithValue("@time", DateTime.Now);
            newCommand.ExecuteNonQuery();
            await DisplayAlert("COOL", "", "OK");
            connection.Close();

        }
    }
}