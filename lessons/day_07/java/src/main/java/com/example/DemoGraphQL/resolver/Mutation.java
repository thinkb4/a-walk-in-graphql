package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputCandidateCreate;
import com.example.DemoGraphQL.input.InputEngineerCreate;
import com.example.DemoGraphQL.input.InputPersonCreate;
import com.example.DemoGraphQL.input.InputSkillCreate;
import com.example.DemoGraphQL.model.*;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;
import graphql.kickstart.tools.GraphQLMutationResolver;
import org.springframework.stereotype.Component;

/**
 * Top-Level resolver for Mutation
 */
@Component
public class Mutation implements GraphQLMutationResolver {

    private final SkillService skillService;
    private final PersonService personService;

    public Mutation(SkillService skillService, PersonService personService) {
        this.skillService = skillService;
        this.personService = personService;
    }

    public Skill createSkill(final InputSkillCreate input) {
        return this.skillService.createSkill(input);
    }

    public Person createPerson(final InputPersonCreate input) {
        return this.personService.createPerson(input);
    }

    public Candidate createCandidate(final InputCandidateCreate input) {
        return (Candidate) this.personService.createCandidate(input);
    }

    public Engineer createEngineer(final InputEngineerCreate input) {
        return (Engineer) this.personService.createEngineer(input);
    }
}
