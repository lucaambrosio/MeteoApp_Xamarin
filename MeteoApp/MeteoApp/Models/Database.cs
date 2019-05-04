using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace MeteoApp
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocationsSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Location>().Wait();
        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<Location>> GetItemsAsync()
        {
            return database.Table<Location>().ToListAsync();
        }

        /*
         * Query con query SQL.
         */
        public Task<List<Location>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Location>("SELECT * FROM [Location] WHERE [ID] =?", id);
        }

        /*
         * Query con LINQ.
         */
        public Task<Location> GetItemAsync(int id)
        {
            return database.Table<Location>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Location item)
        {
            if (item.ID == 0) 
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Location item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> InsertItemAsync(Location location)
        {
            return database.InsertAsync(location);
        }
        public Task<int> UpdateItemAsync(Location location)
        {
            return database.UpdateAsync(location);
        }
    }
}
