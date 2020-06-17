package com.example.DemoGraphQL.model;

public enum EyeColor {

    BLUE("blue"),
    GREEN("green"),
    BROWN("brown"),
    BLACK("black");

    public final String label;

    private EyeColor(String label) {
        this.label = label;
    }

    public String getLabel() {
        return label;
    }

    public static EyeColor fromLabel(String label) {
        for (EyeColor eyeColor : EyeColor.values()) {
            if (eyeColor.label.equalsIgnoreCase(label)) {
                return eyeColor;
            }
        }
        throw new IllegalArgumentException("No constant with label " + label + " found");
    }
}
