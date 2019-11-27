using LocaleAppTest.Resx;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace LocaleAppTest
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();

            var items = new List<string>(10);
            for (int i = 1; i <= 5; i++)
            {
                items.Add($"Item \n\t value is :{i}");
            }

            Items = new ObservableCollection<string>(items);

            myCustomLisView.ItemsSource = Items;

            myLabel.Text = LocaleAppResources.myLabelText;
        }

        private void ChangeLocale_Clicked(object sender, EventArgs e)
        {
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(GetNextLocaleString());
            LocaleAppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            //Navigation.PushModalAsync(new MainPage());
            App.Current.MainPage = new NavigationPage(new MainPage());// Relaunch Same Page.
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            Items.Add("Added New Item:"+(Items.Count+1));
            myCustomLisView.UpdateView?.Invoke();
        }

        private string GetNextLocaleString()
        {
            int LocaleIndex = new Random().Next(3);
            string lang;
            switch (LocaleIndex)
            {
                case 1:
                    lang = "ar";
                    break;
                case 2:
                    lang = "es";
                    break;
                default:
                    lang = "en";
                    break;
            }
            return lang;
        }
    }
}
