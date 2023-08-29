package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;

public record InputPerson(
        Long id,
        Integer age,
        EyeColor eyeColor,
        Long favSkill) {}
