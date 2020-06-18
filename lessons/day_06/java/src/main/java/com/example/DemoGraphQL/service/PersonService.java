package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.*;
import com.example.DemoGraphQL.model.*;
import com.example.DemoGraphQL.repository.PersonRepository;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.ExampleMatcher;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.*;
import java.util.concurrent.ThreadLocalRandom;
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

    public List<Person> getPersons(Optional<InputPerson> input) {
        return input.map(v -> filterByInput(v)).orElse(this.personRepository.findAll());
    }

    public Optional<Person> getPerson(Optional<InputPerson> input) {
        return input.map((InputPerson v) -> findByInput(v)).orElse(null);
    }

    public List<Person> getFriends(Person person, Optional<InputPerson> input) {
        List<Person> friends;
        if (input.isPresent()) {
            InputPerson inputPerson = input.get();
            List<Predicate<Person>> allPredicates = new ArrayList<>();
            if (inputPerson.getId() != null) allPredicates.add(p -> p.getId().equals(inputPerson.getId()));
            if (inputPerson.getAge() != null) allPredicates.add(p -> p.getAge().equals(inputPerson.getAge()));
            if (inputPerson.getEyeColor() != null)
                allPredicates.add(p -> p.getEyeColor().equals(inputPerson.getEyeColor()));
            if (inputPerson.getFavSkill() != null)
                allPredicates.add(p -> p.getFavSkill()!=null && p.getFavSkill().getId().equals(inputPerson.getFavSkill()));
            if (inputPerson.getRole() != null)
                allPredicates.add(p -> p instanceof Engineer && ((Engineer)p).getRole()!=null && ((Engineer)p).getRole().equals(inputPerson.getRole()));
            if (inputPerson.getGrade() != null)
                allPredicates.add(p -> p instanceof Engineer && ((Engineer)p).getGrade()!=null && ((Engineer)p).getGrade().equals(inputPerson.getGrade()));
            if (inputPerson.getTargetRole() != null)
                allPredicates.add(p -> p instanceof Candidate && ((Candidate)p).getTargetRole()!=null && ((Candidate)p).getTargetRole().equals(inputPerson.getTargetRole()));
            if (inputPerson.getTargetGrade() != null)
                allPredicates.add(p -> p instanceof Candidate && ((Candidate)p).getTargetGrade()!=null && ((Candidate)p).getTargetGrade().equals(inputPerson.getTargetGrade()));

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
            InputSkill inputSkill = input.get();
            List<Predicate<Skill>> allPredicates = new ArrayList<>();
            if (inputSkill.getId() != null) allPredicates.add(p -> p.getId().equals(inputSkill.getId()));
            if (inputSkill.getName() != null) allPredicates.add(p -> p.getName().equals(inputSkill.getName()));

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

    public Person createPerson(Optional<InputPersonCreate> input) {
        return input.map(v -> {
            Person newPerson = new Contact();
            newPerson.setName(v.getName());
            newPerson.setSurname(v.getSurname());
            newPerson.setEmail(v.getEmail());
            newPerson.setAge(v.getAge());
            newPerson.setEyeColor(v.getEyeColor());
            if (v.getFavSkill() != null) {
                newPerson.setFavSkill(this.skillRepository.findById(v.getFavSkill()).orElse(null));
            }
            if (v.getSkills() != null) {
                newPerson.setSkills(new HashSet<>(this.skillRepository.findAllById(v.getSkills())));
            }
            if (v.getFriends() != null) {
                newPerson.setFriends(new HashSet<>(this.personRepository.findAllById(v.getFriends())));
            }
            return personRepository.save(newPerson);
        }).orElse(null);
    }


    public Person createCandidate(Optional<InputCandidateCreate> input) {
        return input.map(v -> {
            Candidate newCandidate = new Candidate();
            newCandidate.setName(v.getName());
            newCandidate.setSurname(v.getSurname());
            newCandidate.setEmail(v.getEmail());
            newCandidate.setAge(v.getAge());
            newCandidate.setEyeColor(v.getEyeColor());
            newCandidate.setTargetGrade(v.getTargetGrade());
            newCandidate.setTargetRole(v.getTargetRole());
            if (v.getFavSkill() != null) {
                newCandidate.setFavSkill(this.skillRepository.findById(v.getFavSkill()).orElse(null));
            }
            if (v.getSkills() != null) {
                newCandidate.setSkills(new HashSet<>(this.skillRepository.findAllById(v.getSkills())));
            }
            if (v.getFriends() != null) {
                newCandidate.setFriends(new HashSet<>(this.personRepository.findAllById(v.getFriends())));
            }
            return personRepository.save(newCandidate);
        }).orElse(null);
    }

    public Person createEngineer(Optional<InputEngineerCreate> input) {
        return input.map(v -> {
            Engineer newEngineer = new Engineer();
            newEngineer.setEmployeeId(ThreadLocalRandom.current().nextLong(1,999));
            newEngineer.setName(v.getName());
            newEngineer.setSurname(v.getSurname());
            newEngineer.setEmail(v.getEmail());
            newEngineer.setAge(v.getAge());
            newEngineer.setEyeColor(v.getEyeColor());
            newEngineer.setGrade(v.getGrade());
            newEngineer.setRole(v.getRole());
            if (v.getFavSkill() != null) {
                newEngineer.setFavSkill(this.skillRepository.findById(v.getFavSkill()).orElse(null));
            }
            if (v.getSkills() != null) {
                newEngineer.setSkills(new HashSet<>(this.skillRepository.findAllById(v.getSkills())));
            }
            if (v.getFriends() != null) {
                newEngineer.setFriends(new HashSet<>(this.personRepository.findAllById(v.getFriends())));
            }
            return personRepository.save(newEngineer);
        }).orElse(null);
    }

    public List<Person> searchByName(Optional<String> searchTerm) {
        Person filterBy = new Person();
        filterBy.setName(searchTerm.orElse(""));
        ExampleMatcher matcher = ExampleMatcher.matching()
                .withStringMatcher(ExampleMatcher.StringMatcher.CONTAINING)
                .withIgnoreCase()
                .withIgnoreNullValues();
        return this.personRepository.findAll(Example.of(filterBy, matcher));
    }

    private Optional<Person> findByInput(InputPerson input) {
        // Considering that depending on the search criteria more than one result can be obtained, we need to findAll limit to 1.
        return this.personRepository.findAll(prepareQueryByExample(input), PageRequest.of(0, 1)).get().findFirst();
    }

    private List<Person> filterByInput(InputPerson input) {
        return this.personRepository.findAll(prepareQueryByExample(input));
    }

    private Example<Person> prepareQueryByExample(InputPerson input) {
        Person filterBy = new Person();
        filterBy.setId(input.getId());
        filterBy.setAge(input.getAge());
        filterBy.setEyeColor(input.getEyeColor());
        if (input.getFavSkill() != null) {
            Skill tmpSkill = new Skill();
            tmpSkill.setId(input.getFavSkill());
            filterBy.setFavSkill(tmpSkill);
        }
        if (input.getRole() != null || input.getGrade() != null) {
            filterBy = new Engineer();
            ((Engineer) filterBy).setRole(input.getRole());
            ((Engineer) filterBy).setGrade(input.getGrade());
        } else if(input.getTargetRole()!= null || input.getTargetGrade() != null) {
            filterBy = new Candidate();
            ((Candidate) filterBy).setTargetRole(input.getTargetRole());
            ((Candidate) filterBy).setTargetGrade(input.getTargetGrade());
        }
        return Example.of(filterBy);
    }
}
