package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;
import com.example.DemoGraphQL.model.Grade;
import com.example.DemoGraphQL.model.Role;

public class InputPerson {
    private Long id;
    private Integer age;
    private EyeColor eyeColor;
    private Long favSkill;
    private Role targetRole;
    private Grade targetGrade;
    private Role role;
    private Grade grade;

    public InputPerson() {
    }

    public InputPerson(Long id, Integer age, EyeColor eyeColor, Long favSkill, Role targetRole, Grade targetGrade, Role role, Grade grade) {
        this.id = id;
        this.age = age;
        this.eyeColor = eyeColor;
        this.favSkill = favSkill;
        this.targetRole = targetRole;
        this.targetGrade = targetGrade;
        this.role = role;
        this.grade = grade;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Integer getAge() {
        return age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public EyeColor getEyeColor() {
        return eyeColor;
    }

    public void setEyeColor(EyeColor eyeColor) {
        this.eyeColor = eyeColor;
    }

    public Long getFavSkill() {
        return favSkill;
    }

    public void setFavSkill(Long favSkill) {
        this.favSkill = favSkill;
    }

    public Role getTargetRole() {
        return targetRole;
    }

    public void setTargetRole(Role targetRole) {
        this.targetRole = targetRole;
    }

    public Grade getTargetGrade() {
        return targetGrade;
    }

    public void setTargetGrade(Grade targetGrade) {
        this.targetGrade = targetGrade;
    }

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }

    public Grade getGrade() {
        return grade;
    }

    public void setGrade(Grade grade) {
        this.grade = grade;
    }
}
