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
            await Shell.Current.GoToAsync("//GoalsPage");
        }
    }
}
