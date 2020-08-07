package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;
import graphql.kickstart.tools.GraphQLQueryResolver;
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

    public Optional<Person> person(final Long id) {
        return this.personService.getPerson(id);
    }

    public List<Person> persons(final Long id) {
        return this.personService.getPersons(id);
    }

    public Optional<Skill> skill(final Long id) {
        return this.skillService.getSkill(id);
    }

    public List<Skill> skills(final Long id) {
        return this.skillService.getSkills(id);
    }
}
