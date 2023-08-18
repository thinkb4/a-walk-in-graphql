package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
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
        return Optional.ofNullable(id).map(v -> {
            List<Person> persons = new ArrayList<>();
            this.personRepository.findById(v).ifPresent(persons::add);
            return persons;
        }).orElse(this.personRepository.findAll());
    }

    public Optional<Person> getPerson(Long id) {
        Optional<Person> person = null;
        if (id != null) {
            person = this.personRepository.findById(id);
        }
        return person;
    }

    public List<Person> getFriends(Person person, Long friendId) {
        return Optional.ofNullable(friendId).map(v -> {
            List<Person> friends = new ArrayList<>();
            person.getFriends().stream()
                    .filter(myFriend -> myFriend.getId().equals(v))
                    .findFirst()
                    .ifPresent(friends::add);
            return friends;
        }).orElse(new ArrayList<>(person.getFriends()));
    }

    public List<Skill> getSkills(Person person, Long skillId) {
        return Optional.ofNullable(skillId).map(v -> {
            List<Skill> skills = new ArrayList<>();
            person.getSkills().stream()
                    .filter(mySkill -> mySkill.getId().equals(v))
                    .findFirst()
                    .ifPresent(skills::add);
            return skills;
        }).orElse(new ArrayList<>(person.getSkills()));
    }

//// HINTS FOR DAY 03 !! - PLEASE UNCOMMENT THESE METHODS TO PROVIDE SUPPORT FOR INPUT TYPES
//    public List<Person> getPersons(InputPerson input) {
//        return Optional.ofNullable(input).map(v -> filterByInput(v)).orElse(this.personRepository.findAll());
//    }
//
//    public Optional<Person> getPerson(InputPerson input) {
//        return Optional.ofNullable(input).map((InputPerson v) -> findByInput(v)).orElse(null);
//    }
//
//    public List<Person> getFriends(Person person, InputPerson input) {
//        List<Person> friends;
//        if (input != null) {
//            List<Predicate<Person>> allPredicates = new ArrayList<>();
//            if (input.getId() != null) allPredicates.add(p -> p.getId().equals(input.getId()));
//            if (input.getAge() != null) allPredicates.add(p -> p.getAge().equals(input.getAge()));
//            if (input.getEyeColor() != null) allPredicates.add(p -> p.getEyeColor().equals(input.getEyeColor()));
//            if (input.getFavSkill() != null) allPredicates.add(p -> p.getFavSkill().getId().equals(input.getFavSkill()));
//
//            //Composes several predicates into a single predicate, and then applies the composite predicate to a stream.
//            Predicate<Person> compositePredicate = allPredicates.stream().reduce(w -> true, Predicate::and);
//            friends = person.getFriends().stream()
//                    .filter(compositePredicate)
//                    .collect(Collectors.toList());
//        } else {
//            friends = new ArrayList<>(person.getFriends());
//        }
//        return friends;
//    }
//
//    public List<Skill> getSkills(Person person, InputSkill input) {
//        List<Skill> skills;
//        if (input != null) {
//            List<Predicate<Skill>> allPredicates = new ArrayList<>();
//            if (input.getId() != null) allPredicates.add(p -> p.getId().equals(input.getId()));
//            if (input.getName() != null) allPredicates.add(p -> p.getName().equals(input.getName()));
//
//            // Composes several predicates into a single predicate, and then applies the composite predicate to a stream.
//            Predicate<Skill> compositePredicate = allPredicates.stream().reduce(w -> true, Predicate::and);
//            skills = person.getSkills().stream()
//                    .filter(compositePredicate)
//                    .collect(Collectors.toList());
//        } else {
//            skills = new ArrayList<>(person.getSkills());
//        }
//        return skills;
//    }
//
//    private Optional<Person> findByInput(InputPerson input) {
//        // Considering that depending on the search criteria more than one result can be obtained, we need to findAll limit to 1.
//        return this.personRepository.findAll(prepareQueryByExample(input), PageRequest.of(0,1)).get().findFirst();
//    }
//
//    private List<Person> filterByInput(InputPerson input) {
//        return this.personRepository.findAll(prepareQueryByExample(input));
//    }
//
//    private Example<Person> prepareQueryByExample(InputPerson input) {
//        Person filterBy = new Person();
//        filterBy.setId(input.getId());
//        filterBy.setAge(input.getAge());
//        filterBy.setEyeColor(input.getEyeColor());
//        if (input.getFavSkill() != null) {
//            Skill tmpSkill = new Skill();
//            tmpSkill.setId(input.getFavSkill());
//            filterBy.setFavSkill(tmpSkill);
//        }
//        return Example.of(filterBy);
//    }
}
