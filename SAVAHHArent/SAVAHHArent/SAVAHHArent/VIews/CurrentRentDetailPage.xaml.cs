using SAVAHHArent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CarModel", "carmodel")]
    public partial class CurrentRentDetailPage : ContentPage
    {
        public string CarModel
        {
            set
            {
                BindingContext = CarData.Cars.FirstOrDefault(m => m.Model == Uri.UnescapeDataString(value));
            }
        }
        public CurrentRentDetailPage()
        {
            InitializeComponent();
        }
    }
}