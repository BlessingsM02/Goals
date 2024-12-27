
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Service;
using Goals.View;

namespace Goals.ViewModel
{
    public partial class PlanViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public PlanViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task NavigateToAnalytics()
        {
            //await Shell.Current.GoToAsync("//AnalyticsPage");
        }

        [RelayCommand]
        private async Task NavigateToBudget()
        {
            //await Shell.Current.GoToAsync("//BudgetPage");
        }

        [RelayCommand]
        private async Task NavigateToYearPlan()
        {
            //await Shell.Current.GoToAsync("//YearPlanPage");
        }

        [RelayCommand]
        private async Task NavigateToGoals()
        {
            await Shell.Current.GoToAsync("//GoalPage");
            //await Navigation.PushAsync(new GoalPage());
        }
    }
}
