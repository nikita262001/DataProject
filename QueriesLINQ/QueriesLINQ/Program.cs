using System;
using System.Collections.Generic;
using System.Linq;

namespace QueriesLINQ
{
    class Program
    {
        static List<string> list = new List<string> { "Test111", "Test222", "Test333", "Test444", "Test555" };

        static List<Person> people = new List<Person> {
            new Person { Name = "Рустем" , Surname = "Исламов" , Age = 10},
            new Person { Name = "Никита" , Surname = "Волков" , Age = 20},
            new Person { Name = "Илья" , Surname = "Ермолаев" , Age = 30},
            new Person { Name = "Эмиль" , Surname = "Тагиев" , Age = 40},
            new Person { Name = "Амир" , Surname = "Сафеев" , Age = 50},
            new Person { Name = "Булат" , Surname = "Рахматуллин" , Age = 60},
            new Person { Name = "Даниэль" , Surname = "Малиновский" , Age = 70},
            new Person { Name = "Булат" , Surname = "Нигматуллин" , Age = 80},
            new Person { Name = "Никита" , Surname = "Кормаков" , Age = 90},
            new Person { Name = "Никита" , Surname = "Тестеров" , Age = 100},
        };      
        static void Main(string[] args)
        {
            /*people.Select();
            Enumerable.Select(people);*/

            #region select
            //Пример №1
            /*IEnumerable<Person> peopleName = from person in people
                                             select person;

            foreach (Person item in peopleName)
            {
                Console.WriteLine(item.ToString());
            }*/

            //Пример №2
            /*IEnumerable<string> peopleName = from person in people
                                             select person.Name;

            foreach (string item in peopleName)
            {
                Console.WriteLine(item.ToString());
            }*/

            //Пример №3
            /*var peopleName = people.Select(element => element.Name);

            foreach (var item in peopleName)
            {
                Console.WriteLine(item);
            }*/


            //Пример №4
            /*foreach (var item in people.Select(element => element.Name))
            {
                Console.WriteLine(item);
            }*/

            //Пример №5
            /*List<string> results1 = people.Select((person) => person.Name).ToList();

            List<string> results2 = (from person in people
                                     select person.Name).ToList();*/

            //Пример №6
            /*List<Person> results1 = people.Select((person) => person).ToList();

            int a = results1.Max((person) => person.Age);*/
            #endregion

            #region selectWhere

            /*Person person1 = new Person { Name = "Рустем", Surname = "Исламов", Age = 10 };

            IEnumerable<Person> peopleName1 = from person in people
                                              where person.Equals("Никита")
                                              select person;

            IEnumerable<Person> peopleName2 = from person in people
                                              where person.Name == "Никита"
                                              select person;
            IEnumerable<Person> peopleName3 = from person in people
                                              where person.Name == person1.Name && person.Surname == person1.Surname && person.Age == person1.Age
                                              //where person.Equals(person1)
                                              //where person.Name == "Никита" //.Equals("Никита")
                                              select person;
            IEnumerable<Person> peopleName4 = from person in people
                                              where person.Name == person1.Name && person.Surname == person1.Surname && person.Age == person1.Age
                                              //where person.Equals(person1)
                                              //where person.Name == "Никита" //.Equals("Никита")
                                              select person;

            foreach (Person item in peopleName1)
            {
                Console.WriteLine(item.ToString());
            }*/


            #endregion

            #region OrderBy

            /*var orderBy = from person in people
                          orderby person.Name //descending
                          select person;

            //var orderBy = people.OrderByDescending((person) => person.Name);

            foreach (var item in orderBy)
            {
                Console.WriteLine(item.ToString());
            }*/ 

            #endregion

            #region Group

            IEnumerable<IGrouping<string, Person>> peopleGroups1 = from person in people
                               group person by person.Name;

            IEnumerable<IGrouping<string, Person>> peopleGroups2 = from person in people
                                                                   orderby person.Age
                                                                   group person by person.Name into groupKeys
                                                                   orderby groupKeys.Key
                                                                   select groupKeys;

            var peopleGroups3 = from person in people
                                group person by person.Name into groupKeys
                                select new
                                { //анонимный класс
                                    Key = groupKeys.Key,
                                    Elements = groupKeys.OrderBy((elem)=> elem.Age),
                                };

            var peopleGroups4 = from person in people
                                group person by person.Name into groupKeys
                                select new
                                { //анонимный класс
                                    Key = groupKeys.Key,
                                    Elements = from gr in groupKeys
                                               orderby gr.Age
                                               select gr,
                                };


            foreach (IGrouping<string, Person> item in peopleGroups2)
            {
                Console.WriteLine(item.Key + ": ");
                
                foreach (var itemPerson in item)
                {
                    Console.WriteLine($"\t {itemPerson.Age}");
                }
            }

            #endregion

            #region Join - объединение



            #endregion
        }
    }
}
