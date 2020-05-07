package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLResolver;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;

/**
 * Field-level resolver for Skill class
 */
@Component
public class SkillResolver implements GraphQLResolver<Skill> {

    private final SkillService skillService;

    public SkillResolver(SkillService skillService) {
        this.skillService = skillService;
    }

    /**
     * This is a resolver for "parent" entity field
     */
    public Skill getParent(Skill skill) {
        Skill parent = null;
        if (skill.getParent() != null)
            parent = skillService.getSkill(skill.getParent().getId()).get();
        return parent;
    }

    /**
     * This is just a sample resolver for a virtual field
     */
    public String getNow(Skill skill) {
        return LocalDateTime.now().toString();
    }
}
