package com.example.DemoGraphQL.repository;

import com.example.DemoGraphQL.model.Person;
import com.example.DemoGraphQL.model.Skill;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends JpaRepository<Person, Long> {
}
