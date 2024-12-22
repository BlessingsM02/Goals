using Goals.ViewModel;

namespace Goals.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Trigger the ViewModel to refresh accounts
        if (BindingContext is LoginViewModel homeViewModel)
        {
            homeViewModel.RefreshAccounts();
        }
    }
}