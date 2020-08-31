using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyQuiz
{
    public class AnswerItem
    {
        readonly SQLiteAsyncConnection _database;

        List<Answer> answers = new List<Answer>
        {
            new Answer{IdQuestion = 1, Name = "0(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 1, Name = "0(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 1, Name = "0(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 2, Name = "1(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 2, Name = "1(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 2, Name = "1(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 3, Name = "2(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 3, Name = "2(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 3, Name = "2(Вопрос) Ответ #3" ,CorrectAnswer = false},

            new Answer{IdQuestion = 4, Name = "4(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 4, Name = "4(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 4, Name = "4(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 5, Name = "5(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 5, Name = "5(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 5, Name = "5(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 6, Name = "6(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 6, Name = "6(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 6, Name = "6(Вопрос) Ответ #3" ,CorrectAnswer = false},

            new Answer{IdQuestion = 7, Name = "7(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 7, Name = "7(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 7, Name = "7(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 8, Name = "8(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 8, Name = "8(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 8, Name = "8(Вопрос) Ответ #3" ,CorrectAnswer = false},
            new Answer{IdQuestion = 9, Name = "9(Вопрос) Ответ #1" ,CorrectAnswer = true},
            new Answer{IdQuestion = 9, Name = "9(Вопрос) Ответ #2" ,CorrectAnswer = false},
            new Answer{IdQuestion = 9, Name = "9(Вопрос) Ответ #3" ,CorrectAnswer = false},
        };

        public AnswerItem(SQLiteAsyncConnection database)
        {
            _database = database;

            if (_database.Table<Answer>().CountAsync().Result == 0)
            {
                IntializeData(answers);
            }
        }

        private void IntializeData(IEnumerable<Answer> startData)
        {
            _database.InsertAllAsync(startData);
        }
        public Task<List<Answer>> GetItemsAsync()
        {
            return _database.Table<Answer>().ToListAsync();
        }
        public Task<int> SaveItemAsync(Answer item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> EditItemAsync(Answer item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> DeleteItemAsync(Answer item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<Answer> GetItemAsync(int id)
        {
            return _database.Table<Answer>().Where(i => i.IdАnswer == id).FirstOrDefaultAsync();
        }
    }
}
