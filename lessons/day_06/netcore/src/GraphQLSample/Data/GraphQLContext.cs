using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Contact>();
         modelBuilder.Entity<Candidate>();
         modelBuilder.Entity<Engineer>();
         base.OnModelCreating(modelBuilder);
      }

      public void LoadFromJson()
      {
         var dirInfo = new System.IO.DirectoryInfo(@"../../../datasource/data.json");
         try
         {
            var fileContent = File.ReadAllText(dirInfo.FullName);
            var fileData = JsonConvert.DeserializeObject<FileData>(fileContent);

            Skill.AddRange(fileData.skills.Select(SkillData.ToEntity));
            SaveChanges();

            foreach (var person in fileData.persons)
            {
               var entity = PersonData.ToEntity(person);
               entity.Skills = Skill.Where(s => person.skills.Contains(s.Id.ToString())).ToList();
               if (int.TryParse(person.favSkill, out int parsedValue))
               {
                  entity.FavSkill = Skill.Find(parsedValue);
               }
               Person.Add(entity);
            }

            SaveChanges();

            foreach (var person in fileData.persons)
            {
               var entity = Person.Find(int.Parse(person.id));
               entity.Friends = Person.Where(s => person.friends.Contains(s.Id.ToString())).ToList();
            }

            SaveChanges();

         }
         catch (System.Exception)
         {
         }
      }
   }
}

