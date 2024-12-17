using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;
using System.Collections.ObjectModel;

namespace Goals.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Account> userAccounts;

        public HomeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            // Load user accounts
            LoadAccounts();
        }

        private async void LoadAccounts()
        {
            var loggedInUserId = Preferences.Get("LoggedInUserId", 0);

            // Fetch accounts for the logged-in user
            var email = await SecureStorage.GetAsync("loggedInUser");
            var user = await _databaseService.GetUserByEmailAsync(email);

            var accounts = await _databaseService.GetUserAccountsAsync(user.Id);
            UserAccounts = new ObservableCollection<Account>(accounts);
        }
    }
}

