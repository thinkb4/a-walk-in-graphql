package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;

import java.util.List;

public record InputPersonCreate (
    String name,
    String surname,
    String email,
    Integer age,
    EyeColor eyeColor,
    Long favSkill,
    List<Long> friends,
    List<Long> skills
){}
