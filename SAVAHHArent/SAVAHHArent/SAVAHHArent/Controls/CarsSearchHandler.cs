using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms;
using SAVAHHArent.Model;

namespace SAVAHHArent.Controls
{
    public class CarsSearchHandler: SearchHandler
    {
        public IList<Car> Cars { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Cars
                    .Where(car => car.Model.ToLower().Contains(newValue.ToLower()) | car.CarMake.ToLower().Contains(newValue.ToLower()))
                    .ToList<Car>();

            }
        }

        protected override async void OnItemSelected (object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            //await Shell.Current.GoToAsync($"{GetNavigationTarget()}?model={((Car)item).Model}");
            await Shell.Current.GoToAsync($"saledetails?carmodel={((Car)item).Model}");
        }

        //string GetNavigationTarget()
        //{
        //    return (Shell.Current as ShellPage).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        //}
    }
}
