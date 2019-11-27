using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Plugin.CurrentActivity;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;
using System.Collections.Generic;
using Java.Lang;
using Java.Util.Concurrent;

namespace FirebaseApp.Droid
{
    [Activity(Label = "FirebaseApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Firebase.FirebaseApp FbApp;
        private string msg = "Empty Message";
        internal static readonly string CHANNEL_ID = "123";
        internal static readonly int NOTIFICATION_ID = 2;
        public static string mVerificationId;
        private static PhoneAuthProvider.ForceResendingToken mResendToken;
        private PhoneAuthProvider.OnVerificationStateChangedCallbacks mCallbacks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            // Firebase Authentication initialization
            InitFirebaseAuthentication();

            LoadApplication(new App());

            IsPlayServicesAvailable();

            Toast.MakeText(this, msg, ToastLength.Long).Show();
            CreateNotificationChannel();
        }

        private void InitFirebaseAuthentication()
        {
            if (FbApp == null)
            {
                FirebaseOptions options = new FirebaseOptions.Builder()
                    .SetApplicationId("1:421483881936:android:46afbd6b7ce3fb75")
                    .SetApiKey("AIzaSyC7wlR96WTArXnrMzLL-T7X4E7w_jPcLS8")
                    .Build();

                bool hasBeenInitialized = false;
                IList<Firebase.FirebaseApp> firebaseApps = Firebase.FirebaseApp.GetApps(this);
                foreach (Firebase.FirebaseApp app in firebaseApps)
                {
                    if (app.Name.Equals(Firebase.FirebaseApp.DefaultAppName))
                    {
                        hasBeenInitialized = true;
                        FbApp = app;
                    }
                }
                if(!hasBeenInitialized)
                    FbApp = Firebase.FirebaseApp.InitializeApp(this, options);
            }

            mCallbacks =new MobileOTPVerification();

            //SendMobileCodeForVerification("+917660991464");
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msg = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msg = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msg = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void SendMobileCodeForVerification(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                phone = "+917660991464";
            }
            var fireAuth = FirebaseAuth.GetInstance(MainActivity.FbApp);

            PhoneAuthProvider.GetInstance(fireAuth).VerifyPhoneNumber(phone, 60, TimeUnit.Seconds, this, mCallbacks);
        }


        public class MobileOTPVerification : PhoneAuthProvider.OnVerificationStateChangedCallbacks
        {
            public override void OnVerificationCompleted(PhoneAuthCredential credential)
            {
                throw new RuntimeException("Handle OnVerificationCompleted");
            }

            public override void OnVerificationFailed(FirebaseException exception)
            {
                throw new RuntimeException("Handle OnVerificationFailed "+exception.Message);
            }

            public override void OnCodeAutoRetrievalTimeOut(string verificationId)
            {
                base.OnCodeAutoRetrievalTimeOut(verificationId);
            }

            public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
            {
                MainActivity.mVerificationId = verificationId;
                MainActivity.mResendToken = forceResendingToken;
            }
        }
    }
}