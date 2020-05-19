using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
    public class SkillType:ObjectGraphType<Skill>
    {
        public SkillType()
        {
            Field(_ => _.id);
            Field(_ => _.name);
            Field(_ => _.parent, nullable: true);
        }
    }
}
