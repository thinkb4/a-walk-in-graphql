package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import graphql.kickstart.tools.GraphQLQueryResolver;
import org.springframework.stereotype.Component;

/**
 * Top-Level resolver for Query
 */
@Component
public class Query implements GraphQLQueryResolver {

    private final SkillService skillService;

    public Query(SkillService skillService) {
        this.skillService = skillService;
    }

    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }

}
