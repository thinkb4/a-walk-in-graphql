package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.Optional;

/**
 * Field-level resolver for Person class
 */
@Component
public abstract class PersonResolver<T extends Person> {

    private final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    public String fullName(T person) {
        return person.getName() + " " + person.getSurname();
    }

    public List<Person> friends(final T person, final InputPerson input) {
        return this.personService.getFriends(person, Optional.ofNullable(input));
    }

    public List<Skill> skills(final T person, final InputSkill input) {
        return this.personService.getSkills(person, Optional.ofNullable(input));
    }
}
