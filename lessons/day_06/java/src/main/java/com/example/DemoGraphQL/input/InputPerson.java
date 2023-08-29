package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;
import com.example.DemoGraphQL.model.Grade;
import com.example.DemoGraphQL.model.Role;

public record InputPerson (
    Long id,
    Integer age,
    EyeColor eyeColor,
    Long favSkill,
    Role targetRole,
    Grade targetGrade,
    Role role,
    Grade grade
){}
