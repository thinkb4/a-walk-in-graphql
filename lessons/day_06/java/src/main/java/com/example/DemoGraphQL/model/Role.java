package com.example.DemoGraphQL.model;

public enum Role {

    DEVELOPER("developer"),
    SDET("sdet"),
    TEAM_LEAD("team_lead");

    public final String label;

    private Role(String label) {
        this.label = label;
    }

    public String getLabel() {
        return label;
    }

    public static Role fromLabel(String label) {
        for (Role role : Role.values()) {
            if (role.label.equalsIgnoreCase(label)) {
                return role;
            }
        }
        throw new IllegalArgumentException("No constant with label " + label + " found");
    }
}
