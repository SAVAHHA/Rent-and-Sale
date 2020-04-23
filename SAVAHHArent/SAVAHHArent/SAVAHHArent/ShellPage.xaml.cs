using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public ShellPage()
        {
            //Welcome();
            InitializeComponent();
        }

        //public async void Welcome()
        //{
        //    await Navigation.PushModalAsync(new LoginPage());
        //}
    }
}