using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocaleAppTest;
using LocaleAppTest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace LocaleAppTest.Droid
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            var superListView = Element as CustomListView;
            if (superListView == null)
                return;
            superListView.BackgroundColor = Color.Red;
            superListView.UpdateView = () => {
                var view = Control as Android.Widget.ListView;
                superListView.BackgroundColor = Color.Cyan;
                SetListViewHeightBasedOnChildren(view);
            };
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            MeasureSpecMode widthMode = MeasureSpec.GetMode(widthMeasureSpec);
            int widthSize = MeasureSpec.GetSize(widthMeasureSpec);

            MeasureSpecMode heightMode = MeasureSpec.GetMode(heightMeasureSpec);
            int heightSize = MeasureSpec.GetSize(heightMeasureSpec);

            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        private void SetListViewHeightBasedOnChildren(Android.Widget.ListView listView)
        {
            var adapter = listView.Adapter;
            if (adapter == null)
                return;

            var totalHeight = 0;
            for (var i = 0; i < adapter.Count; i++)
            {
                var item = adapter.GetView(i, null, listView);
                item.Measure(Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
                totalHeight += item.MeasuredHeight;
                Console.WriteLine("Item height = {0}", item.MeasuredHeight);
            }

            Console.WriteLine("Total height = {0}", totalHeight);

            var layoutParams = listView.LayoutParameters;
            layoutParams = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
            layoutParams.Height = totalHeight + (listView.DividerHeight * adapter.Count - 1);

            Console.WriteLine("Total height + dividers = {0}", layoutParams.Height);
            listView.LayoutParameters = layoutParams;
            listView.RequestLayout();
        }

    }

}