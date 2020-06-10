package com.example.DemoGraphQL.input;

public class InputGlobalSearch {
    private String name;

    public InputGlobalSearch() {
    }

    public InputGlobalSearch(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
