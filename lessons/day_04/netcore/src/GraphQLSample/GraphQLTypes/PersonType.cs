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
         Field(_ => _.id);
         Field(_ => _.name);
         Field(_ => _.surname);
         Field(_ => _.eyeColor, nullable: true, type: typeof(EyeColorType));
         Field<StringGraphType>("fullName", resolve: context => $"{context.Source.name} {context.Source.surname}");
         Field<ListGraphType<SkillType>>("skills",
            arguments: new QueryArguments
            {
               new  QueryArgument<InputSkillType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkill>("input");
               return personRepo.GetSkills(context.Source.id, input);
            });
         Field<ListGraphType<PersonType>>("friends",
            arguments: new QueryArguments
            {
               new  QueryArgument<InputPersonType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPerson>("input");
               return personRepo.GetFriends(context.Source.id, input);
            });
         Field<SkillType>("favSkill", resolve: context => skillRepo.Get(InputSkill.FromId(context.Source.favSkillId)));
      }
   }
}
