# [A walk in GraphQL](../README.md)

## Spring boot setup

The [Spring Boot GraphQL Starter](https://github.com/graphql-java-kickstart/graphql-spring-boot) combined with the [GraphQL Java Tools](https://github.com/graphql-java-kickstart/graphql-java-tools) library offer a fantastic way to get a GraphQL server running in a very short time, we need only write the code necessary for our resolvers and services.

### Requirements

* Java 17

#### AdoptOpenJDK 17 (LTS) Installation

Installers are currently available for Windows®, Linux®, and macOS® JDK and JRE packages. Installation steps are covered in the following sections:

* [Main page command line installation](https://adoptium.net/es/installation/) 
* [Windows MSI installer packages](https://adoptium.net/es/installation/windows/)
* [macOS PKG installer packages](https://adoptium.net/es/installation/macOS/)
* [Linux RPM and DEB installer packages](https://adoptium.net/es/installation/linux/)
* [Alternatively if you want to use multiples JDK and multiple tools versions administered you can use SDKMAN!](https://sdkman.io/)

### Project Plugins

* `java` Java JVM to be used for compile and run the code Gradle Plugin. (Required to use java source code)
* `org.springframework.boot` Spring Boot Gradle Plugin. (Required to use Spring boot)
* `io.spring.dependency-management` A Gradle plugin that provides Maven-like dependency management funtionallity. (Required to use dependencies in the project)

### Project Dependencies

* `spring-boot-starter` Core starter, including auto-configuration support, logging and YAML. [https://docs.spring.io/spring-boot/docs/3.1.2/reference/htmlsingle/#using.build-systems.starters]
* `spring-boot-starter-data-jpa` Starter for using Spring Data JPA with Hibernate. [https://docs.spring.io/spring-boot/docs/3.1.2/reference/htmlsingle/#using.build-systems.starters]
* `spring-boot-starter-graphql` Starter for building GraphQL applications with Spring GraphQL [https://docs.spring.io/spring-graphql/docs/current/api/] 
* `spring-boot-starter-web` Starter for building web, including RESTful, applications using Spring MVC. Uses Tomcat as the default embedded container. [https://docs.spring.io/spring-boot/docs/3.1.2/reference/htmlsingle/#using.build-systems.starters]
* Aditional Spring Boot Starters doc [https://github.com/spring-projects/spring-boot/blob/main/README.adoc#spring-boot-starters]


Spring Boot will automatically pick these up and set up the appropriate handlers to work automatically. By default, this will expose the GraphQL Service on the `/graphql` endpoint of our application and will accept POST requests containing the GraphQL Payload. This endpoint can be customised in our `application.properties` file if necessary.

### Keep in mind

* The `GraphQL Tools` library works by processing GraphQL Schema files to build the correct structure and then wires special beans to this structure.
* The `Spring Boot GraphQL starter` automatically finds these schema files, we just need to save these files with the extension ".graphqls" on the classpath.
* Query and Mutation objects are root GraphQL objects. They don’t have any associated "data" class. In such cases, the "resolver" classes should @Controller spring annotated, and @QueryMapping (for query) 
  and @MutationMapping (for mutation), respectively.
* `Beans Representing Types:` Every complex type in the GraphQL server is represented by a Java bean with corresponding spring annotation. Fields inside the Java bean will mapped onto fields with corresponging spring annotation, in the GraphQL response based on the name of the field.
* Sometimes, the value of a field is non-trivial to load. This might involve database lookups, complex calculations, or anything else. GraphQL Tools has a concept of a "Field Resolver" that is used for this purpose. 
  The field resolver is any method contatined into the "Entity Resolver" in the Spring Context that has the same name as the data bean, with the suffix @SchemaMapping spring annotation. Methods on the field resolver bean follow all of the same rules like the data bean. The method should be annotated using @SchemaMapping and into the method, the data bean instance it's  passed as first parameter.

## Run Application

### Using Gradle Wrapper

Gradle is a build automation tool known for its flexibility to build software. 
The recommended way to execute any Gradle build is with the help of the Gradle Wrapper (in short just “Wrapper”). 
The Wrapper is a script that invokes a declared version of Gradle, downloading it beforehand if necessary. 
As a result, developers can get up and running with a Gradle project quickly without having to follow manual installation processes saving your company time and money.
[https://docs.gradle.org/current/userguide/gradle_wrapper.html]

### Run the app using Gradle 
1. Open a terminal
2. go to the java exercise directory
3. run `./gradlew bootRun `(linux) or `gradlew.bat bootRun`(windows) to start the GraphQL server.

### Using the IDE (IntelliJ IDEA)

1. Open IntelliJ IDEA
2. Select "Open or Import" option
3. go to the exercise directory and choose the "java" folder
4. run DemoGraphQlApplication

## Testing GraphQL queries

To display a GUI for editing and testing GraphQL queries and mutations against the server you can open your browser and type [http://localhost:8080/playground](http://localhost:8080/playground)

## Inspect the Database

H2 database has an embedded GUI console for browsing the contents of a database and running SQL queries.
After starting the application, you can navigate to:
[http://localhost:8080/h2-console/login.jsp](http://localhost:8080/h2-console/login.jsp) and enter the following information:

* JDBC URL: jdbc:h2:mem:graphQL
* User Name: sa
* Password: \<blank>

## Learning Resources

* [GraphQL official documentation](https://graphql.org/learn/)
* [GraphQL java official documentation](https://www.graphql-java.com/documentation/getting-started)
* [Spring Boot GraphQL documentation](https://spring.io/projects/spring-graphql)
