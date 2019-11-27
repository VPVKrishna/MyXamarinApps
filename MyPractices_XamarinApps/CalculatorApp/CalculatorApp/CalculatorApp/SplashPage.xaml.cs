using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculatorApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            base.OnAppearing();

            openCalculatorPage();
        }

        private async void openCalculatorPage()
        {
            await Task.Delay(2000);
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new CalculationPage(), false);
        }
    }
}