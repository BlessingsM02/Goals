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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}
