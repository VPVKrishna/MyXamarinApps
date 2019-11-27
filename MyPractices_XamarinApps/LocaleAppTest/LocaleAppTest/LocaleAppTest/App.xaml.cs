using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LocaleAppTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //SetCultureToArabic();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }

        private void SetCultureToSpanish()
        {
            CultureInfo englishUSCulture = new CultureInfo("es");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }

        private void SetCultureToArabic()
        {
            CultureInfo englishUSCulture = new CultureInfo("ar");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }
    }
}
