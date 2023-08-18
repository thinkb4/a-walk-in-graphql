package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;

import java.util.List;
import java.util.Optional;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

/**
 * Field-level resolver for Person class
 */
@Controller
public class PersonResolver {

    private final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    @SchemaMapping
    public String fullName(final Person person) {
        return person.getName() + " " + person.getSurname();
    }

    @SchemaMapping    
    public List<Person> friends(final Person person, 
                                @Argument(name = "id") final Long friendId) {
        return this.personService.getFriends(person, friendId);
    }

    @SchemaMapping
    public List<Skill> skills(final Person person, 
                              @Argument(name = "id") final Long skillId) {
        return this.personService.getSkills(person, skillId);
    }
    
    @QueryMapping
    public Person randomPerson() {
        return this.personService.getRandomPerson();
    }

    @QueryMapping
    public Optional<Person> person(@Argument final Long id) {
        return this.personService.getPerson(id);
    }

    @QueryMapping
    public List<Person> persons(@Argument final Long id) {
        return this.personService.getPersons(id);
    }
}
