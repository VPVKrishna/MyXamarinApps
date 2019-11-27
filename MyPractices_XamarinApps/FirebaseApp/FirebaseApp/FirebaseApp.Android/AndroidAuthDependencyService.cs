using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using FirebaseApp.Helper;
using Java.Lang;
using Java.Util.Concurrent;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseApp.Droid.AndroidAuthDependencyService))]
namespace FirebaseApp.Droid
{
    public class AndroidAuthDependencyService : IAuthDependencyService
    {
        public async Task<string> LoginWithUsernameAndPasswordAsync(string username, string password)
        {
            var authResult = await FirebaseAuth.GetInstance(MainActivity.FbApp).SignInWithEmailAndPasswordAsync(username, password);
            var token = await authResult.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> RegisterWithUsernameAndPasswordAsync(string username, string password)
        {
            var authResult = await FirebaseAuth.GetInstance(MainActivity.FbApp).CreateUserWithEmailAndPasswordAsync(username, password);
            var token = await authResult.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var authResult = await FirebaseAuth.GetInstance(MainActivity.FbApp).CreateUserWithEmailAndPasswordAsync(username, oldPassword);
            await authResult.User.UpdatePasswordAsync(newPassword);
            var token = await authResult.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> ChangePasswordAsync(object currentUser, string newPassword)
        {
            var fbCurrentUser = currentUser as FirebaseUser;
            await fbCurrentUser.UpdatePasswordAsync(newPassword);
            var token = await fbCurrentUser.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> ChangePasswordAsync(string newPassword)
        {
            return await ChangePasswordAsync(GetCurrentUser(), newPassword);
        }

        public async Task ForgotPasswordAsync(string username)
        {
            await FirebaseAuth.GetInstance(MainActivity.FbApp).SendPasswordResetEmailAsync(username);
        }

        public object GetCurrentUser()
        {
            return FirebaseAuth.GetInstance(MainActivity.FbApp).CurrentUser;
        }

        public async Task<IList<string>> FetchProvidersAsync(string username)
        {
            var providers = await FirebaseAuth.GetInstance(MainActivity.FbApp).FetchProvidersForEmailAsync(username);
            return providers.Providers;
        }

        public void SendMobileCodeForVerification(string phone)
        {
            var fireAuth = FirebaseAuth.GetInstance(MainActivity.FbApp);
            var context = MainActivity.FbApp.ApplicationContext;
            
            PhoneAuthProvider.GetInstance(fireAuth).VerifyPhoneNumber(phone, 60, TimeUnit.Seconds, Executors.NewSingleThreadExecutor(), new MobileVerification());
        }
    }

    public class MobileVerification : PhoneAuthProvider.OnVerificationStateChangedCallbacks
    {
        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            throw new RuntimeException("Handle OnVerificationCompleted");
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            throw new RuntimeException("Handle OnVerificationFailed");
        }
    }
}