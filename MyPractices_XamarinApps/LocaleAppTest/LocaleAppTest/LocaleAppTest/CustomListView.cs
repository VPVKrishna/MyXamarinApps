using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LocaleAppTest
{
    public class CustomListView : ListView
    {
        public static readonly BindableProperty IsScrollingEnableProperty =
            BindableProperty.Create(nameof(IsScrollingEnable),
                                    typeof(bool),
                                    typeof(CustomListView),
                                    true);

        public bool IsScrollingEnable
        {
            get { return (bool)GetValue(IsScrollingEnableProperty); }
            set { SetValue(IsScrollingEnableProperty, value); }
        }

        public Action UpdateView
        {
            get; set;
        }
    }
}
