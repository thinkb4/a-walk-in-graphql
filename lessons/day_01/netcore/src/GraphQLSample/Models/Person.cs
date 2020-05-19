using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLNetCore.Models
{
    public class Person
    {
        public int id { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        [NotMapped]
        public List<int> friends { get; set; }
        [NotMapped]
        public List<int> skills { get; set; }
        public int? favSkill { get; set; }
    }
}
