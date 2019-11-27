using FirebaseApp.Repository;
using FirebaseApp.View.Pages;
using System;
using Xamarin.Forms;

namespace FirebaseApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnFbMessaging_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new DbPage());
        }

        private async void BtnFbStorage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new StoragePage());
        }

        private async void BtnLocalList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BookStorePage(new BookRepository()));
        }

        private async void BtnPreference_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BookStorePage(new PrefRepository()));
        }

        private async void BtnSqliteDatabase_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BookStorePage(new SqliteRepository()));
        }

        private async void BtnFirebaseDatabase_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BookStorePage(new FirebaseRepository()));
        }

        private async void BtnFbAuth_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FbAuthPage());
        }
    }
}
