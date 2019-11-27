using Xamarin.Forms;
using CommonLogicProject;

namespace PrismApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            buttonSubmit.TextColor = SharedLogic.GetButtonColor();
        }
    }
}