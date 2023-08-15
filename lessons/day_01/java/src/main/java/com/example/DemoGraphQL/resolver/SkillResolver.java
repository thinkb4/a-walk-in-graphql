package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.SkillService;
import java.time.LocalDateTime;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

@Controller
public class SkillResolver {

    private final SkillService skillService;
    
    public SkillResolver(SkillService skillService) {
        this.skillService = skillService;
    }

    @QueryMapping
    public Skill randomSkill() {
        return this.skillService.getRandomSkill();
    }

    @SchemaMapping
    public String now(Skill skill) {
        return LocalDateTime.now().toString();
    } 
    
}
