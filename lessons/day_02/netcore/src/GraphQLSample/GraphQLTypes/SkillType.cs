using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType(ISkillRepository repo)
        {
            Name = "Skill";
            Field(_ => _.id, type: typeof(IdGraphType));
            Field(_ => _.name);
            Field<SkillType>("parent", resolve: context => repo.Get(context.Source.parentId));
        }
    }
}
