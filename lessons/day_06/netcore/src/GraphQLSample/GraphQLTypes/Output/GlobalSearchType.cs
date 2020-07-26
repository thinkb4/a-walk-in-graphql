using GraphQL.Types;

namespace GraphQLNetCore.GraphQLTypes.Output
{
   public class GlobalSearchType :  UnionGraphType
   {
      public GlobalSearchType()
      {
         Name = "GlobalSearch";
         Type<SkillType>();
         Type<ContactType>();
         Type<CandidateType>();
         Type<EngineerType>();
      }
   }
}
