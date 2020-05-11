package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLResolver;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.stereotype.Component;

/**
 * Field-level resolver for Person class
 */
@Component
public class PersonResolver implements GraphQLResolver<Person> {

    private final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    public String fullName(Person person) {
        return person.getName() + " " + person.getSurname();
    }
}
