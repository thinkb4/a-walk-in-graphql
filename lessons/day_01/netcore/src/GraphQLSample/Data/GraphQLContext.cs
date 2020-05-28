using GraphQLNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphQLNetCore.Data
{
    public class GraphQLContext : DbContext
    {
        public GraphQLContext(DbContextOptions<GraphQLContext> options) : base(options)
        {
            LoadFromJson();
        }

        public DbSet<Skill> Skill { get; set; }
        public DbSet<Person> Person { get; set; }

        public void LoadFromJson()
        {
            var dirInfo = new System.IO.DirectoryInfo(@".\");
            try
            {
                var response = (JObject)JsonConvert.DeserializeObject(File.ReadAllText((dirInfo.Parent).Parent.Parent.FullName + @"\datasource\data.json"));
                var persons  = JsonConvert.DeserializeObject<List<Person>>(response["persons"].ToString());
                var skills = JsonConvert.DeserializeObject<List<Skill>>(response["skills"].ToString());
                foreach (var person in persons)
                {
                    Person.Add(person);
                    Person.Where(p => p.id == person.id).FirstOrDefault()?.skills?
                        .AddRange(person.skills);
                    Person.Where(p => p.id == person.id).FirstOrDefault()?.friends?
                       .AddRange(person.friends);
                    SaveChanges();
                }
                Skill.AddRange(skills);
                SaveChanges();
            }
            catch (System.Exception)
            {
            }
        }
    }
}

