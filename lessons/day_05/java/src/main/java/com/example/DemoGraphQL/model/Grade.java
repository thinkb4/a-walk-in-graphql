package com.example.DemoGraphQL.model;

public enum Grade {

    TRAINEE("trainee"),
    JUNIOR("junior"),
    SENIOR("senior");

    public final String label;

    private Grade(String label) {
        this.label = label;
    }

    public String getLabel() {
        return label;
    }

    public static Grade fromLabel(String label) {
        for (Grade grade : Grade.values()) {
            if (grade.label.equalsIgnoreCase(label)) {
                return grade;
            }
        }
        throw new IllegalArgumentException("No constant with label " + label + " found");
    }
}
