namespace Goals.View;

public partial class GoalPage : ContentPage
{
	public GoalPage()
	{
		InitializeComponent();
	}

    private async void addButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateGoalPage());
    }
}