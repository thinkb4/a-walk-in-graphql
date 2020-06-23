package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import graphql.kickstart.tools.GraphQLResolver;
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

    public List<Person> friends(final Person person, final Long friendId) {
        return this.personService.getFriends(person, Optional.ofNullable(friendId));
    }

    public List<Skill> skills(final Person person, final Long skillId) {
        return this.personService.getSkills(person, Optional.ofNullable(skillId));
    }
}
