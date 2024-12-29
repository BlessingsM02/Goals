using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Goals.ViewModel
{
    public partial class GoalViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Goal> Goals { get; }

        [ObservableProperty]
        private Goal newGoal;

        [ObservableProperty]
        private bool isLoading;
        public GoalViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Goals = new ObservableCollection<Goal>();
            NewGoal = new Goal
            {
                Deadline = DateTime.Now.AddMonths(1),
                CreatedAT = DateTime.Now,
                CurrentAmount = 0,
                IsAchieved = false
            };

            LoadGoalsCommand.Execute(null);
        }
        [RelayCommand]
        public async Task LoadGoals()
        {
            try
            {
                IsLoading = true;

                // Clear the collection to avoid duplication
                Goals.Clear();

                // Fetch goals from the database
                var goalsList = await _databaseService.GetGoalsAsync();

                // Populate the ObservableCollection
                foreach (var goal in goalsList)
                {
                    Goals.Add(goal);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load goals: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task SaveGoal()
        {
            if (string.IsNullOrEmpty(NewGoal.GoalName) || NewGoal.GoalAmount <= 0 || NewGoal.Deadline <= DateTime.Now)
            {
                await Shell.Current.DisplayAlert("Invalid Input", "Please fill in all fields with valid data.", "OK");
                return;
            }

            NewGoal.CreatedAT = DateTime.Now;

            await _databaseService.SaveGoalAsync(NewGoal);

            Goals.Add(NewGoal);
            NewGoal = new Goal
            {
                Deadline = DateTime.Now.AddMonths(1),
                CreatedAT = DateTime.Now,
                CurrentAmount = 0,
                IsAchieved = false
            };

            await Shell.Current.GoToAsync("//GoalPage");
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("//GoalPage");
        }

      
        [RelayCommand]
        private async Task NavigateToGoalDetail(Goal selectedGoal)
        {
            if (selectedGoal == null)
                return;
            try
            {
                await Shell.Current.GoToAsync("///GoalDetailPage", true, new Dictionary<string, object>
                {
                    { "Goal", selectedGoal }
                });
            }
            catch (Exception ex)
            {
                //Console.WriteLine($" Error {ex}");
                await Shell.Current.DisplayAlert("Erro", $"Massage: {ex}", "ok");
            }
            
        }
    }
}
