package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLQueryResolver;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.stereotype.Component;

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
}
