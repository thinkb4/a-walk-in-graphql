using System.Collections.Generic;

namespace GraphQLNetCore.Models
{
    public class FileData
    {
        public List<PersonData> persons { get; set; }
        public List<SkillData> skills { get; set; }
    }
}