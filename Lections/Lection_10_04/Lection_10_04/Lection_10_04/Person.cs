using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lection_10_04
{
    [Table("People")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string FavouriteColor { get; set; }
    }
}
