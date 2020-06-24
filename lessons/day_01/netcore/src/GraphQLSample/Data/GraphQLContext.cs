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
      }

      public DbSet<Skill> Skill { get; set; }
      public DbSet<Person> Person { get; set; }

      public void LoadFromJson()
      {
         var dirInfo = new System.IO.DirectoryInfo(@".\");
         try
         {
            var response = (JObject)JsonConvert.DeserializeObject(File.ReadAllText((dirInfo.Parent).Parent.Parent.FullName + @"\datasource\data.json"));
            var persons = JsonConvert.DeserializeObject<List<PersonData>>(response["persons"].ToString());
            var skills = JsonConvert.DeserializeObject<List<SkillData>>(response["skills"].ToString());

            Skill.AddRange(skills.Select(SkillData.ToEntity));
            SaveChanges();

            foreach (var person in persons)
            {
               var entity = PersonData.ToEntity(person);
               entity.skills = Skill.Where(s => person.skills.Contains(s.id)).ToList();
               entity.favSkill = Skill.Find(person.favSkill);
               Person.Add(entity);
            }

            SaveChanges();

            foreach (var person in persons)
            {
               var entity = Person.Find(person.id);
               entity.friends = Person.Where(s => person.friends.Contains(s.id)).ToList();
            }
            
            SaveChanges();

         }
         catch (System.Exception)
         {
         }
      }
   }
}

