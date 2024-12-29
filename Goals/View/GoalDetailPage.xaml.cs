using Goals.Model;
using Goals.Service;

namespace Goals.View
{
    [QueryProperty(nameof(Goal), "Goal")]
    public partial class GoalDetailPage : ContentPage
    {
        private Goal _goal;

        public Goal Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                OnPropertyChanged(nameof(Goal));
                BindingContext = _goal; // Set the BindingContext to the Goal
            }
        }

        public GoalDetailPage()
        {
            InitializeComponent();
        }

     /*   private async void BackButton_Clicked(object sender, EventArgs e)
         {
             await Shell.Current.GoToAsync("//..");
         }

           private async void EditButton_Clicked(object sender, EventArgs e)
           {
               if (_goal != null)
               {
                   await Shell.Current.GoToAsync($"///EditGoalPage", true, new Dictionary<string, object>
                   {
                       { "Goal", _goal }
                   });
               }
           }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (_goal != null)
            {
                bool confirmation = await DisplayAlert(
                    "Delete Goal",
                    "Are you sure you want to delete this goal?",
                    "Yes",
                    "No");

                if (confirmation)
                {
                    await _databaseService.DeleteGoalAsync(_goal.Id);
                    await Shell.Current.GoToAsync("///GoalPage"); // Navigate back to the goals list
                }
            }
        }*/
    }
}
