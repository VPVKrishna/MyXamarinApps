using Xamarin.Forms;

namespace FirebaseApp.View
{
    public class CustomEntry : Entry
    {

        public CustomEntry()
        {
            Completed += (sender, e) =>
            {
                OnNext();
            };
        }
        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(nameof(NextEntry), typeof(Xamarin.Forms.View), typeof(Entry));
        public Xamarin.Forms.View NextEntry
        {
            get => (Xamarin.Forms.View)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        public void OnNext()
        {
            NextEntry?.Focus();
        }
    }
}
