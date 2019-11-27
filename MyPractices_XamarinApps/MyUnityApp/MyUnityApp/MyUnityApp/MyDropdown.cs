using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyUnityApp
{
    public class MyDropdown : View
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            propertyName: nameof(ItemsSource),
            returnType: typeof(List<MyDropModel>),
            declaringType: typeof(List<MyDropModel>),
            defaultValue: null);

        public List<MyDropModel> ItemsSource
        {
            get { return (List<MyDropModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            propertyName: nameof(SelectedIndex),
            returnType: typeof(int),
            declaringType: typeof(int),
            defaultValue: -1);

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public event EventHandler<ItemSelectedEventArgs> ItemSelected;

        public void OnItemSelected(int pos)
        {
            ItemSelected?.Invoke(this, new ItemSelectedEventArgs() { SelectedIndex = pos });
        }

        public MyDropModel GetValueAt(int index)
        {
            MyDropModel model = ItemsSource[index];
            return model;
        }
    }

    public class ItemSelectedEventArgs : EventArgs
    {
        public int SelectedIndex { get; set; }
    }

    public class MyDropModel
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
