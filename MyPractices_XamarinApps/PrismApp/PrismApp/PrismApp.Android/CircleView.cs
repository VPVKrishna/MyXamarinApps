using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace PrismApp.Droid
{
    public class CircleView : View
    {
        private Paint CirclePaint = new Paint();
        private Paint LinePaint = new Paint();

        public CircleView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public CircleView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
            CirclePaint.Color = Color.Blue;

            LinePaint.Color = Color.Red;
            LinePaint.StrokeWidth = 18f;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawCircle(100f, 100f, 40f, CirclePaint);
            canvas.DrawLine(150f, 150f, 200f, 150f, LinePaint);
        }
    }
}