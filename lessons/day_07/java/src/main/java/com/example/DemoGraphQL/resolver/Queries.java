package com.example.DemoGraphQL.resolver;

import com.example.DemoGraphQL.input.InputGlobalSearch;
import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.service.PersonService;
import com.example.DemoGraphQL.service.SkillService;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

/**
 * Top-Level resolver for Queries
 */
@Controller
public class Queries {

    private final SkillService skillService;
    private final PersonService personService;

    public Queries(SkillService skillService, PersonService personService) {
        this.skillService = skillService;
        this.personService = personService;
    }

    @QueryMapping
    public Person randomPerson() {
        return this.personService.getRandomPerson();
}

    @QueryMapping
    public Optional<Person> person(@Argument InputPerson input) {
        return this.personService.getPerson(input);
    }

    @QueryMapping
    public List<Person> persons(@Argument InputPerson input) {
        return this.personService.getPersons(input);
    }
    
    @QueryMapping
    public List<Object> search(@Argument final InputGlobalSearch input) {
        List<Object> searchList = new ArrayList<>();
        searchList.addAll(this.personService.searchByName(input.getName()));
        searchList.addAll(this.skillService.searchByName(input.getName()));
        return searchList;
    }
}
