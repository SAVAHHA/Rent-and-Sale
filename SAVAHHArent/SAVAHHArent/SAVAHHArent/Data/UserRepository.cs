using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using SAVAHHArent.Model;

namespace SAVAHHArent.Data
{
    public class UserRepository
    {
        readonly SQLiteAsyncConnection database;
        public UserRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<UserTable>().Wait();
        }
        public Task<List<UserTable>> GetUsersAsync()
        {
            return database.Table<UserTable>().ToListAsync();
        }

        public Task<int> SaveUserAsync(UserTable user)
        {
            return database.InsertAsync(user);
        }

        public Task<UserTable> GetUserAsync(int id)
        {
            return database.GetAsync<UserTable>(id);
        }

        public Task<int> DeleteUserAsync(int id)
        {
            return database.DeleteAsync<UserTable>(id);
        }

        public Task<int> DeleteAll()
        {
            return database.DeleteAllAsync<UserTable>();
        }

        public Task<int> InsertAsync(UserTable user)
        {
            return database.UpdateAsync(user);
        }

    }
}
