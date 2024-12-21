using SQLite;
using Goals.Model;

namespace Goals.Service
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Account>().Wait();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<bool> RegisterUserAsync(User user)
        {
            // Check if user already exists by email
            var existingUser = await _database.Table<User>()
                                              .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
                return false; // User already exists

            // Hash the password before saving
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Insert new user
            await _database.InsertAsync(user);
            return true;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _database.Table<User>()
                                  .FirstOrDefaultAsync(u => u.Email == email);
        }



        public async Task<int> GetAccountCountForUserAsync(int userId)
        {
            return await _database.Table<Account>()
                                  .Where(a => a.UserId == userId)
                                  .CountAsync();
        }



        public async Task<bool> CreateAccountAsync(Account account)
        {
            var count = await GetAccountCountForUserAsync(account.UserId);
            if (count >= 4)
            {
                return false; // User already has 4 accounts
            }

            await _database.InsertAsync(account);
            return true;
        }


        public Task<List<Account>> GetUserAccountsAsync(int userId)
        {
            // Fetch accounts that belong to the specified UserId
            return _database.Table<Account>()
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
        }




    }
}
