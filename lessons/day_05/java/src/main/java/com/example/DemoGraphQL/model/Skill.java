package com.example.DemoGraphQL.model;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Objects;
import java.util.Set;

@Entity
public class Skill {
    @Id
    @Column(name = "SKILL_ID", nullable = false)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "SKILL_NAME", nullable = false)
    private String name;

    @ManyToOne
    @JoinColumn(name = "PARENT_ID")
    private Skill parent;

    @ManyToMany(mappedBy = "skills")
    private Set<Person> referents = new HashSet<>();

    public Skill() {
    }

    public Skill(String name, Skill parent) {
        this.name = name;
        this.parent = parent;
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

    public Skill getParent() {
        return parent;
    }

    public void setParent(Skill parent) {
        this.parent = parent;
    }

    public Set<Person> getReferents() {
        return referents;
    }

    public void setReferents(Set<Person> referents) {
        this.referents = referents;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Skill)) return false;
        Skill skill = (Skill) o;
        return Objects.equals(getId(), skill.getId()) &&
                Objects.equals(getName(), skill.getName()) &&
                Objects.equals(getParent(), skill.getParent());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getId(), getName(), getParent());
    }

    @Override
    public String toString() {
        return "Skill{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", parent=" + parent.name +
                '}';
    }
}
