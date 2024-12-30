
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Service;

namespace Goals.ViewModel
{
    public partial class PlanViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        [ObservableProperty]
        private float overallGoalProgress;

        [ObservableProperty]
        private string overallGoalPercentageText;
        public PlanViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadGoalsData();
        }

        [RelayCommand]
        private async Task NavigateToAnalytics()
        {
            //await Shell.Current.GoToAsync("//AnalyticsPage");
        }

        [RelayCommand]
        private async Task NavigateToBudget()
        {
            await Shell.Current.GoToAsync("//BudgetPage");
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
        }
        private async Task LoadGoalsData()
        {
            var goals = await _databaseService.GetGoalsAsync();

            if (goals.Any())
            {
                var totalGoalAmount = goals.Sum(g => g.GoalAmount);
                var totalCurrentAmount = goals.Sum(g => g.CurrentAmount);

                OverallGoalProgress = totalGoalAmount > 0 ? (float)(totalCurrentAmount / totalGoalAmount) : 0;
                OverallGoalPercentageText = $"{(int)(OverallGoalProgress * 100)}%";
            }
            else
            {
                OverallGoalProgress = 0;
                OverallGoalPercentageText = "0%";
            }
        }
    }
}
