using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;

namespace Goals.ViewModel
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string accountName;

        [ObservableProperty]
        private double initialBalance;
        
        [ObservableProperty]
        private string accountType;



        public IRelayCommand CreateAccountCommand { get; }

        public CreateAccountViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            CreateAccountCommand = new AsyncRelayCommand(CreateAccountAsync);
        }

        private async Task CreateAccountAsync()
        {
            var email = await SecureStorage.GetAsync("loggedInUser");
            var user = await _databaseService.GetUserByEmailAsync(email);

            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            var newAccount = new Account
            {
                AccountName = AccountName,
                AccountType = AccountType,
                Balance = InitialBalance,
                UserId = user.Id
            };

            var success = await _databaseService.CreateAccountAsync(newAccount);

            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
                //AccountName = string.Empty;
               // InitialBalance = 0;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Limit Reached", "You can only create a maximum of 4 accounts.", "OK");
            }
        }
    }
}
