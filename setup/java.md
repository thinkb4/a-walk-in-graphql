# [A walk in GraphQL](/README.md)

## Spring boot setup

The [Spring Boot GraphQL Starter](https://github.com/graphql-java-kickstart/graphql-spring-boot) combined with the [GraphQL Java Tools](https://github.com/graphql-java-kickstart/graphql-java-tools) library offer a fantastic way to get a GraphQL server running in a very short time, we need only write the code necessary for our resolvers and services.

### Requirements

* Java 1.8

#### AdoptOpenJDK 8 (LTS) Installation

Installers are currently available for Windows®, Linux®, and macOS® JDK and JRE packages. Installation steps are covered in the following sections:

* [Windows MSI installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#windows-msi)
* [macOS PKG installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#macos-pkg)
* [Linux RPM and DEB installer packages](https://adoptopenjdk.net/installation.html?variant=openjdk8&jvmVariant=hotspot#linux-pkg)

### Project Dependencies

* `graphql-spring-boot-starter` to turn your boot application into GraphQL server. (see [graphql-java-servlet](https://github.com/graphql-java-kickstart/graphql-java-servlet))
* `playground-spring-boot-starter` to embed GraphQL Playground tool for schema introspection and query debugging (see [GraphQL Playground](https://github.com/prisma/graphql-playground))
* `graphql-java-tools` a schema-first tool that allows us to use the GraphQL schema language to build your graphql-java schema (see [graphql-java-tools](https://github.com/graphql-java-kickstart/graphql-java-tools)). Inspired by apollo [graphql-tools](https://github.com/apollographql/graphql-tools), it parses the given GraphQL schema and allows you to BYOO (bring your own object) to fill in the implementations.

Spring Boot will automatically pick these up and set up the appropriate handlers to work automatically. By default, this will expose the GraphQL Service on the `/graphql` endpoint of our application and will accept POST requests containing the GraphQL Payload. This endpoint can be customised in our `application.properties` file if necessary.

### Keep in mind

* The `GraphQL Tools` library works by processing GraphQL Schema files to build the correct structure and then wires special beans to this structure. 
* The `Spring Boot GraphQL starter` automatically finds these schema files, we just need to save these files with the extension ".graphqls" on the classpath.
* Query and Mutation objects are root GraphQL objects. They don’t have any associated "data" class. In such cases, the "resolver" classes would implement `GraphQLQueryResolver` or `GraphQLMutationResolver`.
* `Beans Representing Types:` Every complex type in the GraphQL server is represented by a Java bean. Fields inside the Java bean will directly map onto fields in the GraphQL response based on the name of the field.
* Sometimes, the value of a field is non-trivial to load. This might involve database lookups, complex calculations, or anything else. GraphQL Tools has a concept of a "Field Resolver" that is used for this purpose. 
The field resolver is any bean in the Spring Context that has the same name as the data bean, with the suffix "Resolver", and that implements the `GraphQLResolver` interface. Methods on the field resolver bean follow all of the same rules as on the data bean but are also provided the data bean itself as a first parameter. If a field resolver and the data bean both have methods for the same GraphQL field then the field resolver will take precedence.

## Run Application

### Using Maven Wrapper

The Maven Wrapper is an excellent choice for projects that need a specific version of Maven (or for users that don't want to install Maven at all).

1. Open a terminal
2. go to the java exercise directory
3. run `./mvnw spring-boot:run`(linux) or `mvnw.cmd spring-boot:run`(windows) to start the GraphQL server.

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

* [GrapQL official documentation](https://graphql.org/learn/)
* [graphql-java documentation](https://www.graphql-java.com/documentation/)
* [GraphQL Java Kickstart - Springboot](https://www.graphql-java-kickstart.com/spring-boot/)
* [GraphQL Java Kickstart - Tools](https://www.graphql-java-kickstart.com/tools/)
