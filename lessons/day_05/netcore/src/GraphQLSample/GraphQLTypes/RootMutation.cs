using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootMutation : ObjectGraphType
   {
      private readonly ISkillRepository _skillRepository;

      public RootMutation(ISkillRepository skillRepository)
      {
         _skillRepository = skillRepository;

         Name = "Mutation";
         Field<NonNullGraphType<SkillType>>("createSkill",
            arguments: new QueryArguments
            {
               new  QueryArgument<NonNullGraphType<InputSkillCreateType>> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkillCreate>("input");
               return _skillRepository.CreateSkill(input);
            });
      }
   }
}
