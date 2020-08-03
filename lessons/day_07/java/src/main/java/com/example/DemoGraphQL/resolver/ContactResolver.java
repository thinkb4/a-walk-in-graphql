package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.model.Contact;
import com.example.DemoGraphQL.service.PersonService;
import org.springframework.stereotype.Component;

/**
 * Field-level resolver for Contact class
 */
@Component
public class ContactResolver extends PersonResolver<Contact> {

    public ContactResolver(PersonService personService) {
        super(personService);
    }

}
