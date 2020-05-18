using SAVAHHArent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("UserLogin", "userlogin")]
    public partial class ProfilePage : ContentPage
    {

        public string UserLogin
        {
            set
            {
                BindingContext = UserData.Users.FirstOrDefault(m => m.Login == Uri.UnescapeDataString(value));
            }
        }

        public ProfilePage()
        {
            InitializeComponent();

            

            //Alert();
            
        }

        public async void Alert()
        {
            await DisplayAlert("", "added", "yeah");
        }

        public async void R()
        {
            var longs = await App.Database.GetUsersAsync();
            await DisplayAlert("", longs.Count().ToString(), "ok");
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var userTable = new UserTable { Id = 1, Login = "savahha", Password = "1111", Name = "Anna" };
            //var userTable = (UserTable)BindingContext;
            if (userTable.Login != null)
            {
                //App.Database.SaveItemAsync(userTable);
                await App.Database.SaveUserAsync(userTable);
                Alert();
                R();
            }

            string _login = loginEntry.Text;
            loginEntry.IsEnabled = true;
            passwordEntry.IsEnabled = true;

            Button _button = new Button { Text = "Save", Margin = 15, BackgroundColor = Color.LightPink };
            mainStackLayout.Children.Add(_button);
            _button.Clicked += SaveButtonClicked;
            EditButton.IsEnabled = false;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            string _login = loginEntry.Text;
            string _password = passwordEntry.Text;

            foreach (var _user in UserData.Users)
            {
                if (_user.ID_User.ToString() == idLabel.Text)
                {
                    _user.Login = _login;
                    _user.Password = _password;



                    try
                    {
                        string myConnectionString = "Server=www.db4free.net;Port=3306;User Id=anaisanais;Password=anais321;Database=rentsale;OldGuids=True";
                        MySqlConnection connection = new MySqlConnection(myConnectionString);
                        connection.Open();
                        MySqlCommand newCommand = new MySqlCommand("UPDATE Users SET Login=@login, Password=@password WHERE ID_User=@id ", connection);
                        newCommand.Parameters.AddWithValue("@login", _login);
                        newCommand.Parameters.AddWithValue("@password", _password);
                        newCommand.Parameters.AddWithValue("@id", idLabel.Text);
                        newCommand.ExecuteNonQuery();
                        
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("No Internet connection", ex.InnerException?.Message, "ok");
                    }

                }
            }

            //App.Database.SaveItem(new UserTable { Id = 1, Login = loginEntry.Text, Password = passwordEntry.Text, Name = nameLabel.Text });

            Button button = (Button)sender;
            button.Text = "Saved!";
            button.BackgroundColor = Color.Pink;
            loginEntry.IsEnabled = false;
            passwordEntry.IsEnabled = false;
            await Task.Delay(200);
            EditButton.IsEnabled = true;
            mainStackLayout.Children.Remove(button);
            
        }
    }
}