using System.Linq;
using GraphQL.Types;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
    public class RootQuery:ObjectGraphType
    {
        public RootQuery(ISkillRepository _skillRepository)
        {
            Field<ListGraphType<SkillType>>("skills", resolve: context =>
            {

                return _skillRepository.GetAll();
            });

            Field<SkillType>("randomSkill", resolve: context =>
            {

                return _skillRepository.GetRandom();
            });

            Field<ListGraphType<SkillType>>("filteredSkills",
                arguments: new QueryArguments
                {
                   new  QueryArgument<StringGraphType> {  Name = "name"}
                },
                resolve: context =>
                {
                    string name = context.GetArgument<string>("name");
                    return _skillRepository.GetAll().Where(_ => _.name == name).ToList();
                });
        }
    }
}
