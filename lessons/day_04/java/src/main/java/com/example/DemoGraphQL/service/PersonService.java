package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.InputPerson;
import com.example.DemoGraphQL.input.InputPersonCreate;
import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.PersonRepository;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;

@Service
public class PersonService {

    private final PersonRepository personRepository;
    private final SkillRepository skillRepository;

    public PersonService(final PersonRepository personRepository, final SkillRepository skillRepository) {
        this.personRepository = personRepository;
        this.skillRepository = skillRepository;
    }

    public Person getRandomPerson() {
        List<Person> givenList = this.personRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public List<Person> getPersons(InputPerson input) {
        return Optional.ofNullable(input).map(v -> filterByInput(v)).orElse(this.personRepository.findAll());
    }

    public Optional<Person> getPerson(InputPerson input) {
        return Optional.ofNullable(input).map((InputPerson v) -> findByInput(v)).orElse(null);
    }

    public List<Person> getFriends(Person person, InputPerson input) {
        List<Person> friends;
        if (input != null) {
            List<Predicate<Person>> allPredicates = new ArrayList<>();
            if (input.id()!= null) allPredicates.add(p -> p.getId().equals(input.id()));
            if (input.age() != null) allPredicates.add(p -> p.getAge().equals(input.age()));
            if (input.eyeColor() != null) allPredicates.add(p -> p.getEyeColor().equals(input.eyeColor()));
            if (input.favSkill() != null) allPredicates.add(p -> p.getFavSkill().getId().equals(input.favSkill()));

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

    public List<Skill> getSkills(Person person, InputSkill input) {
        List<Skill> skills;
        if (input != null) {
            List<Predicate<Skill>> allPredicates = new ArrayList<>();
            if (input.id() != null) allPredicates.add(p -> p.getId().equals(input.id()));
            if (input.name() != null) allPredicates.add(p -> p.getName().equals(input.name()));

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

    public Person createPerson(InputPersonCreate input) {
        return Optional.ofNullable(input).map(v -> {
            Person newPerson = new Person();
            newPerson.setName(v.name());
            newPerson.setSurname(v.surname());
            newPerson.setEmail(v.email());
            newPerson.setAge(v.age());
            newPerson.setEyeColor(v.eyeColor());
            if (v.favSkill() != null) {
                newPerson.setFavSkill(this.skillRepository.findById(v.favSkill()).orElse(null));
            }
            if (v.skills() != null) {
                newPerson.setSkills(new HashSet<>(this.skillRepository.findAllById(v.skills())));
            }
            if (v.friends() != null) {
                newPerson.setFriends(new HashSet<>(this.personRepository.findAllById(v.friends())));
            }
            return personRepository.save(newPerson);
        }).orElse(null);
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
        filterBy.setId(input.id());
        filterBy.setAge(input.age());
        filterBy.setEyeColor(input.eyeColor());
        if (input.favSkill() != null) {
            Skill tmpSkill = new Skill();
            tmpSkill.setId(input.favSkill());
            filterBy.setFavSkill(tmpSkill);
        }
        return Example.of(filterBy);
    }
}
