using Android.Content;
using Android.Graphics;
using LocaleAppTest;
using LocaleAppTest.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineView), typeof(LineViewRenderer))]
namespace LocaleAppTest.Droid
{
    public class LineViewRenderer : ViewRenderer<LineView, Android.Views.View>
    {
        private Paint circlePaint = new Paint();
        private Paint rectPaint = new Paint();
        private RectF Rec = new RectF(130, 130, 290, 290);

        public LineViewRenderer(Context context) : base(context)
        {
            circlePaint.Color = Android.Graphics.Color.GreenYellow;
            circlePaint.AntiAlias = true;

            rectPaint.Color = Android.Graphics.Color.Tomato;
            rectPaint.AntiAlias = true;

            this.SetWillNotDraw(false);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == LineView.WidthProperty.PropertyName) //Tried with multiple of DrawRect properties.
                this.Invalidate(); // Force a call to OnDraw

            LineView drawSender = ((LineView)sender);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.DrawCircle(cx: 40, cy: 40, radius: 55, paint: circlePaint);
            canvas.DrawRect(Rec, rectPaint);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension(150, 150);
        }
    }
}