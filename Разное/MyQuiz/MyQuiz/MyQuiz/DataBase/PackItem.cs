using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyQuiz
{
    public class PackItem
    {
        readonly SQLiteAsyncConnection _database;

        List<Pack> questions = new List<Pack>
        {
            new Pack{Name = "Пак #1",DateCreate = DateTime.Now },
            new Pack{Name = "Пак #2",DateCreate = DateTime.Now },
            new Pack{Name = "Пак #3",DateCreate = DateTime.Now },
        };
        public PackItem(SQLiteAsyncConnection database)
        {
            _database = database;

            if (_database.Table<Pack>().CountAsync().Result == 0)
            {
                IntializeData(questions);
            }
        }

        private void IntializeData(IEnumerable<Pack> startData)
        {
            _database.InsertAllAsync(startData);
        }

        public Task<List<Pack>> GetItemsAsync()
        {
            return _database.Table<Pack>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Pack item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(Pack item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(Pack item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<Pack> GetItemAsync(int id)
        {
            return _database.Table<Pack>().Where(i => i.IdPack == id).FirstOrDefaultAsync();
        }
    }
}
