using System.Linq;
using GraphQL.Types;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
    public class RootQuery:ObjectGraphType
    {
        private readonly ISkillRepository _skillRepository;
        public RootQuery(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
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
                   new  QueryArgument<StringGraphType> {  Name = "id"}
                },
                resolve: context =>
                {
                    //string name = context.GetArgument<string>("name");
                    string id = context.GetArgument<string>("id");
                    return _skillRepository.GetAll().Where(_ => _.id == id).ToList();
                });
        }
    }
}
