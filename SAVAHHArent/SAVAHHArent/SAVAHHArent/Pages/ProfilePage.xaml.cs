using SAVAHHArent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVAHHArent.Model;

namespace SAVAHHArent.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public List<Car> BoughtCars { get; set; }
        public List<string> BoughtCarsString { get; set; }
        public List<Car> RentedCars { get; set; }
        public List<string> RentedCarsString { get; set; }
        public List<Car> CurrentRentCars { get; set; }
        public List<string> CurrentRentCarsString { get; set; }

        public ProfilePage()
        {
            InitializeComponent();
            BoughtCars = new List<Car>();
            BoughtCarsString = new List<string>();
            RentedCars = new List<Car>();
            RentedCarsString = new List<string>();
            LoadData();

            //Alert();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        public void LoadData()
        {
            BoughtCars = new List<Car>();
            BoughtCarsString = new List<string>();
            string myConnectionString = "Server=192.168.111.113;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM sales WHERE ID_User=@id", connection);
            newCommand.Parameters.AddWithValue("@id", App.ID_inHost);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object carID = mySqlDataReader.GetValue(3);
                    BoughtCarsString.Add(carID.ToString());
                }
            }
            connection.Close();

            foreach (var _id in BoughtCarsString)
            {
                var car = new Car();
                foreach (var _checkCar in CarData.Cars)
                {
                    if (_checkCar.ID_Car.ToString() == _id)
                    {
                        car = _checkCar;
                        BoughtCars.Add(car);
                    }
                }
            }
            BoughtCarsCollectionView.ItemsSource = BoughtCars;

            RentedCars = new List<Car>();
            RentedCarsString = new List<string>();
            CurrentRentCars = new List<Car>();
            CurrentRentCarsString = new List<string>();
            string myConnectionString2 = "Server=192.168.111.113;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            MySqlConnection connection2 = new MySqlConnection(myConnectionString2);
            connection2.Open();
            MySqlCommand newCommand2 = new MySqlCommand("SELECT * FROM rents WHERE ID_User=@id", connection2);
            newCommand2.Parameters.AddWithValue("@id", App.ID_inHost);
            MySqlDataReader mySqlDataReader2 = newCommand2.ExecuteReader();
            if (mySqlDataReader2.HasRows)
            {
                while (mySqlDataReader2.Read())
                {
                    object carID = mySqlDataReader2.GetValue(2);
                    object IDpayment = mySqlDataReader2.GetValue(17);
                    if (Int32.Parse(IDpayment.ToString()) == 0)
                    {
                        CurrentRentCarsString.Add(carID.ToString());
                    }
                    else
                    {
                        RentedCarsString.Add(carID.ToString());
                    }
                }
            }
            connection2.Close();

            foreach (var _id in RentedCarsString)
            {
                var car = new Car();
                foreach (var _checkCar in CarData.Cars)
                {
                    if (_checkCar.ID_Car.ToString() == _id)
                    {
                        car = _checkCar;
                        RentedCars.Add(car);
                    }
                }
            }
            RentedCarsCollectionView.ItemsSource = RentedCars;

            foreach (var _id in CurrentRentCarsString)
            {
                var car = new Car();
                foreach (var _checkCar in CarData.Cars)
                {
                    if (_checkCar.ID_Car.ToString() == _id)
                    {
                        car = _checkCar;
                        CurrentRentCars.Add(car);
                    }
                }
            }
            CurrentRentCarsCollectionView.ItemsSource = CurrentRentCars;

        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {

            string _login = loginEntry.Text;
            loginEntry.IsEnabled = true;
            passwordEntry.IsEnabled = true;

            Button _button = new Button { Text = "Save", Margin = 15, BackgroundColor = Color.LightPink };
            editStackLayout.Children.Add(_button);
            _button.Clicked += SaveButtonClicked;
            EditButton.IsEnabled = false;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            string _login = loginEntry.Text;
            string _password = passwordEntry.Text;

            try
            {
                //string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True;Connection Timeout=200";
                string myConnectionString = "Server=192.168.111.113Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand newCommand = new MySqlCommand("UPDATE Users SET Login=@login, Password=@password WHERE ID_User=@id", connection);
                newCommand.Parameters.AddWithValue("@login", _login);
                newCommand.Parameters.AddWithValue("@id", App.ID_inHost);
                newCommand.Parameters.AddWithValue("@password", _password);
                newCommand.ExecuteNonQuery();
                var newUser = new UserTable { Id_inHost = App.ID_inHost, Login = _login, Name = App.Name, Password = _password };
                await App.Database.DeleteUserAsync(App.ID);
                await App.Database.SaveUserAsync(newUser);
                await DisplayAlert("Ready", "Your profile was updated", "ok");
                connection.Close();
            }
            catch (Exception ex)
            {
                await DisplayAlert("No Internet connection", ex.InnerException?.Message, "ok");

            }

            Button button = (Button)sender;
            button.Text = "Saved!";
            button.BackgroundColor = Color.Pink;
            loginEntry.IsEnabled = false;
            passwordEntry.IsEnabled = false;
            await Task.Delay(200);
            EditButton.IsEnabled = true;
            mainStackLayout.Children.Remove(button);

        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteUserAsync(App.ID);
            App.Current.MainPage = new ShellPage();
        }

        private async void RentedCarsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string carId = (e.CurrentSelection.FirstOrDefault() as Car).ID_Car.ToString();
            App.CurrentCarID = Int32.Parse(carId);
            await Shell.Current.GoToAsync($"currentRentDetailPage?carid={carId}");
        }
    }
}