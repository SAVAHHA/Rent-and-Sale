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
    public partial class CurrentRentDetailPage : ContentPage
    {
        public int TotalCost { get; set; }
        public int IDrent { get; set; }
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

        public static int CarID { get; set; }
        public CurrentRentDetailPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            CarID = 1111;
            r();
            testLabel.Text = _carModel;
            string myConnectionString = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM rents WHERE ID_User=@idUser and ID_Car=@idCar", connection);
            newCommand.Parameters.AddWithValue("@idUser", App.ID_inHost);
            newCommand.Parameters.AddWithValue("@idCar", CarID);
            MySqlDataReader dataReader = newCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    object _idRent = dataReader.GetValue(0);
                    object _placeStart = dataReader.GetValue(3);
                    object _costOfDelivery = dataReader.GetValue(4);
                    object _dateStart = dataReader.GetValue(5);
                    object _dateEnd = dataReader.GetValue(6);
                    object _numberOfDays = dataReader.GetValue(7);
                    object _costOfRent = dataReader.GetValue(8);
                    object _placeEnd = dataReader.GetValue(9);
                    object _costOfFinish = dataReader.GetValue(10);
                    object _IDinsurance = dataReader.GetValue(11);
                    object _payMade = dataReader.GetValue(12);
                    object _carWasTaken = dataReader.GetValue(13);
                    object _carWasDelivered = dataReader.GetValue(14);
                    object _carWasReturned = dataReader.GetValue(15);
                    object _sanctionsWereMade = dataReader.GetValue(16);
                    object _idPayment = dataReader.GetValue(17);

                    IDrent = Int32.Parse(_idRent.ToString());
                    PlaceStart = _placeStart.ToString();
                    CostOfDelivery = Int32.Parse(_costOfDelivery.ToString());
                    DateStart = DateTime.Parse(_dateStart.ToString());
                    DateEnd = DateTime.Parse(_dateEnd.ToString());
                    NumberOfDays = Int32.Parse(_numberOfDays.ToString());
                    CostOfRent = Int32.Parse(_costOfRent.ToString());
                    PlaceEnd = _placeEnd.ToString();
                    CostOfFinish = Int32.Parse(_costOfFinish.ToString());
                    IDinsurance = Int32.Parse(_IDinsurance.ToString());
                    CarWasTaken = Int32.Parse(_carWasTaken.ToString());
                    CarWasDelivered = Int32.Parse(_carWasDelivered.ToString());
                    CarWasReturned = Int32.Parse(_carWasReturned.ToString());
                    SanctionsWereMade = Int32.Parse(_sanctionsWereMade.ToString());
                    IDpayment = Int32.Parse(_idPayment.ToString());

                }
            }
            //await DisplayAlert(ID_Payment.ToString(), "", "OK");
            connection.Close();
            idRentLabel.Text = PlaceStart;
        }

        private async void r()
        {
            await DisplayAlert(ModelCarLabel.Text, IdCarLabel.Text, "OP");
        }

        private async void endSessionButton_Clicked(object sender, EventArgs e)
        {
            var nowDate = DateTime.Now;
            if (nowDate <= DateEnd)
            {
                sanctionsLabel.Text = "no";
                SanctionsWereMade = 0;
                
            }
            else
            {
                SanctionsWereMade = 1;
                sanctionsLabel.Text = SanctionData.Sanctions[0].Name;
                string myConnectionString2 = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
                MySqlConnection connection2 = new MySqlConnection(myConnectionString2);
                connection2.Open();
                MySqlCommand newCommand2 = new MySqlCommand("INSERT INTO rents_sanctions(ID_Rent,ID_Sanction) VALUES(@idRent, @idSanction)", connection2);
                newCommand2.Parameters.AddWithValue("@idRent", IDrent);
                newCommand2.Parameters.AddWithValue("@idSanction", 1);
                newCommand2.ExecuteNonQuery();
                connection2.Close();
                TotalCost += SanctionData.Sanctions[0].Cost;
            }

            TotalCost += CostOfDelivery + CostOfFinish + CostOfRent + InsuranceData.Insurances[IDinsurance].Cost;
            totalCostLabel.Text = TotalCost.ToString();

            await Task.Delay(300);
            bool result = await DisplayAlert("You need to pay", totalCostLabel.Text, "Yes", "No");
            if (result)
            {
                string myConnectionString3 = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
                MySqlConnection connection3 = new MySqlConnection(myConnectionString3);
                connection3.Open();
                MySqlCommand newCommand3 = new MySqlCommand("INSERT INTO Payments(Type_of_payment,Sum,ID_User,Date_of_payment) VALUES(@type,@sum,@id,@time)", connection3);
                newCommand3.Parameters.AddWithValue("@type", "Card");
                newCommand3.Parameters.AddWithValue("@sum", totalCostLabel.Text);
                newCommand3.Parameters.AddWithValue("@id", App.ID_inHost);
                newCommand3.Parameters.AddWithValue("@time", nowDate);
                newCommand3.ExecuteNonQuery();
                await DisplayAlert("Ready", "You've payed", "OK");
                connection3.Close();

                //int ID_Payment = 0;
                string myConnectionString4 = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
                MySqlConnection connection4 = new MySqlConnection(myConnectionString4);
                connection4.Open();
                MySqlCommand newCommand4 = new MySqlCommand("SELECT ID_Payment FROM Payments WHERE ID_User=@idUser and Date_of_payment=@nowTime", connection4);
                newCommand4.Parameters.AddWithValue("@idUser", App.ID_inHost);
                newCommand4.Parameters.AddWithValue("@nowTime", nowDate);
                MySqlDataReader dataReader = newCommand4.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        object idPayment = dataReader.GetValue(0);
                        IDpayment = Int32.Parse(idPayment.ToString());
                    }
                }
                await DisplayAlert(IDpayment.ToString(), IDrent.ToString(), "OK");
                connection4.Close();


                string myConnectionString5 = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
                MySqlConnection connection5 = new MySqlConnection(myConnectionString5);
                connection5.Open();
                MySqlCommand newCommand5 = new MySqlCommand("UPDATE rents SET ID_Payment=@idPayment WHERE ID_Rent=@idRent", connection5);
                newCommand5.Parameters.AddWithValue("@idPayment", IDpayment);
                newCommand5.Parameters.AddWithValue("@idRent", IDrent);
                newCommand5.ExecuteNonQuery();
                await DisplayAlert("Ready", "You've payed", "OK");
                connection5.Close();

                //string myConnectionString6 = "Server = 172.17.171.49; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
                //MySqlConnection connection6 = new MySqlConnection(myConnectionString6);
                //connection6.Open();
                //MySqlCommand newCommand6 = new MySqlCommand("UPDATE Cars SET Rent=1 WHERE ID_Car=@idCar", connection6);
                //newCommand6.Parameters.AddWithValue("@idCar", IdCarLabel.Text);
                //newCommand6.ExecuteNonQuery();
                //await DisplayAlert("Ready", "You've finished the rent", "OK");
                //connection6.Close();

                //await Shell.Current.GoToAsync("///profile");
            }
        }
    }
}