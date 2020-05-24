package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLQueryResolver;
import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.Optional;

/**
 * Top-Level resolver for Query
 */
@Component
public class Query implements GraphQLQueryResolver {

    private final SkillService skillService;
    private final PersonService personService;

    public Query(SkillService skillService, PersonService personService) {
        this.skillService = skillService;
        this.personService = personService;
    }

    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }

    public Person randomPerson() {
        return this.personService.getRandomPerson();
    }

    public Optional<Person> person(final Optional<InputPerson> input) {
        return this.personService.getPerson(input);
    }

    public List<Person> persons(final Optional<InputPerson> input) {
        return this.personService.getPersons(input);
    }

    public Optional<Skill> skill(final Optional<InputSkill> input) {
        return this.skillService.getSkill(input);
    }

    public List<Skill> skills(final Optional<InputSkill> input) {
        return this.skillService.getSkills(input);
    }
}
