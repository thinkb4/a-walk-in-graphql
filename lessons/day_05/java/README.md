# [A walk in GraphQL](../../../README.md)

## Day 5 exercise - Java

Read the instructions on the [Day 5 exercise](../day_05.md#exercise) definition

HINT:
* Apply the following "hint" git patch: ".../a-walk-in-graphql/lessons/day_05/java/src/main/resources/Java_day_05_Interfaces_and_Unions_hint.patch".
    * From command line: `git apply src/main/resources/Java_day_05_Interfaces_and_Unions_hint.patch`
    * From IntelliJ IDEA:
        * Open the "VCS" menu > "Apply Patch.."
        * Select the ".../a-walk-in-graphql/lessons/day_05/java/src/main/resources/Java_day_05_Interfaces_and_Unions_hint.patch" file patch > OK    

Now you need to complete the Mutation and Query resolvers with the required missing methods.

### Requirements

Java 17 is required. Please look at [here](../../../setup/java.md#requirements) if you do not have it installed on your local environment. 

### Keep in mind

* The `GraphQL Tools` library works by processing GraphQL Schema files to build the correct structure and then wires special beans to this structure. 
* The `Spring Boot GraphQL starter` automatically finds these schema files, we just need to save these files with the extension ".graphqls" on the classpath.
* Query and Mutation objects are root GraphQL objects. They don’t have any associated "data" class. In such cases, the "resolver" classes would implement `GraphQLQueryResolver` or `GraphQLMutationResolver`.
* `Beans Representing Types:` Every complex type in the GraphQL server is represented by a Java bean. Fields inside the Java bean will directly map onto fields in the GraphQL response based on the name of the field.
* Sometimes, the value of a field is non-trivial to load. This might involve database lookups, complex calculations, or anything else. GraphQL Tools has a concept of a "Field Resolver" that is used for this purpose. 
The field resolver is any bean in the Spring Context that has the same name as the data bean, with the suffix "Resolver", and that implements the `GraphQLResolver` interface. Methods on the field resolver bean follow all of the same rules as on the data bean but are also provided the data bean itself as a first parameter. If a field resolver and the data bean both have methods for the same GraphQL field then the field resolver will take precedence.

### Run the server

 See [Java setup and run server](../../../setup/java.md#run-application)

### Run queries and mutations with Playground

See [Testing GraphQL queries](../../../setup/java.md#testing-graphql-queries)

### Database

See [Inspect the Database](../../../setup/java.md#inspect-the-database)
