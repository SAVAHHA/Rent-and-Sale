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
    public partial class RentDetailPage : ContentPage
    {
        public string CarModel
        {
            set
            {
                BindingContext = CarData.Cars.FirstOrDefault(m => m.Model == Uri.UnescapeDataString(value));
            }
        }

        public RentDetailPage()
        {
            InitializeComponent();
        }

        private async void RentButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"confirmRentPage?carmodel={ModelLabel.Text}");
        }

        protected override void OnAppearing()
        {
            R();

            //userNameLabel.Text = App.Database.GetItem(1).Name;
            base.OnAppearing();
        }

        public async void R()
        {
            var longs = await App.Database.GetUsersAsync();
            await DisplayAlert("", longs.Count().ToString(), "ok");
        }
    }
}