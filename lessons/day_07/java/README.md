# [A walk in GraphQL](../../../README.md)

## Day 7 exercise - Java

Read the instructions on the [Day 7 exercise](../day_07.md#exercise) definition

### Requirements

Java 17 is required. Please look at [here](../../../setup/java.md#requirements) if you do not have it installed on your local environment. 

HINTS: 

* Exceptions (https://docs.spring.io/spring-graphql/docs/current/reference/html/#execution.exceptions)
* Spring for GraphQL registers a DataFetcherExceptionHandler that provides default handling and enables the DataFetcherExceptionResolver contract.
* Apply the following "hint" git patch to get an example of defending and informative error handling strategies. Implemented 
  three ways to treat the exceptions. (The patches to apply to the source code are mutually exclusive. They are each one just an example how can be treat the exceptions.)
  * a.- Using Controller advice and graphql spring-boot annotations. 
         Catch the exception globally across controllers.
  * b.- Using a CustomExceptionResolver extending DataFetcherExceptionResolverAdapter provided by graphql spring boot implementations. 
         Catch the exceptions from a java component programmatically globally in the application context.
  * c.- Using spring-boot annotations for catch specific entity methods exceptions into a controller.
         Catch the exceptions for a user defined only specific controller.
* From command line: 
  * a.- git apply  src/main/resources/Java_day_07_Error_Handling_Using_ControllerAdvice_GraphQlExceptionHandler_across_controllers.patch
  * b.- git apply  src/main/resources/Java_day_07_Error_Handling_Using_CustomExceptionResolver.patch
  * c.- git apply  src/main/resources/Java_day_07_Error_Handling_Using_GraphQlExceptionHandler_for_specific_controller.patch
* From IntelliJ IDEA:
     * Open the "VCS" menu > "Apply Patch.."
     * Select the:
       * a.- ".../a-walk-in-graphql/lessons/day_07/java/src/main/resources/Java_day_07_Error_Handling_Using_ControllerAdvice_GraphQlExceptionHandler_across_controllers.patch" file patch > OK
       * b.- ".../a-walk-in-graphql/lessons/day_07/java/src/main/resources/Java_day_07_Error_Handling_Using_CustomExceptionResolver.patch" file patch > OK
       * c.- ".../a-walk-in-graphql/lessons/day_07/java/src/main/resources/Java_day_07_Error_Handling_Using_GraphQlExceptionHandler_for_specific_controller.patch" file patch > OK

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
