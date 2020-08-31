using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nominations
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; } // ФИО
        public int Nominations { get; set; } // Кол-во наминаций
        public int NominationsWin { get; set; } // выйграных номинаций
        public string Image { get; set; } // ссылка картинки на основную номинацию
    }
}
