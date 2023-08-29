package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputPersonCreate;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;

import java.util.List;
import java.util.Optional;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
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
                                @Argument(name = "input") final InputPerson input) {
        return this.personService.getFriends(person, input);
    }

    @SchemaMapping
    public List<Skill> skills(final Person person, 
                              @Argument(name = "input") final InputSkill input) {
        return this.personService.getSkills(person, input);
    }
    
    @QueryMapping
    public Person randomPerson() {
        return this.personService.getRandomPerson();
}

    @QueryMapping
    public Optional<Person> person(@Argument InputPerson input) {
        return this.personService.getPerson(input);
    }

    @QueryMapping
    public List<Person> persons(@Argument InputPerson input) {
        return this.personService.getPersons(input);
    }
    
    @MutationMapping
    public Person createPerson(@Argument final InputPersonCreate input) {
        return this.personService.createPerson(input);
    }
}
