using FirebaseApp.View.Blocks;
using System;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FirebaseApp.Model
{
    public class TextModelBlock : BaseNotifier
    {

        public int InputType;

        private bool _isFocused;
        private bool _isEnabled;
        private bool _isVisible;
        private string _text;
        private string _error;
        private string _hint;

        public string Text
        {
            set
            {
                SetPropertyValue(ref _text, value);
            }
            get {
                if(_text == null) {
                    _text = string.Empty;
                 }
                return _text;
            }
        }

        public string ErrorText
        {
            set
            {
                SetPropertyValue(ref _error, value);
            }
            get
            {
                if (_error == null)
                {
                    _error = string.Empty;
                }
                return _error;
            }
        }

        public string Hint
        {
            set
            {
                SetPropertyValue(ref _hint, value);
            }
            get
            {
                if (_hint == null)
                {
                    _hint = string.Empty;
                }
                return _hint;
            }
        }

        public bool IsVisible
        {
            set
            {
                SetPropertyValue(ref _isVisible, value);
            }
            get
            {
                return _isVisible;
            }
        }

        public bool IsEnabled
        {
            set
            {
                SetPropertyValue(ref _isEnabled, value);
            }
            get
            {
                return _isEnabled;
            }
        }

        public bool IsFocused
        {
            set
            {
                SetPropertyValue(ref _isFocused, value);
            }
            get
            {
                return _isFocused;
            }
        }

        public ICommand EntryCompleteCommand { private set; get; }

        public TextModelBlock()
        {
            //For Move to next cursor
            EntryCompleteCommand = new Command((nextView) => {
                if (nextView is null)
                {
                    //Skip this statement on null
                }
                else
                {
                    var next = nextView as Entry;
                    next.Focus();
                }
            });
        }
    }
}
