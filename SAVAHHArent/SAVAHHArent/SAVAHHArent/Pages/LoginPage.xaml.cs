using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
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

            //foreach (var user in UserData.Users)
            //{
            //    if (user.Login == login)
            //    {
            //        if (user.Password == password)
            //        {
            //            var _user = new UserTable { Id_inHost = 1, Login = login, Name = "Anna", Password = password };
            //            await App.Database.SaveUserAsync(_user);

            //            await Shell.Current.GoToAsync($"profilePage?userid={"1"}");
            //        }
            //        else
            //        {
            //            await DisplayAlert("Rejected", "Incorrect password", "OK");
            //            passwordEntry.Text = "";
            //        }
            //        check = false;
            //    }
            //}
            //if (check == true)
            //{
            //    await DisplayAlert("Rejected", "No user with such login", "OK");
            //    loginEntry.Text = "";
            //    passwordEntry.Text = "";
            //}

            string myConnectionString = "Server=192.168.31.145;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand newCommand = new MySqlCommand("SELECT * FROM users WHERE Login=@login", connection);
            newCommand.Parameters.AddWithValue("@login", login);
            MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            await DisplayAlert("", "COOl", "OK");
            if (mySqlDataReader.HasRows)
            {
                while (mySqlDataReader.Read())
                {
                    object _id = mySqlDataReader.GetValue(0);
                    object _name = mySqlDataReader.GetValue(1);
                    object _loginGet = mySqlDataReader.GetValue(2);
                    object _passwordGet = mySqlDataReader.GetValue(3);

                    if (password == _passwordGet.ToString())
                    {
                        var _user = new UserTable { Id_inHost = Int32.Parse(_id.ToString()), Login = _loginGet.ToString(), Name = _name.ToString(), Password = _passwordGet.ToString() };
                        await App.Database.SaveUserAsync(_user);
                        //await Shell.Current.GoToAsync($"profilePage?userid={"1"}");
                        await Shell.Current.GoToAsync("profilePage");

                    }
                    else
                    {
                        await DisplayAlert("Rejected", "Incorrect password", "OK");
                        passwordEntry.Text = "";
                    }
                }
            }
            else
            {
                await DisplayAlert("Rejected", "No user with such login", "OK");
                loginEntry.Text = "";
                passwordEntry.Text = "";
            }
            connection.Close();

            //try
            //{
            //    // string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True;Connection Timeout=200";
            //    string myConnectionString = "Server=192.168.31.145;Port=3306;User Id=savahha;Password=1111;Database=rentandsale;OldGuids=True;Connection Timeout=200";
            //    MySqlConnection connection = new MySqlConnection(myConnectionString);
            //    MySqlCommand newCommand = new MySqlCommand("SELECT * FROM users WHERE Login=@login", connection);
            //    newCommand.Parameters.AddWithValue("@login", login);
            //    MySqlDataReader mySqlDataReader = newCommand.ExecuteReader();
            //    await DisplayAlert("", "COOl", "OK");
            //    if (mySqlDataReader.HasRows)
            //    {
            //        while (mySqlDataReader.Read())
            //        {
            //            object _id = mySqlDataReader.GetValue(0);
            //            object _name = mySqlDataReader.GetValue(1);
            //            object _loginGet = mySqlDataReader.GetValue(2);
            //            object _passwordGet = mySqlDataReader.GetValue(3);

            //            if (password == _passwordGet.ToString())
            //            {
            //                var _user = new UserTable { Id_inHost = Int32.Parse(_id.ToString()), Login = _loginGet.ToString(), Name = _name.ToString(), Password = _passwordGet.ToString() };
            //                await App.Database.SaveUserAsync(_user);
            //                //await Shell.Current.GoToAsync($"profilePage?userid={"1"}");
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
