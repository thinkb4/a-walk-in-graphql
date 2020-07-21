using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Enums;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes.Output
{
   public class PersonType : ObjectGraphType<Person>
   {
      public PersonType(IPersonRepository personRepo, ISkillRepository skillRepo)
      {
         Name = nameof(Person);
         Field(_ => _.Age);
         Field(_ => _.Email);
         Field(_ => _.Id, type: typeof(IdGraphType));
         Field(_ => _.Name);
         Field(_ => _.Surname);
         Field(_ => _.EyeColor, nullable: true, type: typeof(EyeColorType));
         Field<StringGraphType>("fullName", resolve: context => $"{context.Source.Name} {context.Source.Surname}");
         Field<ListGraphType<NonNullGraphType<SkillType>>>(
            nameof(Person.Skills),
            arguments: new QueryArguments
            {
               new  QueryArgument<InputSkillType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkill>("input");
               return personRepo.GetSkills(context.Source.Id, input);
            });
         Field<ListGraphType<NonNullGraphType<PersonType>>>(
            nameof(Person.Friends),
            arguments: new QueryArguments
            {
               new  QueryArgument<InputPersonType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPerson>("input");
               return personRepo.GetFriends(context.Source.Id, input);
            });
         Field<SkillType>(
            nameof(Person.FavSkill),
            resolve: context => skillRepo.Get(InputSkill.FromId(context.Source.FavSkillId))
            );
      }
   }
}
