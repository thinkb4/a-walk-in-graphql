package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

import java.util.List;

/**
 * Top-Level resolver for Query
 */
@Controller
public class Query {

    private final SkillService skillService;
    private final PersonService personService;

    public Query(SkillService skillService, PersonService personService) {
        this.skillService = skillService;
        this.personService = personService;
    }

    @QueryMapping
    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }

    @QueryMapping
    public Person randomPerson() {
        return this.personService.getRandomPerson();
    }

    @QueryMapping
    public List<Person> persons(final Long id) {
        return this.personService.getPersons(id);
    }

}
