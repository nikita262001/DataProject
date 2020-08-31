using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSQLConnect
{
    [Table(Name = "Person")]
    public class Person
    {
        [Column(Name = "IdPerson", IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdPerson { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Fam")]
        public string Fam { get; set; }

        [Column(Name = "Otch")]
        public string Otch { get; set; }
    }
}