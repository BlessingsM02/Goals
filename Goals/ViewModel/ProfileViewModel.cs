using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using Goals.Model;
using Goals.Service;

namespace Goals.ViewModel
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        public User currentUser;

        public IRelayCommand LogoutCommand { get; }

        public ProfileViewModel(DatabaseService databaseService)
        {
            LoadUserDetails();
            _databaseService = databaseService;
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);

            
        }

        private async void LoadUserDetails()
        {
            var email = await SecureStorage.GetAsync("loggedInUser");
            if (!string.IsNullOrEmpty(email))
            {
                // Fetch user details from the database
                CurrentUser = await _databaseService.GetUserByEmailAsync(email);
            }
        }

        private async Task LogoutAsync()
        {
            // Clear SecureStorage
            SecureStorage.Remove("loggedInUser");

            // Navigate to Login Page
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
