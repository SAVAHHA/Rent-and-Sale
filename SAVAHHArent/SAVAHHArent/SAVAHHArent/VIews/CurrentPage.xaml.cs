using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.VIews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CarModel", "carmodel")]
    public partial class CurrentPage : ContentPage
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

        public CurrentPage()
        {
            InitializeComponent();
           // R();
        }

        //public async void R()
        //{
        //    int _costPerDay = Int32.Parse(CostPerDayLabel.Text);
        //    await DisplayAlert(_costPerDay.ToString(), "cost", "OK");
        //}
    }
}