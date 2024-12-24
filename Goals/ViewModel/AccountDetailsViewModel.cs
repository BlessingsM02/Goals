using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goals.Model;
using Goals.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Goals.ViewModel
{
    [QueryProperty(nameof(Account), "Account")]
    public partial class AccountDetailsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Account account;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions = new();

        public AccountDetailsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        partial void OnAccountChanged(Account value)
        {
            if (value != null && (Account == null || Account.Id != value.Id))
            {
                _ = LoadDataAsync(value.Id); // Fire-and-forget
            }
        }

        public async void OnNavigatedTo()
        {
            if (Account != null)
            {
                await LoadDataAsync(Account.Id);
            }
        }

        private async Task LoadDataAsync(int accountId)
        {
            // Fetch account and transactions asynchronously
            var account = await _databaseService.GetAccountByIdAsync(accountId);
            var transactionList = await _databaseService.GetTransactionsForAccount(accountId);

            // Update properties on the UI thread
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Account = account;
                Transactions = new ObservableCollection<Transaction>(transactionList);
            });
        }

        [RelayCommand]
        private async Task DeleteAccountAsync()
        {
            if (Account == null) return;

            bool confirmation = await Shell.Current.DisplayAlert(
                "Delete Account",
                "Are you sure you want to delete this account?",
                "Yes",
                "No");

            if (confirmation)
            {
                await _databaseService.DeleteAccountAsync(Account.Id);
                await Shell.Current.GoToAsync("///HomePage");
            }
        }

        [RelayCommand]
        private async Task NavigateBackAsync()
        {
            // Navigate back to the previous page
            await Shell.Current.GoToAsync("///HomePage");
        }
    }
}
