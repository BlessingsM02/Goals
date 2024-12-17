using Goals.Model;

namespace Goals.View;

public partial class AccountDetailsPage : ContentPage
{
    public AccountDetailsPage(Account account)
    {
        InitializeComponent();
        BindingContext = account; // Pass the account data to the page
    }
}
