package com.example.DemoGraphQL.service;

import com.example.DemoGraphQL.model.Skill;
import com.example.DemoGraphQL.repository.SkillRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.Random;

@Service
public class SkillService {

    private final SkillRepository skillRepository;

    public SkillService(SkillRepository skillRepository) {
        this.skillRepository = skillRepository;
    }

    public Optional<Skill> getSkill(final long id) {
        return this.skillRepository.findById(id);
    }

    public Skill getRandomSkill() {
        List<Skill> givenList = this.skillRepository.findAll();
        Random rand = new Random();
        return givenList.get(rand.nextInt(givenList.size()));
    }
}
