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
    public partial class ConfirmRentPage : ContentPage
    {
        public string _carModel { get; set; }
        public string CarModel
        {
            set
            {
                BindingContext = CarData.Cars.FirstOrDefault(m => m.Model == Uri.UnescapeDataString(value));
                _carModel = Uri.UnescapeDataString(value);
            }
            get
            {
                return _carModel;
            }
        }

        public ConfirmRentPage()
        {
            InitializeComponent();
        }
    }
}