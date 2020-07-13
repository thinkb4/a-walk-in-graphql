using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType(ISkillRepository repo)
        {
            Field(_ => _.id);
            Field(_ => _.name);
            Field<SkillType>("parent", resolve: context => repo.Get(context.Source.parentId));
        }
    }
}
