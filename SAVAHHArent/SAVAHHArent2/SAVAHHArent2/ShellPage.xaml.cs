using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent2.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using SAVAHHArent2.Data;
using SAVAHHArent2.Views;

namespace SAVAHHArent2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public int HasEntered = 0;
        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        public Dictionary<string, Type> Routes { get { return routes; } }

        public ShellPage()
        {
            //Welcome();
            InitializeComponent();
            RegisterRoutes();
            //Serialize<int>("DataFile.json", HasEntered);
        }

        void RegisterRoutes()
        {
            routes.Add("rentPage", typeof(RentPage));
            routes.Add("salePage", typeof(SalePage));
            routes.Add("saledetails", typeof(SaleDetailPage));
            routes.Add("registrationPage", typeof(RegistrationPage));
            routes.Add("loginPage", typeof(LoginPage));
            routes.Add("profilePage", typeof(ProfilePage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        
        //public async void Welcome()
        //{
        //    await Navigation.PushModalAsync(new LoginPage());
        //}
    }
}