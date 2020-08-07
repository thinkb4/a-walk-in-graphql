package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Candidate;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.stereotype.Component;

/**
 * Field-level resolver for Candidate class
 */
@Component
public class CandidateResolver extends PersonResolver<Candidate> {

    public CandidateResolver(PersonService personService) {
        super(personService);
    }

}
