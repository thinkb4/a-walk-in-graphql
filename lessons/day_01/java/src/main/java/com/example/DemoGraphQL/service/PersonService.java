package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.repository.PersonRepository;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.Random;

@Service
public class PersonService {

    private final PersonRepository personRepository;

    public PersonService(final PersonRepository personRepository) {
        this.personRepository = personRepository;
    }

    public Person getRandomPerson() {
        List<Person> givenList = this.personRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public List<Person> getPersons(Long id) {
        List<Person> persons = new ArrayList<>();
        return Optional.ofNullable(id).map(v -> {
            this.personRepository.findById(v).ifPresent(persons::add);
            return persons;
        }).orElse(this.personRepository.findAll());
    }
}
