namespace Goals.View;

public partial class BudgetPage : ContentPage
{
	public BudgetPage()
	{
		InitializeComponent();
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///PlanPage");
    }

    private async void addButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateBudgetPage());
    }
}