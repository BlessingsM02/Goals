namespace Goals
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        /*protected override async void OnStart()
        {
            var loggedInUser = await SecureStorage.GetAsync("loggedInUser");

            if (!string.IsNullOrEmpty(loggedInUser))
            {
                // Navigate to Home Page
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                // Navigate to Login Page
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }*/
    }
}

