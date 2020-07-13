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
         Field<EyeColorType>("eyeColor");
         Field<StringGraphType>("fullName", resolve: context => $"{context.Source.name} {context.Source.surname}");
         Field<ListGraphType<SkillType>>("skills",
            arguments: new QueryArguments
            {
                   new  QueryArgument<IntGraphType> {  Name = "id" }
            },
            resolve: context =>
            {
               var id = context.GetArgument<int?>("id");
               return personRepo.GetSkills(context.Source.id, id);
            });
         Field<ListGraphType<PersonType>>("friends",
            arguments: new QueryArguments
            {
                   new  QueryArgument<IntGraphType> {  Name = "id" }
            },
            resolve: context =>
            {
               var id = context.GetArgument<int?>("id");
               return personRepo.GetFriends(context.Source.id, id);
            });
         Field<SkillType>("favSkill", resolve: context => skillRepo.Get(context.Source.favSkillId));
      }
   }
}
