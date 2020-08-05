using System.Collections.Generic;

namespace GraphQLNetCore.Models.Abstractions
{
   public interface IInputPersonCreate<T> where T:Person
   {
      T ToPerson();
      List<int> Friends { get; }
      List<int> Skills { get; }
   }
}
