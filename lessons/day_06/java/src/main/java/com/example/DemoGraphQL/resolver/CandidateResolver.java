package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputCandidateCreate;
import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Candidate;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import java.util.List;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

/**
 * Field-level resolver for Candidate class
 */
@Controller
public class CandidateResolver extends PersonResolver {

    public CandidateResolver(PersonService personService) {
        super(personService);
    }

    @SchemaMapping
    public String fullName(final Candidate candidate) {
        return super.fullName(candidate);
    }

    @SchemaMapping    
    public List<Person> friends(final Candidate candidate, 
                                @Argument(name = "input") final InputPerson input) {
        return super.friends(candidate, input);
    }

    @SchemaMapping
    public List<Skill> skills(final Candidate candidate, 
                              @Argument(name = "input") final InputSkill input) {
        return super.skills(candidate, input);
    }
    @MutationMapping
    public Candidate createCandidate(@Argument final InputCandidateCreate input) {
        return (Candidate) this.personService.createCandidate(input);
    }

}
