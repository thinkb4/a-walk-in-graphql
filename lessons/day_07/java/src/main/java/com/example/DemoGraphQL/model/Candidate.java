package com.example.DemoGraphQL.model;

import jakarta.persistence.*;

@Entity
@DiscriminatorValue("Candidate")
public class Candidate extends Person {

    @Column(name = "TARGET_ROLE")
    @Enumerated(EnumType.STRING)
    private Role targetRole;

    @Column(name = "TARGET_GRADE")
    @Enumerated(EnumType.STRING)
    private Grade targetGrade;

    public Candidate() {
    }

    public Candidate(String name, String surname, String email, EyeColor eyeColor, Integer age) {
        super(name, surname, email, eyeColor, age);
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

}
