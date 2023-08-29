package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;
import com.example.DemoGraphQL.model.Grade;
import com.example.DemoGraphQL.model.Role;

import java.util.List;

public record InputEngineerCreate (
    String name,
    String surname,
    String email,
    Integer age,
    EyeColor eyeColor,
    Long favSkill,
    List<Long> friends,
    List<Long> skills,
    Role role,
    Grade grade
){}