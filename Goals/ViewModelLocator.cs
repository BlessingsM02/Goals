using Goals.Service;
using Goals.ViewModel;

namespace Goals
{
    public static class ViewModelLocator
    {
        private static DatabaseService _databaseService = new DatabaseService(FileAccessHelper.GetLocalFilePath("goals.db3"));

        public static LoginViewModel LoginViewModel => new LoginViewModel(_databaseService);
        public static RegistrationViewModel RegistrationViewModel => new RegistrationViewModel(_databaseService);
        public static ProfileViewModel ProfileViewModel => new ProfileViewModel(_databaseService);
        public static CreateAccountViewModel CreateAccountViewModel => new CreateAccountViewModel(_databaseService);
        public static HomeViewModel HomeViewModel => new HomeViewModel(_databaseService);
        public static AccountDetailsViewModel AccountDetailsViewModel => new AccountDetailsViewModel(_databaseService);
        public static TransactionViewModel TransactionViewModel => new TransactionViewModel(_databaseService);

    }
}
