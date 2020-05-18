using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RentPage : ContentPage
    {
        public RentPage()
        {
            InitializeComponent();
        }

        async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string carModel = (e.CurrentSelection.FirstOrDefault() as Car).Model;
            // This works because route names are unique in this application.
            //await DisplayAlert("", carModel, "ok");
            await Shell.Current.GoToAsync($"rentdetails?carmodel={carModel}");
        }
    }
}