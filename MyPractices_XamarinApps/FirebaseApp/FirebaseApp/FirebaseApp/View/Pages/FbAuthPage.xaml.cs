using FirebaseApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirebaseApp.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FbAuthPage : ContentPage
	{
        private IAuthDependencyService AuthDependencyService;
        private string token;

        public FbAuthPage ()
		{
			InitializeComponent ();

            AuthDependencyService = DependencyService.Get<IAuthDependencyService>();
		}

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                token = await AuthDependencyService.LoginWithUsernameAndPasswordAsync(entryUserName.Text, entryPassword.Text);
                await DisplayAlert("Success", "Logged in successfully...!\n"+token, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                token = await AuthDependencyService.RegisterWithUsernameAndPasswordAsync(entryUserName.Text, entryPassword.Text);
                await DisplayAlert("Success", "Registered successfully...!\n" + token, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void ButtonChangePassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                token = await AuthDependencyService.ChangePasswordAsync(entryUserName.Text, entryPassword.Text, entryPassword.Text);
                await DisplayAlert("Success", "Changed password successfully...!\n" + token, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void ButtonForgotPassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                await AuthDependencyService.ForgotPasswordAsync(entryUserName.Text);
                await DisplayAlert("Success", "Password sent to registered mail successfully...!\n" + token, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void ButtonFetchProviders_Clicked(object sender, EventArgs e)
        {
            try
            {
                var providers = await AuthDependencyService.FetchProvidersAsync(entryUserName.Text);
                await DisplayAlert("Success", "Fetched Providers for given user...!\n" + providers, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void ButtonSendVerificationCode_Clicked(object sender, EventArgs e)
        {
            try
            {
                AuthDependencyService.SendMobileCodeForVerification(entryUserName.Text);
                await DisplayAlert("Success", "Fetched Providers for given user...!\n" , "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}