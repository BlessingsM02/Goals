using Goals.ViewModel;

namespace Goals.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Trigger the ViewModel to refresh accounts
        if (BindingContext is HomeViewModel homeViewModel)
        {
            homeViewModel.RefreshAccounts();
        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccountPage());
    }

    private async void RecordButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTransactionPage());
    }
}