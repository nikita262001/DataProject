using System;
using System.Collections.Generic;
using System.Text;

namespace QueriesLINQ
{
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        enum PersonClass
        {
            Student,
            School,
            Norm,
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Фамилия: {Surname}, Возраст: {Age}";
        }

        /*public override bool Equals(object obj)
        {
            Person person = obj as Person;

            return this.Name == person.Name &&  this.Surname == person.Surname && this.Age == person.Age;
        }*/
    }
}
