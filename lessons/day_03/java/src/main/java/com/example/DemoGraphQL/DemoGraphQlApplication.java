package com.example.DemoGraphQL;

import com.example.DemoGraphQL.model.EyeColor;
import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.PersonRepository;
import com.example.DemoGraphQL.repository.SkillRepository;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

import java.io.File;
import java.util.*;

@SpringBootApplication
public class DemoGraphQlApplication {

    public static void main(String[] args) {
        SpringApplication.run(DemoGraphQlApplication.class, args);
    }

    /**
     * A single source of truth is used for the course data structure. This bean's purpose is to feed the json datasource
     * into an easy to use persistence layer for the Java exercise.
     */
    @Bean
    public CommandLineRunner loadData4Demo(SkillRepository skillRepository, PersonRepository personRepository) {
        return (args) -> {
            // read json and write to db
            ObjectMapper objectMapper = new ObjectMapper();
            JsonNode rootNode = objectMapper.readTree(new File("../datasource/data.json"));
            Map<Long, Skill> skillsMap = new HashMap<>();
            for (JsonNode skill : rootNode.path("skills")) {
                Skill savedSkill = null;
                if (skill.path("parent").isNull()) {
                    savedSkill = skillRepository.save(new Skill(skill.path("name").asText(), null));
                } else {
                    Skill parent = skillRepository.findById(skill.path("parent").asLong()).get();
                    savedSkill = skillRepository.save(new Skill(skill.path("name").asText(), parent));
                }
                skillsMap.put(savedSkill.getId(), savedSkill);
            }

            // load persons
            Map<Long, Person> personsMap = new HashMap<>();
            for (JsonNode person : rootNode.path("persons")) {
                Person newPerson = new Person(
                        person.path("name").asText(),
                        person.path("surname").asText(),
                        person.path("email").asText(),
                        EyeColor.fromLabel(person.path("eyeColor").asText()),
                        person.path("age").asInt());

                // load favourite skill
                if (!person.path("favSkill").isNull()) {
                    newPerson.setFavSkill(skillsMap.get(person.path("favSkill").asLong()));
                }

                // load skills for a person
                if (person.path("skills").size() > 0) {
                    Set<Skill> personSkills = new HashSet<>();
                    for (JsonNode skill : person.path("skills")) {
                        personSkills.add(skillsMap.get(skill.asLong()));
                    }
                    newPerson.setSkills(personSkills);
                }

                // load friends
                if (person.path("friends").size() > 0) {
                    List<Long> friendIds = new ArrayList<>();
                    person.path("friends").elements().forEachRemaining(f -> friendIds.add(f.asLong()));
                    Set<Person> friends = new HashSet<>(personRepository.findAllById(friendIds));
                    newPerson.setFriends(friends);
                }

                personRepository.saveAndFlush(newPerson);
            }
        };
    }
}
