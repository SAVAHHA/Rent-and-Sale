using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;
using SAVAHHArent.Data;
using SAVAHHArent.VIews;

namespace SAVAHHArent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public int Check;
        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        public Dictionary<string, Type> Routes { get { return routes; } }

        public ShellPage()
        {
            
            InitializeComponent();
            RegisterRoutes();
            Welcome();
            //profileTab.Content = new LoginPage();
            if (App.ID == 0)
            {
                profileTab.Content = new LoginPage();
            }
            else
            {
                var profilePage = new ProfilePage();
                profileTab.Content = profilePage;
                //profilePage.UserID = "1";
            }
        }

        void RegisterRoutes()
        {
            routes.Add("rentPage", typeof(RentPage));
            routes.Add("salePage", typeof(SalePage));
            routes.Add("saledetails", typeof(SaleDetailPage));
            routes.Add("rentdetails", typeof(RentDetailPage));
            routes.Add("registrationPage", typeof(RegistrationPage));
            routes.Add("loginPage", typeof(LoginPage));
            routes.Add("profilePage", typeof(ProfilePage));
            routes.Add("confirmRentPage", typeof(ConfirmRentPage));
            routes.Add("confirmSalePage", typeof(ConfirmSalePage));
            routes.Add("currentRentDetailPage", typeof(CurrentRentDetailPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        public async void Welcome()
        {
            var users = App.Database.GetUsersAsync().Result;

            await DisplayAlert(App.ID.ToString(), users.Count.ToString(), "ok");
        }
    }
}