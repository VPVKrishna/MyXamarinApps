using FirebaseApp.Repository;
using FirebaseApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace FirebaseApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookStorePage : ContentPage
    {

        #region public variables
        public BookViewModel ViewModel { private set; get; }
        #endregion

        public BookStorePage(IRepository repository)
        {
            BindingContext = ViewModel = new BookViewModel(repository);
            InitializeComponent();
        }

        /**

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            _viewModel.AddBlocksSetup();
        }

        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            _viewModel.UpdateBlocksSetup();
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            _viewModel.DeleteBlocksSetup();
        }

        private void BtnGet_Clicked(object sender, EventArgs e)
        {
            _viewModel.GetBlocksSetup();
        }

        private void BtnShowList_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnSumit_Clicked(object sender, EventArgs e)
        {
            _viewModel.PerforOperation();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var operation = picker.Items[selectedIndex];
                _viewModel.SelectedPickerItem = operation;
                _viewModel.SetUIBlocks();
            }
        }
       **/
    }
}