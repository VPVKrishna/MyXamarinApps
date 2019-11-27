using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirebaseApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoragePage : ContentPage
	{
        // Reference link: https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-storage/
        private MediaFile file;

        public StoragePage ()
		{
			InitializeComponent ();
		}

        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgPicked.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                var url = await StoreImages(file.GetStream());
                Debug.WriteLine("Uploaded Image Url : "+url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<string> StoreImages(Stream stream)
        {
            string timeStamp = GetTimestamp(DateTime.Now);
            string ImageName = "Img_" + timeStamp + ".png";
            var fbStorage = new FirebaseStorage("fir-app-b1cb2.appspot.com");
            var storageImage = await fbStorage
                .Child("MyDatabase")
                .Child(ImageName)
                .PutAsync(stream);
            string imageUrl = storageImage;
            return imageUrl;
        }

        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMMdd_HHmmss_ffff");
        }

        private async void BtnStore_Clicked(object sender, EventArgs e)
        {
            var url = await StoreImages(file.GetStream());
            Debug.WriteLine("Uploaded Image Url : " + url);
        }
    }
}