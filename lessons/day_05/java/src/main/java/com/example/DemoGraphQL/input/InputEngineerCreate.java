package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;
import com.example.DemoGraphQL.model.Grade;
import com.example.DemoGraphQL.model.Role;

import java.util.List;

public class InputEngineerCreate {
    private String name;
    private String surname;
    private String email;
    private Integer age;
    private EyeColor eyeColor;
    private Long favSkill;
    private List<Long> friends;
    private List<Long> skills;
    private Role role;
    private Grade grade;

    public InputEngineerCreate() {
    }

    public InputEngineerCreate(String name, String surname, String email, Integer age, EyeColor eyeColor, Long favSkill, List<Long> friends, List<Long> skills, Role role, Grade grade) {
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.age = age;
        this.eyeColor = eyeColor;
        this.favSkill = favSkill;
        this.friends = friends;
        this.skills = skills;
        this.role = role;
        this.grade = grade;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
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

    public List<Long> getFriends() {
        return friends;
    }

    public void setFriends(List<Long> friends) {
        this.friends = friends;
    }

    public List<Long> getSkills() {
        return skills;
    }

    public void setSkills(List<Long> skills) {
        this.skills = skills;
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
