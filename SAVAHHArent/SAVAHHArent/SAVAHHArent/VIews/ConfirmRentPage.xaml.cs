using SAVAHHArent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;

namespace SAVAHHArent.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CarModel", "carmodel")]
    public partial class ConfirmRentPage : ContentPage
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

        public ConfirmRentPage()
        {
            InitializeComponent();
            List<string> infos = new List<string>();
            infos.Add("-");
            foreach (var _insurance in InsuranceData.Insurances)
            {
                string info = _insurance.Name + " " + _insurance.Cost.ToString();
                infos.Add(info);
                //InsurancePicker.ItemsSource.Add(info);
            }
            InsurancePicker.ItemsSource = infos;
        }

        private void GetCostButton_Clicked(object sender, EventArgs e)
        {
            int _costPerDay = Int32.Parse(CostPerDayLabel.Text);
            var DateStart = RentStartDatePicker.Date;
            var DateEnd = RentEndDatePicker.Date;

            int numberOfDays = DateEnd.DayOfYear - DateStart.DayOfYear;
            int SumForRent = (numberOfDays + 1) * _costPerDay;
            if (PlaceStartPicker.Items[PlaceStartPicker.SelectedIndex] != "Из автоцентра" & PlaceStartPicker.Items[PlaceStartPicker.SelectedIndex] != "-")
            {
                SumForRent += 300;
            }
            if (PlaceEndPicker.Items[PlaceEndPicker.SelectedIndex] != "В автоценрт" & PlaceEndPicker.Items[PlaceEndPicker.SelectedIndex] != "-")
            {
                SumForRent += 300;
            }
            if (InsurancePicker.Items[InsurancePicker.SelectedIndex] != "-")
            {
                SumForRent += Int32.Parse(InsurancePicker.Items[InsurancePicker.SelectedIndex].Split().Last());
            }
            SumOfRentLabel.Text = SumForRent.ToString();
            

        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            int _costPerDay = Int32.Parse(CostPerDayLabel.Text);
            int numberOfDays = RentEndDatePicker.Date.DayOfYear - RentStartDatePicker.Date.DayOfYear;
            int SumForRent = (numberOfDays + 1) * _costPerDay;
            //int Cost = Int32.Parse(SumOfRentLabel.Text);
            int CarID = Int32.Parse(IdCarLabel.Text);
            int InsuranceID = InsurancePicker.SelectedIndex;
            string PlaceStart = PlaceStartPicker.Items[PlaceStartPicker.SelectedIndex];
            string PlaceEnd = PlaceEndPicker.Items[PlaceEndPicker.SelectedIndex];
            var DateStart = RentStartDatePicker.Date;
            var DateEnd = RentEndDatePicker.Date;
            int CostStart = 0;
            int CostEnd = 0;
            if (PlaceStartPicker.Items[PlaceStartPicker.SelectedIndex] != "Из автоцентра" & PlaceStartPicker.Items[PlaceStartPicker.SelectedIndex] != "-")
            {
                CostStart += 300;
            }
            if (PlaceEndPicker.Items[PlaceEndPicker.SelectedIndex] != "В автоценрт" & PlaceEndPicker.Items[PlaceEndPicker.SelectedIndex] != "-")
            {
                CostEnd += 300;
            }
            if (InsurancePicker.Items[InsurancePicker.SelectedIndex] != "-")
            {
                SumForRent += Int32.Parse(InsurancePicker.Items[InsurancePicker.SelectedIndex].Split().Last());
            }

            int Cost = CostEnd + CostStart + SumForRent;
            await DisplayAlert("Are you sure?", Cost.ToString(), "Ok");
            DateTime nowTime = DateTime.Now;
            string myConnectionString3 = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection3 = new MySqlConnection(myConnectionString3);
            connection3.Open();
            MySqlCommand newCommand3 = new MySqlCommand("INSERT INTO Rents(ID_User,ID_Car,Place_start,Cost_of_delivery,Date_start,Date_end,Number_of_days,Cost_of_rent,Place_end,Cost_of_finish,ID_Insuarance) " +
                "VALUES(@idUser,@idCar,@placeStart,@costOfDelivery,@dateStart,@dateEnd,@numberOfDays,@costOfRent,@placeEnd,@costOfFinish,@idInsurance)", connection3);
            newCommand3.Parameters.AddWithValue("@idUser", App.ID_inHost);
            newCommand3.Parameters.AddWithValue("@idCar", CarID);
            newCommand3.Parameters.AddWithValue("@placeStart", PlaceStart);
            newCommand3.Parameters.AddWithValue("@costOfDelivery", CostStart);
            newCommand3.Parameters.AddWithValue("@dateStart", DateStart);
            newCommand3.Parameters.AddWithValue("@dateEnd", DateEnd);
            newCommand3.Parameters.AddWithValue("@numberOfDays", numberOfDays);
            newCommand3.Parameters.AddWithValue("@costOfRent", SumForRent);
            newCommand3.Parameters.AddWithValue("@placeEnd", PlaceEnd);
            newCommand3.Parameters.AddWithValue("@costOfFinish", CostEnd);
            newCommand3.Parameters.AddWithValue("@idInsurance", InsuranceID);
            newCommand3.ExecuteNonQuery();
            connection3.Close();

            string myConnectionString4 = "Server = 192.168.111.113; Port = 3306; User Id = savahha; Password = 1111; Database = rentandsale; OldGuids = True; Connection Timeout = 200";
            MySqlConnection connection4 = new MySqlConnection(myConnectionString4);
            connection4.Open();
            MySqlCommand newCommand4 = new MySqlCommand("UPDATE Cars SET Rent=2 WHERE ID_Car=@idCar", connection4);
            newCommand4.Parameters.AddWithValue("@idCar", IdCarLabel.Text);
            newCommand4.ExecuteNonQuery();
            await DisplayAlert("Ready", "You've rented the car", "OK");
            connection4.Close();

            var car = new Car();
            foreach (var _car in CarData.CarsForRent)
            {
                if (_car.ID_Car == CarID)
                {
                    car = _car;
                }
            }
            CarData.CarsForRent.Remove(car);

            App.Current.MainPage = new ShellPage();
        }
    }
}