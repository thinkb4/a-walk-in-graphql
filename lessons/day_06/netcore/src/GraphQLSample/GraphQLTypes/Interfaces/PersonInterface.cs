using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Enums;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes.Output
{
   public class PersonInterface : InterfaceGraphType<Person>
   {
      public PersonInterface()
      {
         Name = nameof(Person);
         Field(_ => _.Age);
         Field(_ => _.Email);
         Field(_ => _.Id, type: typeof(IdGraphType));
         Field(_ => _.Name);
         Field(_ => _.Surname);
         Field(_ => _.EyeColor, nullable: true, type: typeof(EyeColorType));
         Field<StringGraphType>("fullName");
         Field<ListGraphType<NonNullGraphType<SkillType>>>(
            nameof(Person.Skills),
            arguments: new QueryArguments
            {
               new  QueryArgument<InputSkillType> {  Name = "input" }
            });
         Field<ListGraphType<NonNullGraphType<PersonInterface>>>(
            nameof(Person.Friends),
            arguments: new QueryArguments
            {
               new  QueryArgument<InputPersonType> {  Name = "input" }
            });
         Field<SkillType>(nameof(Person.FavSkill));
      }
   }
}
