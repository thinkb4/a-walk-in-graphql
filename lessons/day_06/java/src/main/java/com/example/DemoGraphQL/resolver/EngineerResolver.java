package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Engineer;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.stereotype.Component;

/**
 * Field-level resolver for Engineer class
 */
@Component
public class EngineerResolver extends PersonResolver<Engineer> {

    public EngineerResolver(PersonService personService) {
        super(personService);
    }

}
