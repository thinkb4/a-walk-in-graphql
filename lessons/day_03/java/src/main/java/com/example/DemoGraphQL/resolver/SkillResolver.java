package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;
import org.springframework.graphql.data.method.annotation.Argument;

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
    @SchemaMapping(field = "parent")
    public Optional<Skill> getParent(final Skill skill) {
        return (skill.getParent() != null) ? this.skillService.getSkill(skill.getParent().getId()) : null;
    }

    /**
     * This is just a sample resolver for a virtual field
     */
    @SchemaMapping(field = "now")
    public String getNow(Skill skill) {
        return LocalDateTime.now().toString();
    }

    @QueryMapping
    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }
    
    @QueryMapping
    public Optional<Skill> skill(@Argument final Long id) {
        return this.skillService.getSkill(id);
    }

    @QueryMapping
    public List<Skill> skills(@Argument final Long id) {
        return this.skillService.getSkills(id);
    }
}
