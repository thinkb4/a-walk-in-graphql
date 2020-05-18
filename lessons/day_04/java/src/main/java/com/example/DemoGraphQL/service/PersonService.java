package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.PersonRepository;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.ExampleMatcher;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.Random;
import java.util.function.Predicate;
import java.util.stream.Collectors;

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

    public List<Person> getPersons(Optional<InputPerson> input) {
        List<Person> persons = new ArrayList<>();
        if (input.isPresent()) {
            persons = filterByInput(input.get());
        } else {
            persons.addAll(this.personRepository.findAll());
        }
        return persons;
    }

    public Optional<Person> getPerson(Optional<InputPerson> input) {
        Optional<Person> person = null;
        if (input.isPresent()) {
            person = findByInput(input.get());
        }
        return person;
    }

    public List<Person> getFriends(Person person, Optional<InputPerson> input) {
        List<Person> friends;
        if (input.isPresent()) {
            List<Predicate<Person>> allPredicates = new ArrayList<>();
            if (input.get().getId() != null) allPredicates.add(p -> p.getId().equals(input.get().getId()));
            if (input.get().getAge() != null) allPredicates.add(p -> p.getAge().equals(input.get().getAge()));
            if (input.get().getEyeColor() != null) allPredicates.add(p -> p.getEyeColor().equals(input.get().getEyeColor()));
            if (input.get().getFavSkill() != null) allPredicates.add(p -> p.getFavSkill().getId().equals(input.get().getFavSkill()));

            //Composes several predicates into a single predicate, and then applies the composite predicate to a stream.
            Predicate<Person> compositePredicate = allPredicates.stream().reduce(w -> true, Predicate::and);
            friends = person.getFriends().stream()
                    .filter(compositePredicate)
                    .collect(Collectors.toList());
        } else {
            friends = new ArrayList<>(person.getFriends());
        }
        return friends;
    }

    public List<Skill> getSkills(Person person, Optional<InputSkill> input) {
        List<Skill> skills;
        if (input.isPresent()) {
            List<Predicate<Skill>> allPredicates = new ArrayList<>();
            if (input.get().getId() != null) allPredicates.add(p -> p.getId().equals(input.get().getId()));
            if (input.get().getName() != null) allPredicates.add(p -> p.getName().equals(input.get().getName()));

            // Composes several predicates into a single predicate, and then applies the composite predicate to a stream.
            Predicate<Skill> compositePredicate = allPredicates.stream().reduce(w -> true, Predicate::and);
            skills = person.getSkills().stream()
                    .filter(compositePredicate)
                    .collect(Collectors.toList());
        } else {
            skills = new ArrayList<>(person.getSkills());
        }
        return skills;
    }

    private Optional<Person> findByInput(InputPerson input) {
        // Considering that depending on the search criteria more than one result can be obtained, we need to findAll limit to 1.
        return this.personRepository.findAll(prepareQueryByExample(input), PageRequest.of(0,1)).get().findFirst();
    }

    private List<Person> filterByInput(InputPerson input) {
        return this.personRepository.findAll(prepareQueryByExample(input));
    }

    private Example<Person> prepareQueryByExample(InputPerson input) {
        Person filterBy = new Person();
        ExampleMatcher matcher = ExampleMatcher.matching();
        if (input.getId() != null) filterBy.setId(input.getId());
        if (input.getAge() != null) filterBy.setAge(input.getAge());
        if (input.getEyeColor() != null) filterBy.setEyeColor(input.getEyeColor());
        if (input.getFavSkill() != null) matcher.withMatcher("favSkill.id", match -> match.equals(input.getFavSkill()));
        return Example.of(filterBy, matcher);
    }
}
