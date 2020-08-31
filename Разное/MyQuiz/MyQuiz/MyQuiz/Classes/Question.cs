using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyQuiz
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int IdQuestion { get; set; }
        public string Name { get; set; }
        public int IdPack { get; set; }
    }
}
