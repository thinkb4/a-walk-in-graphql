using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person AddPerson(Person person);
        Person GetRandom();
    }
}
