using Goals.Model;
using Goals.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Goals.ViewModel
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string feedbackMessage;

        public IRelayCommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public RegistrationViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
        }

        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                FeedbackMessage = "All fields are required.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                FeedbackMessage = "Passwords do not match.";
                return;
            }

            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = Password
               // CreatedDate = DateTime.UtcNow
            };

            var result = await _databaseService.RegisterUserAsync(newUser);

            if (result)
            {
                FeedbackMessage = string.Empty;
                await App.Current.MainPage.DisplayAlert("Success", "Registration Successful!", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                FeedbackMessage = "A user with this email already exists.";
            }
        }

        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
