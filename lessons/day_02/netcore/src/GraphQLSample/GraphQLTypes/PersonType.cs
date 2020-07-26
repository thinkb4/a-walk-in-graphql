using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class PersonType : ObjectGraphType<Person>
   {
      public PersonType(IPersonRepository personRepo, ISkillRepository skillRepo)
      {
         Name = nameof(Person);
         Field(_ => _.Age);
         Field(_ => _.Email);
         Field(_ => _.EyeColor);
         Field(_ => _.Id, type: typeof(IdGraphType));
         Field(_ => _.Name);
         Field(_ => _.Surname);
         Field<StringGraphType>("fullName", resolve: context => $"{context.Source.Name} {context.Source.Surname}");
         Field<ListGraphType<NonNullGraphType<SkillType>>>(nameof(Person.Skills), resolve: context => personRepo.GetSkills(context.Source.Id));
         Field<ListGraphType<NonNullGraphType<PersonType>>>(nameof(Person.Friends), resolve: context => personRepo.GetFriends(context.Source.Id));
         Field<SkillType>(nameof(Person.FavSkill), resolve: context => skillRepo.Get(context.Source.FavSkillId));
      }
   }
}
