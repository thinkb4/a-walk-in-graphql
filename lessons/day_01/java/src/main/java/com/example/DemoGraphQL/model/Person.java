package com.example.DemoGraphQL.model;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Objects;
import java.util.Set;

@Entity
public class Person {
    @Id
    @Column(name = "ID", nullable = false)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "NAME", nullable = false)
    private String name;

    @Column(name = "SURNAME", nullable = false)
    private String surname;

    @Column(name = "EMAIL", nullable = false)
    private String email;

    @Column(name = "EYECOLOR", nullable = false)
    private String eyeColor;

    @Column(name = "AGE", nullable = false)
    private int age;

    @ManyToMany
    @JoinTable(
            name = "PERSON_FRIEND",
            joinColumns = @JoinColumn(name = "PERSON_ID"),
            inverseJoinColumns = @JoinColumn(name = "FRIEND_ID"))
    private Set<Person> friends = new HashSet<>();

    @ManyToMany(mappedBy = "friends")
    private Set<Person> friendOf = new HashSet<>();

    @ManyToMany
    @JoinTable(
            name = "PERSON_SKILLS",
            joinColumns = {@JoinColumn(name = "PERSON_ID")},
            inverseJoinColumns = {@JoinColumn(name = "SKILL_ID")}
    )
    private Set<Skill> skills = new HashSet<>();

    @OneToOne
    @JoinColumn(name = "FAV_SKILL_ID")
    private Skill favSkill;

    public Person() {
    }

    public Person(String name, String surname, String email, String eyeColor, int age) {
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.eyeColor = eyeColor;
        this.age = age;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
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

    public String getEyeColor() {
        return eyeColor;
    }

    public void setEyeColor(String eyeColor) {
        this.eyeColor = eyeColor;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public Set<Person> getFriends() {
        return friends;
    }

    public void setFriends(Set<Person> friends) {
        this.friends = friends;
    }

    public Set<Person> getFriendOf() {
        return friendOf;
    }

    public void setFriendOf(Set<Person> friendOf) {
        this.friendOf = friendOf;
    }

    public Set<Skill> getSkills() {
        return skills;
    }

    public void setSkills(Set<Skill> skills) {
        this.skills = skills;
    }

    public Skill getFavSkill() {
        return favSkill;
    }

    public void setFavSkill(Skill favSkill) {
        this.favSkill = favSkill;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Person)) return false;
        Person person = (Person) o;
        return getAge() == person.getAge() &&
                Objects.equals(getId(), person.getId()) &&
                Objects.equals(getName(), person.getName()) &&
                Objects.equals(getSurname(), person.getSurname()) &&
                Objects.equals(getEmail(), person.getEmail()) &&
                Objects.equals(getEyeColor(), person.getEyeColor()) &&
                Objects.equals(getFriends(), person.getFriends()) &&
                Objects.equals(getFriendOf(), person.getFriendOf()) &&
                Objects.equals(getSkills(), person.getSkills()) &&
                Objects.equals(getFavSkill(), person.getFavSkill());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getId(), getName(), getSurname(), getEmail(), getEyeColor(), getAge(), getFriends());
    }

    @Override
    public String toString() {
        return "Person{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", surname='" + surname + '\'' +
                ", email='" + email + '\'' +
                ", eyeColor='" + eyeColor + '\'' +
                ", age=" + age +
                '}';
    }
}
