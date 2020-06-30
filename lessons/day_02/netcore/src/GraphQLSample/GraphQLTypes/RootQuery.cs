using GraphQL.Types;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootQuery : ObjectGraphType
   {
      private readonly ISkillRepository _skillRepository;
      private readonly IPersonRepository _personRepository;

      public RootQuery(ISkillRepository skillRepository, IPersonRepository personRepository)
      {
         _skillRepository = skillRepository;
         _personRepository = personRepository;

         Field<ListGraphType<SkillType>>("skills", resolve: context => _skillRepository.GetAll());

         Field<SkillType>("randomSkill", resolve: context => _skillRepository.GetRandom());

         Field<SkillType>("filteredSkills",
             arguments: new QueryArguments
             {
                   new  QueryArgument<IntGraphType> {  Name = "id" }
             },
             resolve: context =>
             {
                var id = context.GetArgument<int>("id");
                return _skillRepository.Get(id);
             });

         Field<ListGraphType<PersonType>>("people", resolve: context => _personRepository.GetAll());

         Field<PersonType>("randomPerson", resolve: context => _personRepository.GetRandom());

         Field<PersonType>("filteredPeople",
             arguments: new QueryArguments
             {
                   new  QueryArgument<IntGraphType> {  Name = "id" }
             },
             resolve: context =>
             {
                var id = context.GetArgument<int>("id");
                return _personRepository.Get(id);
             });
      }
   }
}
