package com.example.DemoGraphQL.input;

import com.example.DemoGraphQL.model.EyeColor;

public class InputPerson {
    private Long id;
    private Integer age;
    private EyeColor eyeColor;
    private Long favSkill;

    public InputPerson() {
    }

    public InputPerson(Long id, Integer age, EyeColor eyeColor, Long favSkill) {
        this.id = id;
        this.age = age;
        this.eyeColor = eyeColor;
        this.favSkill = favSkill;
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
}
