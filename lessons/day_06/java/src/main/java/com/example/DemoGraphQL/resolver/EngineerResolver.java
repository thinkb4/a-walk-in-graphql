package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputEngineerCreate;
import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Engineer;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import java.util.List;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;


/**
 * Field-level resolver for Engineer class
 */
@Controller
public class EngineerResolver extends PersonResolver {
    
    public EngineerResolver(PersonService personService) {
        super(personService);
    }
    
    @SchemaMapping
    public String fullName(final Engineer engineer) {
        return super.fullName(engineer);
    }

    @SchemaMapping    
    public List<Person> friends(final Engineer engineer, 
                                @Argument(name = "input") final InputPerson input) {
        return super.friends(engineer, input);
    }

    @SchemaMapping
    public List<Skill> skills(final Engineer engineer, 
                              @Argument(name = "input") final InputSkill input) {
        return super.skills(engineer, input);
    }
    
    @MutationMapping
    public Engineer createEngineer(@Argument final InputEngineerCreate input) {
        return (Engineer) this.personService.createEngineer(input);
    }

}
