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
            _database.CreateTableAsync<Transaction>().Wait();
            _database.CreateTableAsync<Goal>().Wait();
        }
        
        //User
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


        //account
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

        public async Task DeleteAccountAsync(int accountId)
        {
            var accountToDelete = await _database.Table<Account>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
            if (accountToDelete != null)
            {
                await _database.DeleteAsync(accountToDelete);
            }
        }

        public Task<List<Account>> GetUserAccountsAsync(int userId)
        {
            // Fetch accounts that belong to the specified UserId
            return _database.Table<Account>()
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
        }

        
        //transaction
        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            await _database.InsertAsync(transaction);
            return true;
        }
        public async Task UpdateAccountAsync(Account account)
        {
            await _database.UpdateAsync(account);
        }
        public async Task<List<Transaction>> GetTransactionsForAccount(int accountId)
        {
            return await _database.Table<Transaction>()
                                   .Where(t => t.AccountId == accountId)
                                   .ToListAsync();
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _database.Table<Account>().FirstOrDefaultAsync(a => a.Id == accountId);
        }


        //Goal
        public async Task SaveGoalAsync(Goal goal)
        {
            await _database.InsertAsync(goal);
        }

        public async Task<List<Goal>> GetGoalsAsync()
        {
            return await _database.Table<Goal>().ToListAsync();
        }
        public Task<Goal> GetGoalByIdAsync(int goalId)
        {
            return _database.Table<Goal>().FirstOrDefaultAsync(g => g.Id == goalId);
        }

        // Delete goal by ID
        public Task<int> DeleteGoalAsync(int goalId)
        {
            return _database.Table<Goal>().DeleteAsync(g => g.Id == goalId);
        }

    }
}
