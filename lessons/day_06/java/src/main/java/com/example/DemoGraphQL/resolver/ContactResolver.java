package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputPersonCreate;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Contact;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import java.util.List;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;


/**
 * Field-level resolver for Contact class
 */
@Controller
public class ContactResolver extends PersonResolver {

    public ContactResolver(PersonService personService) {
        super(personService);
    }
    
    @SchemaMapping
    public String fullName(final Contact contact) {
        return super.fullName(contact);
    }

    @SchemaMapping    
    public List<Person> friends(final Contact contact, 
                                @Argument(name = "input") final InputPerson input) {
        return super.friends(contact, input);
    }

    @SchemaMapping
    public List<Skill> skills(final Contact contact, 
                              @Argument(name = "input") final InputSkill input) {
        return super.skills(contact, input);
    }
    
    @MutationMapping
    public Person createPerson(@Argument final InputPersonCreate input) {
        return this.personService.createPerson(input);
    }
}
