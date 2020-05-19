using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLNetCore.Models
{
    public class Skill
    {
        public int id { get; set; }
        public int? parent { get; set; }
        public string name { get; set; }
    }
}
