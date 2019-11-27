using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            string pngUrl = "https://cdn1.iconfinder.com/data/icons/Vista-Inspirate_1.0/48x48/mimetypes/svg.png";
            ImageSource Source = ImageSource.FromUri(new Uri(pngUrl));
            image_logo.Source = Source;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string catUrl = "https://static.pexels.com/photos/104827/cat-pet-animal-domestic-104827.jpeg";
            ImageSource Source = ImageSource.FromUri(new Uri(catUrl));
            image_logo.Source = Source;
        }
    }
}
