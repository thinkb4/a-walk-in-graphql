using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
    public class PersonRepository: IPersonRepository
    {

        private readonly GraphQLContext _graphContext;
        public PersonRepository(GraphQLContext context)
        {
            _graphContext = context;
        }
        public List<Person> GetAll()
        {
            return _graphContext.Person.ToList();
        }

        public Person AddPerson(Person person)
        {
            _graphContext.Person.Add(person);
            _graphContext.SaveChanges();
            return person;
        }

        public Person GetRandom()
        {
            var rng = new System.Random();
            var indexRandom = rng.Next(_graphContext.Person.Count());
            return _graphContext.Person.ToList()[indexRandom];
        }

    }
}
