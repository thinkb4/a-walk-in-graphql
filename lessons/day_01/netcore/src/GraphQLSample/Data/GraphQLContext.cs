using GraphQLNetCore.Data;
using GraphQLNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        private void LoadFromJson()
        {
            try
            {

                var response = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(@".\datasource\Persons.json"));
                foreach(var person in response)
                {
                    Person.Add(person);
                    Person.Where(p => p.id == person.id).FirstOrDefault()?.skills?
                        .AddRange(person.skills);
                    Person.Where(p => p.id == person.id).FirstOrDefault()?.friends?
                       .AddRange(person.friends);
                    SaveChanges();
                }
                Skill.AddRange(JsonConvert.DeserializeObject<List<Skill>>(File.ReadAllText(@".\datasource\Skills.json")));
                SaveChanges();

            }
            catch (System.Exception)
            {
            }
        }
    }
}

