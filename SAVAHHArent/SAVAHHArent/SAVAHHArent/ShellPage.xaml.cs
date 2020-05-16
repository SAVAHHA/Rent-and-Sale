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

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private T Deserialize<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        private void Serialize<T>(string fileName, T data)
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }

        //public async void Welcome()
        //{
        //    await Navigation.PushModalAsync(new LoginPage());
        //}
    }
}