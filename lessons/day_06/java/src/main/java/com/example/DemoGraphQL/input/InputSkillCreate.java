package com.example.DemoGraphQL.input;

public class InputSkillCreate {
    private Long parent;
    private String name;

    public InputSkillCreate() {
    }

    public InputSkillCreate(Long parent, String name) {
        this.parent = parent;
        this.name = name;
    }

    public Long getParent() {
        return parent;
    }

    public void setParent(Long parent) {
        this.parent = parent;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
