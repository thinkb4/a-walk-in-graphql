package com.example.DemoGraphQL.model;

import jakarta.persistence.*;

@Entity
@DiscriminatorValue("Engineer")
public class Engineer extends Person {

    @Column(name = "EMPLOYEE_ID")
    private Long employeeId;

    @Column(name = "ROLE")
    @Enumerated(EnumType.STRING)
    private Role role;

    @Column(name = "GRADE")
    @Enumerated(EnumType.STRING)
    private Grade grade;

    public Engineer() {
    }

    public Engineer(String name, String surname, String email, EyeColor eyeColor, Integer age) {
        super(name, surname, email, eyeColor, age);
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

    public Long getEmployeeId() {
        return employeeId;
    }

    public void setEmployeeId(Long employeeId) {
        this.employeeId = employeeId;
    }
}
