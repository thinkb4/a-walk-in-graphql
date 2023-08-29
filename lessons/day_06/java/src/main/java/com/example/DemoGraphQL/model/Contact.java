package com.example.DemoGraphQL.model;

import jakarta.persistence.DiscriminatorValue;
import jakarta.persistence.Entity;

@Entity
@DiscriminatorValue("Contact")
public class Contact extends Person {

}
