package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import java.util.List;

/**
 * Field-level resolver for Person class
 */
public abstract class PersonResolver <T extends Person> {

    protected final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    public String fullName(final T person) {
        return person.getName() + " " + person.getSurname();
    }

    public List<Person> friends(final T person, 
                                final InputPerson input) {
        return this.personService.getFriends(person, input);
    }

    public List<Skill> skills(final T person, 
                              final InputSkill input) {
        return this.personService.getSkills(person, input);
    }
}
