using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SAVAHHArent;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new LoginPage());
            await Shell.Current.GoToAsync("loginPage");
        }

        private async void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            string UserName = NameEntry.Text;
            string UserLogin = LoginEntry.Text;
            string UserPassword = PasswordEntry.Text;
            string UserConfirmPassword = ConfirmPasswordEntry.Text;

            if (UserConfirmPassword == UserPassword)
            {
                try
                {
                    string myConnectionString = "Server=127.0.0.1;User Id=savahha;Password=1111;Database=rentandsale;Connection Timeout=200";
                    MySqlConnection connection = new MySqlConnection(myConnectionString);
                    connection.Open();
                    //MySqlCommand checkCommand = new MySqlCommand("SELECT * FROM Users WHERE Login=@login", connection);
                    //checkCommand.Parameters.AddWithValue("@login", UserLogin);
                    //MySqlDataReader mySqlDataReaderCheck = checkCommand.ExecuteReader();
                    //await DisplayAlert("COOL", "", "OK");
                    //if (mySqlDataReaderCheck.HasRows)
                    //{
                    //    await DisplayAlert("Attention", "User with such login already exists", "OK");
                    //    LoginEntry.Text = "";
                    //}
                    //else
                    //{
                        MySqlCommand newCommand = new MySqlCommand("INSERT INTO Users(Name_user,Login,Password) VALUES(@name,@login,@password)", connection);
                        newCommand.Parameters.AddWithValue("@login", UserLogin);
                        newCommand.Parameters.AddWithValue("@name", UserName);
                        newCommand.Parameters.AddWithValue("@password", UserPassword);
                        newCommand.ExecuteNonQuery();
                    await DisplayAlert("COOL", "", "OK");
                    connection.Close();
                    await Navigation.PushModalAsync(new LoginPage());
                    //}

                    //connection.Close();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("No Internet connection", ex.InnerException?.Message, "ok");
                    
                }



                
            }
            else
            {
                await DisplayAlert("Attention", "Passwords do not match", "OK");
                PasswordEntry.Text = "";
                ConfirmPasswordEntry.Text = "";
            }
        }
    }
}