using Prism.Commands;
using Prism.Navigation;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _enabled;

        public bool Enabled {
            set
            {
                SetProperty(ref _enabled, value);
                SubmitCommand.RaiseCanExecuteChanged();
            }
            get
            {
                return _enabled;
            }
        }

        public DelegateCommand SubmitCommand { private set; get; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Main Page";
            SubmitCommand = new DelegateCommand(SubmitClicked, CanSubmit);
        }

        private bool CanSubmit()
        {
            return Enabled;
        }

        private void SubmitClicked()
        {
            Title = "Clicked Submit";
        }
    }
}
