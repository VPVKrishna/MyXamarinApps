﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace LocaleAppTest
{
    public class ItemsControl : WrapLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            "ItemsSource", typeof(IList), typeof(ItemsControl), propertyChanging: OnItemsSourceChanged);

        /// <summary>
        /// Gets or sets the items source - can be any collection of elements.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            "ItemTemplate", typeof(DataTemplate), typeof(ItemsControl));

        /// <summary>
        /// Gets or sets the item template used to generate the visuals for a single item.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public ItemsControl()
        {
            Padding = new Thickness(0);
            Margin = new Thickness(0);
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ItemsControl)bindable).OnItemsSourceChangedImpl((IList)oldValue, (IList)newValue);
        }

        void OnItemsSourceChangedImpl(IList oldValue, IList newValue)
        {
            // Unsubscribe from the old collection
            if (oldValue != null)
            {
                INotifyCollectionChanged ncc = oldValue as INotifyCollectionChanged;
                if (ncc != null)
                    ncc.CollectionChanged -= OnCollectionChanged;
            }

            if (newValue == null)
            {
                Children.Clear();
            }
            else
            {
                FillContainer(newValue);
                INotifyCollectionChanged ncc = newValue as INotifyCollectionChanged;
                if (ncc != null)
                    ncc.CollectionChanged += OnCollectionChanged;
            }
        }

        /// <summary>
        /// This method takes our items source and generates visuals for
        /// each item in the collection; it can reuse visuals which were created
        /// previously and simply changes the binding context.
        /// </summary>
        /// <param name="newValue">New items to display</param>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        void FillContainer(IList newValue)
        {
            var template = ItemTemplate;
            var visuals = Children;

            if (template == null)
                throw new NotSupportedException("ItemTemplate must be specified!");

            var newVisuals = new List<View>(Children); //using a list to avoid multiple layout refresh
            Children.Clear();

            for (int i = 0; i < newVisuals.Count; i++)
            {
                newVisuals[i].IsVisible = i < newValue.Count;
            }

            for (int i = 0; i < newValue.Count; i++)
            {
                var dataItem = newValue[i];

                if (visuals.Count > i)
                {
                    if (template != null)
                    {
                        var visualItem = visuals[i];
                        visualItem.BindingContext = dataItem;
                    }
                }
                else
                {
                    if (template != null)
                    {
                        // Pull real template from selector if necessary.
                        var dSelector = template as DataTemplateSelector;
                        if (dSelector != null)
                            template = dSelector.SelectTemplate(dataItem, this);

                        var view = template.CreateContent() as View;
                        if (view != null)
                        {
                            view.BindingContext = dataItem;
                            newVisuals.Add(view);
                        }
                    }
                }
            }

            foreach (var child in newVisuals) //wish they had a nice AddRange method here
                if (child.IsVisible)
                    Children.Add(child);
        }

        /// <summary>
        /// This is called when the data source collection implements
        /// collection change notifications and the data has changed.
        /// This is not optimized - it simply replaces all the data.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FillContainer((IList)sender);
        }
    }
}
