package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

import java.util.List;

/**
 * Field-level resolver for Person class
 */
@Controller
public class PersonResolver {

    private final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    @QueryMapping
    public Person randomPerson() {
        return this.personService.getRandomPerson();
    }

    @QueryMapping
    public List<Person> persons(@Argument final Long id) {
        return this.personService.getPersons(id);
    }

    @SchemaMapping
    public String fullName(Person person) {
        return person.getName() + " " + person.getSurname();
    }
}
