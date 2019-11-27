using CommonServiceLocator;
using MyUnityApp.MyDatabindings;
using MyUnityApp.UnityApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xamarin.Forms;

namespace MyUnityApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            List<MyDropModel> myDataSource = new List<MyDropModel>();
            myDataSource.Add(new MyDropModel() { Id = 3, Name = "Krishna" });
            myDataSource.Add(new MyDropModel() { Id = 5, Name = "Mahendra" });
            myDataSource.Add(new MyDropModel() { Id = 6, Name = "Ravi" });

            my_dropdown.ItemsSource = myDataSource;
            my_dropdown.SelectedIndex = 1;

            my_dropdown.ItemSelected += MyDropDownSelected;

        }

        private void MyDropDownSelected(object sender, ItemSelectedEventArgs e)
        {
            int index = e.SelectedIndex;
            //MyDropdown drop = sender as MyDropdown;
            //MyDropdown drop2 = ((MyDropdown)sender);
            MyDropModel myDropModel = my_dropdown.ItemsSource[index];

            IUnityContainer container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            ILogger logger = container.Resolve<ILogger>();

            RefitTest refitTest = container.Resolve<RefitTest>();
            refitTest.GetDataAsync().ContinueWith(result =>
            {

                if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                {
                    // Get result and update any UI here.
                    var model = result.Result;
                    logger.LogMessage(model.ToString());
                }
                else if (result.IsFaulted)
                {
                    logger.LogMessage(result.Exception.ToString());
                }
                else if (result.IsCanceled)
                {
                    // Task cancelled
                    logger.LogMessage("API Request "+result.Status.ToString() + " It May due to internet connectivity or restricted internet connectivity");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
            .ConfigureAwait(false);// Execute API call on background or worker thread.
        }

        private void BtnSpinnerLabelRotation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new SpinnerAndLabel()));
        }
    }
}
