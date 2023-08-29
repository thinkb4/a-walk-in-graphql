package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.input.InputSkillCreate;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.ExampleMatcher;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.Random;

@Service
public class SkillService {

    private final SkillRepository skillRepository;

    public SkillService(final SkillRepository skillRepository) {
        this.skillRepository = skillRepository;
    }

    public Optional<Skill> getSkill(Long id) {
        return Optional.ofNullable(id).map(v -> this.skillRepository.findById(v)).orElse(null);
    }

    public Skill getRandomSkill() {
        List<Skill> givenList = this.skillRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public Optional<Skill> getSkill(InputSkill input) {
        return Optional.ofNullable(input).map((InputSkill v) -> findByInput(v)).orElse(null);
    }

    public List<Skill> getSkills(InputSkill input) {
        return Optional.ofNullable(input).map(v -> filterByInput(v)).orElse(this.skillRepository.findAll());
    }

    public Skill createSkill(InputSkillCreate input) {
        return Optional.ofNullable(input).map(v -> {
            Skill parent = (v.parent() != null) ? getSkill(v.parent()).orElse(null) : null;
            Skill newSkill = new Skill(v.name(), parent);
            return skillRepository.save(newSkill);
        }).orElse(null);
    }

    public List<Skill> searchByName(String searchTerm) {
        Skill filterBy = new Skill();
        String term = (searchTerm != null) ? searchTerm : "";
        filterBy.setName(term);
        ExampleMatcher matcher = ExampleMatcher.matching()
                .withStringMatcher(ExampleMatcher.StringMatcher.CONTAINING)
                .withIgnoreCase()
                .withIgnoreNullValues();
        return this.skillRepository.findAll(Example.of(filterBy, matcher));
    }

    private Optional<Skill> findByInput(InputSkill input) {
        Skill filterBy = new Skill();
        if (input.id() != null) filterBy.setId(input.id());
        if (input.name() != null) filterBy.setName(input.name());
        // Considering that depending on the search criteria more than one result can be obtained, we need to findAll limit to 1.
        return this.skillRepository.findAll(Example.of(filterBy), PageRequest.of(0,1)).get().findFirst();
    }

    private List<Skill> filterByInput(InputSkill input) {
        Skill filterBy = new Skill();
        if (input.id() != null) filterBy.setId(input.id());
        if (input.name() != null) filterBy.setName(input.name());
        return this.skillRepository.findAll(Example.of(filterBy));
    }

}
