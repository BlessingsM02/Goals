using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;
using Goals.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Goals.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Account> userAccounts;

        public ICommand NavigateToDetailsCommand { get; }

        public HomeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            // Initialize the command
            NavigateToDetailsCommand = new RelayCommand<Account>(OnNavigateToDetails);

          
            // Load accounts
            LoadAccounts();
        }

        
       

        private async void LoadAccounts()
        {
            var email = await SecureStorage.GetAsync("loggedInUser");
            if (string.IsNullOrEmpty(email)) return;

            var user = await _databaseService.GetUserByEmailAsync(email);
            if (user == null) return;

            // Fetch accounts for the logged-in user
            var accounts = await _databaseService.GetUserAccountsAsync(user.Id);
            UserAccounts = new ObservableCollection<Account>(accounts);
        }

        private async void OnNavigateToDetails(Account selectedAccount)
        {
            if (selectedAccount == null)
            {
                Console.WriteLine("Selected account is null.");
                return;
            }

            try
            {
                // Use absolute route with "///"
                await Shell.Current.GoToAsync($"///AccountDetailsPage", true, new Dictionary<string, object>
        {
            { "Account", selectedAccount }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation failed: {ex.Message}");
            }
        }
    }

}
