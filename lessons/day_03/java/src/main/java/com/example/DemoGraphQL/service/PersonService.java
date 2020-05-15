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

    public Optional<Person> getPerson(final long id) {
        return this.personRepository.findById(id);
    }

    public Person getRandomPerson() {
        List<Person> givenList = this.personRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public List<Person> getPersons(Optional<Long> id) {
        List<Person> persons = new ArrayList<>();
        if (id.isPresent()) {
            Optional<Person> person = this.personRepository.findById(id.get());
            if (person.isPresent()) persons.add(person.get());
        } else {
            persons.addAll(this.personRepository.findAll());
        }
        return persons;
    }

    public Optional<Person> getPerson(Optional<Long> id) {
        return id.isPresent() ? this.personRepository.findById(id.get()) : null;
    }

    public List<Person> getFriends(Person person, Optional<Long> friendId) {
        List<Person> friends = new ArrayList<>();
        if (friendId.isPresent()) {
            Optional<Person> friend = person.getFriends().stream()
                    .filter(myFriend -> myFriend.getId().equals(friendId.get()))
                    .findAny();
            if (friend.isPresent()) friends.add(friend.get());
        } else {
            friends = new ArrayList<>(person.getFriends());
        }
        return friends;
    }

    public List<Skill> getSkills(Person person, Optional<Long> skillId) {
        List<Skill> skills = new ArrayList<>();
        if (skillId.isPresent()) {
            Optional<Skill> skill = person.getSkills().stream()
                    .filter(mySkill -> mySkill.getId().equals(skillId.get()))
                    .findAny();
            if (skill.isPresent()) skills.add(skill.get());
        } else {
            skills = new ArrayList<>(person.getSkills());
        }
        return skills;
    }
}
