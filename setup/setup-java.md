# [A walk in graphql with Spring boot]((/setup-java.md))

## About the Project
The [Spring Boot GraphQL Starter](https://github.com/graphql-java-kickstart/graphql-spring-boot) combined with the [GraphQL Java Tools](https://github.com/graphql-java-kickstart/graphql-java-tools) library offer a fantastic way to get a GraphQL server running in a very short time, we need only write the code necessary for our resolvers and services.

#### Project Dependencies
* `graphql-spring-boot-starter` to turn your boot application into GraphQL server. (see [graphql-java-servlet](https://github.com/graphql-java-kickstart/graphql-java-servlet))
* `graphiql-spring-boot-starter` to embed GraphiQL tool for schema introspection and query debugging. (see [graphiql](https://github.com/graphql/graphiql))
* `graphql-java-tools` a schema-first tool that allows us to use the GraphQL schema language to build your graphql-java schema (see [graphql-java-tools](https://github.com/graphql-java-kickstart/graphql-java-tools)). Inspired by apollo [graphql-tools](https://github.com/apollographql/graphql-tools), it parses the given GraphQL schema and allows you to BYOO (bring your own object) to fill in the implementations.

Spring Boot will automatically pick these up and set up the appropriate handlers to work automatically. By default, this will expose the GraphQL Service on the `/graphql` endpoint of our application and will accept POST requests containing the GraphQL Payload. This endpoint can be customised in our `application.properties` file if necessary.

#### Run Application
1. Open a terminal
2. go to the java exercise directory
3. run `./mvnw spring-boot:run`(linux) or `mvnw.cmd spring-boot:run`(windows) to start the GraphQL server.

#### Testing GraphQL queries
To display a GUI for editing and testing GraphQL queries and mutations against the server you can open your browser and type [http://localhost:8080/playground](http://localhost:8080/playground)

#### Database
H2 database has an embedded GUI console for browsing the contents of a database and running SQL queries.
After starting the application, you can navigate to:
[http://localhost:8080/h2-console/login.jsp](http://localhost:8080/h2-console/login.jsp) and enter the following information:
- JDBC URL: jdbc:h2:mem:graphQL
- User Name: sa
- Password: <blank>



## A little theory
#### Writing the Schema
The GraphQL Tools library works by processing GraphQL Schema files to build the correct structure and then wires special beans to this structure. The Spring Boot GraphQL starter automatically finds these schema files, we just need to save these files with the extension “.graphqls” on the classpath.
We can also have as many of these files as desired, so we can split the scheme up into modules as desired. The one requirement is that there must be exactly one root query, and up to one root mutation. This can not be split across files, unlike the rest of the scheme. This is a limitation of the GraphQL Schema definition itself, and not of the Java implementation.

#### Root Query Resolver
Query or Mutation objects are root GraphQL objects. They don’t have any associated data class. In such cases, the resolver classes would implement GraphQLQueryResolver or GraphQLMutationResolver. These resolvers will be searched for methods that map to fields in their respective root types.

The names of the method must be one of the following, in this order:

    1. <field>
    2. is<field> – only if the field is of type Boolean
    3. get<field>

#### Beans Representing Types
Every complex type in the GraphQL server is represented by a Java bean – whether loaded from the root query or from anywhere else in the structure. Fields inside the Java bean will directly map onto fields in the GraphQL response based on the name of the field.

Sometimes, the value of a field is non-trivial to load. This might involve database lookups, complex calculations, or anything else. GraphQL Tools has a concept of a "Field Resolver" that is used for this purpose. These are Spring beans that can provide values in place of the data bean.
The field resolver is any bean in the Spring Context that has the same name as the data bean, with the suffix "Resolver", and that implements the "GraphQLResolver" interface. Methods on the field resolver bean follow all of the same rules as on the data bean but are also provided the data bean itself as a first parameter.
If a field resolver and the data bean both have methods for the same GraphQL field then the field resolver will take precedence.


## Learning Resources
- [GrapQL official documentation](https://graphql.org/learn/)
- [graphql-java documentation](https://www.graphql-java.com/documentation/)
- [Getting started with spring-boot](https://www.graphql-java.com/tutorials/getting-started-with-spring-boot/)
- [GraphQL Java Kickstart](https://www.graphql-java-kickstart.com/spring-boot/)
- [Building a graphql server with spring boot (By Esteban Herrera - PluralSight)](https://www.pluralsight.com/guides/building-a-graphql-server-with-spring-boot)
- [Getting Started with GraphQL and Spring Boot (www.baeldung.com)](https://www.baeldung.com/spring-graphql)
