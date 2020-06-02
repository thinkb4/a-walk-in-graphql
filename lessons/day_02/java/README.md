# [A walk in graphql with Spring boot]((/README.md))
The [Spring Boot GraphQL Starter](https://github.com/graphql-java-kickstart/graphql-spring-boot) combined with the [GraphQL Java Tools](https://github.com/graphql-java-kickstart/graphql-java-tools) library offer a fantastic way to get a GraphQL server running in a very short time, we need only write the code necessary for our resolvers.

## Day 2 exercise - Java
(Read the instructions on the [Day 2 exercise](../day_02.md#exercise) definition)

### Run Application

1. Open a terminal
2. go to the java exercise directory
3. run `./mvnw spring-boot:run`(linux) or `mvnw.cmd spring-boot:run`(windows) to start the GraphQL server.

### Testing GraphQL queries
To display a GUI for editing and testing GraphQL queries and mutations against the server you can open your browser and type [http://localhost:8080/playground](http://localhost:8080/playground)

### Database
H2 database has an embedded GUI console for browsing the contents of a database and running SQL queries.
After starting the application, you can navigate to:
[http://localhost:8080/h2-console/login.jsp](http://localhost:8080/h2-console/login.jsp) and enter the following information:
- JDBC URL: jdbc:h2:mem:graphQL
- User Name: sa
- Password: <blank>
