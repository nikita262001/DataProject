using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyQuiz
{
    public class Answer
    {
        [PrimaryKey, AutoIncrement]
        public int IdАnswer { get; set; }
        public string Name { get; set; }
        public int IdQuestion { get; set; }
        public bool CorrectAnswer { get; set; } // правильный ответ
    }
}
