package com.example.DemoGraphQL.resolver;

import com.coxautodev.graphql.tools.GraphQLResolver;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;
import java.util.Optional;

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
    public Optional<Skill> getParent(Skill skill) {
        return (skill.getParent() != null) ? this.skillService.getSkill(Optional.ofNullable(skill.getParent().getId())) : null;
    }

    /**
     * This is just a sample resolver for a virtual field
     */
    public String getNow(Skill skill) {
        return LocalDateTime.now().toString();
    }
}
