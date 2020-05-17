using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent2.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent2.Views
{
    [QueryProperty("CarModel", "carmodel")]
    public partial class SaleDetailPage : ContentPage
    {
        public string CarModel
        {
            set
            {
                BindingContext = CarData.Cars.FirstOrDefault(m => m.Model == Uri.UnescapeDataString(value));
            }
        }

        public SaleDetailPage()
        {
            InitializeComponent();
        }
    }
}