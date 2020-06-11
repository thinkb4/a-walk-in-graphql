package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.input.InputSkillCreate;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.ExampleMatcher;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
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
        return this.skillRepository.findById(id);
    }

    public Skill getRandomSkill() {
        List<Skill> givenList = this.skillRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public Optional<Skill> getSkill(Optional<InputSkill> input) {
        return input.map((InputSkill v) -> filterByInput(v)).orElse(null);
    }

    public List<Skill> getSkills(Optional<InputSkill> input) {
        List<Skill> skills = new ArrayList<>();
        return input.map(v -> {
            filterByInput(v).ifPresent(skills::add);
            return skills;
        }).orElse(this.skillRepository.findAll());
    }

    public Skill createSkill(Optional<InputSkillCreate> input) {
        return input.map(v -> {
            Skill parent = (v.getParent() != null) ? getSkill(v.getParent()).orElse(null) : null;
            Skill newSkill = new Skill(v.getName(), parent);
            return skillRepository.save(newSkill);
        }).orElse(null);
    }

    public List<Skill> searchByName(Optional<String> searchTerm) {
        Skill filterBy = new Skill();
        filterBy.setName(searchTerm.orElse(""));
        ExampleMatcher matcher = ExampleMatcher.matching()
                .withStringMatcher(ExampleMatcher.StringMatcher.CONTAINING)
                .withIgnoreCase()
                .withIgnoreNullValues();
        return this.skillRepository.findAll(Example.of(filterBy, matcher));
    }

    private Optional<Skill> filterByInput(InputSkill input) {
        Skill filterBy = new Skill();
        filterBy.setId(input.getId());
        filterBy.setName(input.getName());
        return this.skillRepository.findOne(Example.of(filterBy));
    }


}
