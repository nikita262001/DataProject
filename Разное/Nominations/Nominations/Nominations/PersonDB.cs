using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Nominations
{
    public class PersonDB // класс для работы с базой данных
    {
        private List<Person> _startData = new List<Person>
        {
            new Person{ Image = Device.RuntimePlatform == Device.Android ? "BiznesMan.jpg" :"Images/BiznesMan.jpg", Name = "Спенсер Трэйси" , Nominations = 9,NominationsWin = 2 },
            new Person{ Image = Device.RuntimePlatform == Device.Android ? "Lul.jpg" :"Images/Lul.jpg", Name = "Джек Николсон" , Nominations = 8,NominationsWin = 2 },
            new Person{ Image = Device.RuntimePlatform == Device.Android ? "Master.jpg" :"Images/Master.jpg", Name = "Лоуренс Оливье" , Nominations = 9,NominationsWin = 1 },
            new Person{ Image = Device.RuntimePlatform == Device.Android ? "Oskar.jpg" :"Images/Oskar.jpg", Name = "Дастин Хоффман" , Nominations = 7,NominationsWin = 2 },
            new Person{ Image = Device.RuntimePlatform == Device.Android ? "Sotrydnik.jpg" :"Images/Sotrydnik.jpg", Name = "Дэниэл Дэй-Льюис" , Nominations = 6,NominationsWin = 3 },
        }; // начальные данные

        readonly SQLiteConnection _database;
        public PersonDB(string dbPath)
        {
            _database = new SQLiteConnection(dbPath); // подключается и не создается если существует
            _database.CreateTable<Person>(); // не создается если существует

            if (_database.Table<Person>().Count() == 0) // загрузка начальных данных если 0 элементов в таблице Person
            {
                IntializeData(_startData);
            }
        }

        private void IntializeData(IEnumerable<Person> startData)
        {
            _database.InsertAll(startData);
        }

        public List<Person> GetItems()
        {
            return _database.Table<Person>().ToList();
        }
        public int SaveItem(Person item)
        {
            return _database.Insert(item);
        }
        public int EditItem(Person item)
        {
            return _database.Update(item);
        }
        public int DeleteItem(Person item)
        {
            return _database.Delete(item);
        }
    }
}
