using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Goals.ViewModel
{
    public partial class TransactionViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Account> accounts; // List of accounts

        [ObservableProperty]
        private Account selectedAccount; // Selected account

        [ObservableProperty]
        private ObservableCollection<string> transactionTypes = new ObservableCollection<string> { "Income", "Expense" };

        [ObservableProperty]
        private ObservableCollection<string> categories = new ObservableCollection<string> { "Food", "Transport", "Entertainment","Bill","Income", "School", "Rental","Others" };

        [ObservableProperty]
        private string selectedTransactionType;

        [ObservableProperty]
        private string selectedCategory;

        [ObservableProperty]
        private string amount;

        [ObservableProperty]
        private string description;

        public IRelayCommand CreateTransactionCommand { get; }

        public TransactionViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            // Initialize commands
            CreateTransactionCommand = new RelayCommand(async () => await SaveTransactionAsync());

            // Load accounts
            LoadAccounts();
        }

        private async void LoadAccounts()
        {
            try
            {
                var email = await SecureStorage.GetAsync("loggedInUser");
                if (string.IsNullOrEmpty(email)) return;

                var user = await _databaseService.GetUserByEmailAsync(email);
                if (user == null) return;

                var userAccounts = await _databaseService.GetUserAccountsAsync(user.Id);
                Accounts = new ObservableCollection<Account>(userAccounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load accounts: {ex.Message}");
            }
        }

        private async Task SaveTransactionAsync()
        {
            try
            {
                // Validate inputs
                if (SelectedAccount == null || string.IsNullOrEmpty(SelectedTransactionType) || string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(SelectedCategory))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                    return;
                }

                if (!double.TryParse(Amount, out double parsedAmount) || parsedAmount <= 0)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please enter a valid amount.", "OK");
                    return;
                }

                // Check if the transaction type is Expense and balance is too low
                if (SelectedTransactionType == "Expense" && SelectedAccount.Balance < parsedAmount)
                {
                    await App.Current.MainPage.DisplayAlert("Insufficient Balance", "Balance too low, can't proceed with the transaction.", "OK");
                    return;
                }

                // Create a new transaction
                var transaction = new Transaction
                {
                    AccountId = SelectedAccount.Id, // Use the selected account ID
                    TransType = SelectedTransactionType,
                    Amount = parsedAmount,
                    Category = SelectedCategory,
                    Description = Description,
                    MadeAt = DateTime.Now
                };

                // Save transaction to the database
                await _databaseService.CreateTransaction(transaction);

                // Update the account balance if the transaction is successful
                if (SelectedTransactionType == "Expense")
                {
                    SelectedAccount.Balance -= parsedAmount; // Deduct from balance
                }
                else if (SelectedTransactionType == "Income")
                {
                    SelectedAccount.Balance += parsedAmount; // Add to balance
                }
                await _databaseService.UpdateAccountAsync(SelectedAccount);

                // Clear form inputs
                SelectedAccount = null;
                SelectedTransactionType = null;
                SelectedCategory = null;
                Amount = null;
                Description = null;

                await App.Current.MainPage.DisplayAlert("Success", "Transaction saved successfully.", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to save transaction: {ex.Message}", "OK");
            }
        }

    }
}
