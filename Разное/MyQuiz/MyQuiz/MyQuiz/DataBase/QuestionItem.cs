using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyQuiz
{
    public class QuestionItem
    {
        readonly SQLiteAsyncConnection _database;

        List<Question> questions = new List<Question> 
        {
            new Question{IdPack = 1, Name = "0 Вопрос #1" },
            new Question{IdPack = 1, Name = "0 Вопрос #2" },
            new Question{IdPack = 1, Name = "0 Вопрос #3" },
            new Question{IdPack = 2, Name = "1 Вопрос #1" },
            new Question{IdPack = 2, Name = "1 Вопрос #2" },
            new Question{IdPack = 2, Name = "1 Вопрос #3" },
            new Question{IdPack = 3, Name = "2 Вопрос #1" },
            new Question{IdPack = 3, Name = "2 Вопрос #2" },
            new Question{IdPack = 3, Name = "2 Вопрос #3" },
        };
        public QuestionItem(SQLiteAsyncConnection database)
        {
            _database = database;

            if (_database.Table<Question>().CountAsync().Result == 0)
            {
                IntializeData(questions);
            }
        }

        private void IntializeData(IEnumerable<Question> startData)
        {
            _database.InsertAllAsync(startData);
        }

        public Task<List<Question>> GetItemsAsync()
        {
            return _database.Table<Question>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Question item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(Question item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(Question item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<Question> GetItemAsync(int id)
        {
            return _database.Table<Question>().Where(i => i.IdQuestion == id).FirstOrDefaultAsync();
        }
    }
}
