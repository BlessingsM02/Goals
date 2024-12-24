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
        private ObservableCollection<Transaction> _transactions;

        public AccountDetailsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Transactions = new ObservableCollection<Transaction>();
            //LoadData(account.Id);
        }

        public async Task LoadData(int accountId)
        {
            Account = await _databaseService.GetAccountByIdAsync(accountId);
            var transactionList = await _databaseService.GetTransactionsForAccount(Account.Id);
            Transactions = new ObservableCollection<Transaction>(transactionList);
        }
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }
        [RelayCommand]
        private async Task DeleteAccount()
        {
            if (Account != null)
            {
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
        }
       

        /* [RelayCommand]
         private async Task EditAccount()
         {
             // Navigate to the Edit Account page with the current account's details
             await Shell.Current.GoToAsync($"///EditAccountPage", true, new Dictionary<string, object>
             {
                 { "Account", Account }
             });
         }*/
    }
}
