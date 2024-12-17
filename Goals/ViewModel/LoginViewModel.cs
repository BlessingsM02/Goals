using System.ComponentModel;
using System.Windows.Input;
using Goals.Model;
using Goals.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Goals.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string feedbackMessage;

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                FeedbackMessage = "Please enter email and password.";
                return;
            }

            var user = await _databaseService.GetUserByEmailAsync(Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                FeedbackMessage = "Invalid email or password.";
                return;
            }

            FeedbackMessage = string.Empty;
            await SecureStorage.SetAsync("loggedInUser", user.Email);
            await App.Current.MainPage.DisplayAlert("Success", $"Welcome back, {user.Name}!", "OK");

            // Navigate to Home Page
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}
