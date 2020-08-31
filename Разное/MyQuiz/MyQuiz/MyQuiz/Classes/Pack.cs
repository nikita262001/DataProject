using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyQuiz
{
    public class Pack
    {
        [PrimaryKey, AutoIncrement]
        public int IdPack { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
