using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAVAHHArent.Data;
using System.IO;
using System.Threading.Tasks;

namespace SAVAHHArent
{
    public partial class App : Application
    {

        public const string DATABASE_NAME = "table.db";
        static Table database;
        public static Table Database
        {
            get
            {
                if (database == null)
                {
                    database = new Table(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public static int ID
        {
            get
            {
                if (App.Database.GetUsersAsync().Result.Count != 0)
                {
                    var users = App.Database.GetUsersAsync().Result;
                    return users[0].Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int ID_inHost
        {
            get
            {
                if (App.ID != 0)
                {
                    var users = App.Database.GetUsersAsync().Result;
                    return users[0].Id_inHost;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static string Name
        {
            get
            {
                if (App.ID != 0)
                {
                    var users = App.Database.GetUsersAsync().Result;
                    return users[0].Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string Login
        {
            get
            {
                if (App.ID != 0)
                {
                    var users = App.Database.GetUsersAsync().Result;
                    return users[0].Login;
                }
                else
                {
                    return "";
                }
            }
        }

        public static int CurrentCarID { get; set; }

        public static string Password
        {
            get
            {
                if (App.ID != 0)
                {
                    var users = App.Database.GetUsersAsync().Result;
                    return users[0].Password;
                }
                else
                {
                    return "";
                }
            }
        }

        public App()
        {
            InitializeComponent();
            //D();
            //int _check = Check().Result;
            //App.Database.SaveItem(new UserTable { Id = 1, Login = "savahha", Name = "Anna", Password = "1111" });
            MainPage = new ShellPage();
        }

      
        public async Task<int> Check()
        {
            int check;
            var users = await App.Database.GetUsersAsync();
            if (users.Count != 0)
            {
                check = 1;
            }
            else
            {
                check = 0;
            }
            return check;
            //await App.Database.DeleteAll();
        }

        public async void D()
        {
            await App.Database.SaveUserAsync(new UserTable { Id_inHost = 1, Login = "savahha", Name = "Anna", Password = "1111" });
            //await App.Database.DeleteAll();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
