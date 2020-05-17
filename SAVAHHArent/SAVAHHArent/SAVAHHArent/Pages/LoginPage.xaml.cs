using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
//using System.Data.SqlClient;
using System.Data;
using SAVAHHArent.Pages;
using SAVAHHArent.Model;
using SAVAHHArent.Data;

namespace SAVAHHArent.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        

        public LoginPage()
        {
            InitializeComponent();
            
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var login = loginEntry.Text;
            var password = passwordEntry.Text;
            bool check = true;

            foreach (var user in UserData.Users)
            {
                if (user.Login == login)
                {
                    if (user.Password == password)
                    {
                        await Shell.Current.GoToAsync($"profilePage?userlogin={login}");
                    }
                    else
                    {
                        await DisplayAlert("Rejected", "Incorrect password", "OK");
                        passwordEntry.Text = "";
                    }
                    check = false;
                }
            }
            if (check == true)
            {
                await DisplayAlert("Rejected", "No user with such login", "OK");
                loginEntry.Text = "";
                passwordEntry.Text = "";
            }

            //try
            //{
            //    string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True";
            //    MySqlConnection connection = new MySqlConnection(myConnectionString);
            //    connection.Open();
            //    MySqlCommand newCommand = new MySqlCommand("SELECT * FROM Users WHERE Login=@login", connection);
            //    newCommand.Parameters.AddWithValue("@login", login);
            //    MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            //    if (mySqlDataReader.HasRows)
            //    {
            //        while (mySqlDataReader.Read())
            //        {
            //            object id = mySqlDataReader.GetValue(0);
            //            ID = Int32.Parse(id.ToString());
            //            object name = mySqlDataReader.GetValue(1);
            //            object loginGet = mySqlDataReader.GetValue(2);
            //            object passwordGet = mySqlDataReader.GetValue(3);

            //            if (password == passwordGet.ToString())
            //            {
            //                //await Navigation.PushModalAsync(new MainPage(Int32.Parse(id.ToString()), name.ToString()));
            //                //await Navigation.PushModalAsync(new TabbedMainPage());
            //                await Shell.Current.GoToAsync("profilePage");

            //            }
            //            else
            //            {
            //                await DisplayAlert("Rejected", "Incorrect password", "OK");
            //                passwordEntry.Text = "";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        await DisplayAlert("Rejected", "No user with such login", "OK");
            //        loginEntry.Text = "";
            //        passwordEntry.Text = "";
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("No Internet connection", ex.InnerException?.Message, "ok");
            //}



            //    string connectionString = "Data Source=192.168.1.74,49170;Initial Catalog=Rent_and_Sale;Integrated Security=True";
            //    SqlConnection sqlConnection = new SqlConnection(connectionString);
            //    sqlConnection.Open();
            //    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Login=@login", sqlConnection);
            //    sqlCommand.Parameters.AddWithValue("@login", login);
            //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //    if (sqlDataReader.HasRows)
            //    {
            //        while (sqlDataReader.Read())
            //        {
            //            object id = sqlDataReader.GetValue(0);
            //            object name = sqlDataReader.GetValue(1);
            //            object passwordGet = sqlDataReader.GetValue(4);

            //            if (password == passwordGet.ToString())
            //            {
            //                //await Navigation.PushModalAsync(new MainPage(Int32.Parse(id.ToString()), name.ToString()));
            //                //await Navigation.PushModalAsync(new TabbedMainPage());
            //                await Shell.Current.GoToAsync("profilePage");

            //            }
            //            else
            //            {
            //                await DisplayAlert("Rejected", "Incorrect password", "OK");
            //                passwordEntry.Text = "";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        await DisplayAlert("Rejected", "No user with such login", "OK");
            //        loginEntry.Text = "";
            //        passwordEntry.Text = "";
            //    }
            //    sqlConnection.Close();
         }       



        private async void registrationButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("registrationPage");
        }

        private void loginEntry_Focused(object sender, FocusEventArgs e)
        {
            loginEntry.Text = "";
        }

        private void passwordEntry_Focused(object sender, FocusEventArgs e)
        {
            passwordEntry.Text = "";
            passwordEntry.IsPassword = true;
        }

        
    }
}
