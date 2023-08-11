package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

import java.time.LocalDateTime;
import java.util.Optional;

/**
 * Field-level resolver for Skill class
 */
@Controller
public class SkillResolver {

    private final SkillService skillService;

    public SkillResolver(SkillService skillService) {
        this.skillService = skillService;
    }

    /**
     * This is a resolver for "parent" entity field
     */
    @SchemaMapping
    public Optional<Skill> getParent(Skill skill) {
        return (skill.getParent() != null) ? this.skillService.getSkill(skill.getParent().getId()) : null;
    }

    /**
     * This is just a sample resolver for a virtual field
     */
    @SchemaMapping
    public String getNow(Skill skill) {
        return LocalDateTime.now().toString();
    }

    @QueryMapping
    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }
}
