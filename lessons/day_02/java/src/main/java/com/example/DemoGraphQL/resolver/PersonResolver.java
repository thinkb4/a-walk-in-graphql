package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.service.PersonService;
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

    public String fullName(Person person) {
        return person.getName() + " " + person.getSurname();
    }
}
