using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Enums;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes.Output
{
   public class CandidateType : ObjectGraphType<Candidate>
   {
      public CandidateType(IPersonRepository personRepo, ISkillRepository skillRepo)
      {
         Interface<PersonInterface>();
         IsTypeOf = obj => obj is Candidate;

         Name = nameof(Candidate);
         Field(_ => _.Age);
         Field(_ => _.Email);
         Field(_ => _.Id, type: typeof(IdGraphType));
         Field(_ => _.Name);
         Field(_ => _.Surname);
         Field(_ => _.TargetGrade, type: typeof(GradeType));
         Field(_ => _.TargetRole, type: typeof(RoleType));
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
         Field<ListGraphType<NonNullGraphType<PersonInterface>>>(
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
