using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class PersonType : ObjectGraphType<Person>
   {
      public PersonType(IPersonRepository personRepo, ISkillRepository skillRepo)
      {
         Field(_ => _.age);
         Field(_ => _.email);
         Field(_ => _.eyeColor);
         Field(_ => _.id);
         Field(_ => _.name);
         Field(_ => _.surname);
         Field<StringGraphType>("fullName", resolve: context => $"{context.Source.name} {context.Source.surname}");
         Field<ListGraphType<SkillType>>("skills", resolve: context => personRepo.GetSkills(context.Source.id));
         Field<ListGraphType<PersonType>>("friends", resolve: context => personRepo.GetFriends(context.Source.id));
         Field<SkillType>("favSkill", resolve: context => skillRepo.Get(context.Source.favSkillId));
      }
   }
}
