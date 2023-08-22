package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.SkillRepository;
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
}
