using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia
{
    public class Team
    {
        [BsonId]
        public string TeamName { get; set; }
        public ObservableCollection<string> Mafia { get; set; }
        public string Boss { get; set; }
        public string Sheriff { get; set; }
        public string Maniac { get; set; }
        public string Doctor { get; set; }
        public ObservableCollection<string> Citizens { get; set; }
    }
}
