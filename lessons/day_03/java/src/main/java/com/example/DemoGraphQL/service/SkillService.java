package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.input.InputSkill;
import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.data.domain.Example;
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
        return Optional.ofNullable(id).map(v -> this.skillRepository.findById(v)).orElse(null);
    }

    public Skill getRandomSkill() {
        List<Skill> givenList = this.skillRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }

    public List<Skill> getSkills(Long id) {
        return Optional.ofNullable(id).map(v -> {
            List<Skill> skills = new ArrayList<>();
            this.skillRepository.findById(v).ifPresent(skills::add);
            return skills;
        }).orElse(this.skillRepository.findAll());
    }

//// HINTS FOR DAY 03 !! - PLEASE UNCOMMENT THESE METHODS TO PROVIDE SUPPORT FOR INPUT TYPES
//    public Optional<Skill> getSkill(InputSkill input) {
//        return Optional.ofNullable(input).map((InputSkill v) -> findByInput(v)).orElse(null);
//    }
//
//    public List<Skill> getSkills(InputSkill input) {
//        return Optional.ofNullable(input).map(v -> filterByInput(v)).orElse(this.skillRepository.findAll());
//    }
//
//    public Skill createSkill(InputSkillCreate input) {
//        return Optional.ofNullable(input).map(v -> {
//            Skill parent = (v.getParent() != null) ? getSkill(v.getParent()).orElse(null) : null;
//            Skill newSkill = new Skill(v.getName(), parent);
//            return skillRepository.save(newSkill);
//        }).orElse(null);
//    }
//
//    private Optional<Skill> findByInput(InputSkill input) {
//        Skill filterBy = new Skill();
//        if (input.getId() != null) filterBy.setId(input.getId());
//        if (input.getName() != null) filterBy.setName(input.getName());
//        // Considering that depending on the search criteria more than one result can be obtained, we need to findAll limit to 1.
//        return this.skillRepository.findAll(Example.of(filterBy), PageRequest.of(0,1)).get().findFirst();
//    }
//
//    private List<Skill> filterByInput(InputSkill input) {
//        Skill filterBy = new Skill();
//        if (input.getId() != null) filterBy.setId(input.getId());
//        if (input.getName() != null) filterBy.setName(input.getName());
//        return this.skillRepository.findAll(Example.of(filterBy));
//    }
}
