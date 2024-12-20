using CommunityToolkit.Mvvm.ComponentModel;
using Goals.Model;

namespace Goals.ViewModel
{
    [QueryProperty(nameof(Account), "Account")]
    public partial class AccountDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Account account;

        // Debugging log
        public AccountDetailsViewModel()
        {
            if (Account == null)
            {
                Console.WriteLine("Account is null in AccountDetailsViewModel!");
            }
        }
    }

}
