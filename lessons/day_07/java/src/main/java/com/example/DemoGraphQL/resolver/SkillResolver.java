package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.input.InputSkillCreate;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

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
    public Optional<Skill> getParent(Skill skill) {
        Optional<Skill> parent = null;
        if (skill.getParent() != null)
            parent = skillService.getSkill(skill.getParent().getId());
        return parent;
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
    public Optional<Skill> skill(@Argument final InputSkill input) {
        return this.skillService.getSkill(input);
    }

    @QueryMapping
    public List<Skill> skills(@Argument final InputSkill input) {
        return this.skillService.getSkills(input);
    }
    
    @MutationMapping
    public Skill createSkill(@Argument final InputSkillCreate input) {
        return this.skillService.createSkill(input);
    }
}
