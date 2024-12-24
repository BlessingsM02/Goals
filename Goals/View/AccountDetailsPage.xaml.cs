using Goals.Service;
using Goals.ViewModel;

namespace Goals.View
{
    public partial class AccountDetailsPage : ContentPage
    {
        
        public AccountDetailsPage()
        {
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Trigger the ViewModel to refresh accounts
            if (BindingContext is AccountDetailsViewModel accountDetailsViewModel)
            {
                accountDetailsViewModel.OnNavigatedTo(); 
            }
        }

        
    }
}
