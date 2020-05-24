package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLResolver;
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
public class PersonResolver implements GraphQLResolver<Person> {

    private final PersonService personService;

    public PersonResolver(PersonService personService) {
        this.personService = personService;
    }

    public String fullName(Person person) {
        return person.getName() + " " + person.getSurname();
    }

    public List<Person> friends(final Person person, final Optional<InputPerson> input) {
        return this.personService.getFriends(person, input);
    }

    public List<Skill> skills(final Person person, final Optional<InputSkill> input) {
        return this.personService.getSkills(person, input);
    }
}
