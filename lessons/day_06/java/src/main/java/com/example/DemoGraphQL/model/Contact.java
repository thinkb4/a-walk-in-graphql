package com.example.DemoGraphQL.model;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Contact")
public class Contact extends Person {

}
