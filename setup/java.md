# [A walk in GraphQL](../README.md)

## Spring boot setup

The [Spring Boot GraphQL Starter](https://github.com/graphql-java-kickstart/graphql-spring-boot) combined with the [GraphQL Java Tools](https://github.com/graphql-java-kickstart/graphql-java-tools) library offer a fantastic way to get a GraphQL server running in a very short time, we need only write the code necessary for our resolvers and services.

### Requirements

* Java 17

#### AdoptOpenJDK 17 (LTS) Installation

Installers are currently available for Windows®, Linux®, and macOS® JDK and JRE packages. Installation steps are covered in the following sections:

* [Windows MSI installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#windows-msi)
* [macOS PKG installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#macos-pkg)
* [Linux RPM and DEB installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#linux-pkg)
* [Alternatively if you want to use multiples JDK and multiple tools versions administered you can use SDKMAN!](https://sdkman.io/)

### Project Plugins

* `java` Java JVM to be used for compile an run the code. (Required to use java source code)
* `org.springframework.boot` Spring boot frework ot be used into the project. (Required to use spring boot)
* `io.spring.dependency-management` (Required to use dependencies into spring framework)

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
* Query and Mutation objects are root GraphQL objects. They don’t have any associated "data" class. In such cases, the "resolver" classes can be @Component or @Controller spring annotated, and @QueryMapping (for query) 
  and @MutationMapping (for mutation), respectively.
* `Beans Representing Types:` Every complex type in the GraphQL server is represented by a Java bean with corresponding spring annotation. Fields inside the Java bean will mapped onto fields with corresponging spring annotation, in the GraphQL response based on the name of the field.
* Sometimes, the value of a field is non-trivial to load. This might involve database lookups, complex calculations, or anything else. GraphQL Tools has a concept of a "Field Resolver" that is used for this purpose. 
  The field resolver is any method contatined into the "Entity Resolver" in the Spring Context that has the same name as the data bean, with the suffix @SchemaMapping spring annotation. Methods on the field resolver bean follow all of the same rules as on the data bean but are also provided the data bean itself as a first parameter. 

## Run Application

### Using Gradle Wrapper

The Gradle Wrapper is an excellent choice for projects that need a specific version depdency from Maven central or custom respositories.

### Run the app using Gradle 
1. Open a terminal
2. go to the java exercise directory
3. run `./gradle run `(linux) or `gradle.bat run`(windows) to start the GraphQL server.

### Using the IDE (IntelliJ IDEA)

1. Open IntelliJ IDEA
2. Select "Open or Import" option
3. go to the exercise directory and choose the "java" folder
4. run DemoGraphQlApplication

## Testing GraphQL queries

To display a GUI for editing and testing GraphQL queries and mutations against the server you can open your browser and type [http://localhost:8080/graphiq](http://localhost:8080/graphiq)

## Inspect the Database

H2 database has an embedded GUI console for browsing the contents of a database and running SQL queries.
After starting the application, you can navigate to:
[http://localhost:8080/h2-console/login.jsp](http://localhost:8080/h2-console/login.jsp) and enter the following information:

* JDBC URL: jdbc:h2:mem:graphQL
* User Name: sa
* Password: \<blank>

## Learning Resources

* [GrapQL official documentation](https://graphql.org/learn/)
* [graphql-java documentation](https://www.graphql-java.com/documentation/)
* [GraphQL Java Kickstart - Springboot](https://www.graphql-java-kickstart.com/spring-boot/)
* [GraphQL Java Kickstart - Tools](https://www.graphql-java-kickstart.com/tools/)
