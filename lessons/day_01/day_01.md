# [A walk in GraphQL](/README.md)

## Day 1: Queries and Resolvers

- Queries and Resolvers
  - Query
  - Resolver
  - Exercises
    - JavaScript
    - Python
    - Java
  - Learning resources

## Query

Before jumping to the code let's break down the "Query" concept into meaningful details.

What does **query** mean in GraphQL?

Generally speaking, **a "query"** is **not "a thing"** but **a process** that involves several building blocks in order to complete the operation:

### Definition of the documents

#### 1. The GraphQL document

One or more [GraphQL Documents](http://spec.graphql.org/June2018/#sec-Language.Document) containing **executable** or **representative** definitions of a GraphQL type system must be provided.

#### 2. The representative definition (schema)

On the **representative definition** there must be the ["Root Operation definition"](http://spec.graphql.org/June2018/#sec-Root-Operation-Types) related to the operation (will see this later) we want to perform, and the definition of the data type (whenever not included on the built-in [Scalar Types](http://spec.graphql.org/June2018/#sec-Scalars)) the operation is meant to return; in this case an [Object Type](http://spec.graphql.org/June2018/#sec-Objects).

```graphql
## an Object Type definition
type Person {
  id: ID ## a Scalar type definition for the `id` field
  name: String! ## a Scalar type definition for the `name` field
}

## A Root Operation definition
type Query {
  ## a Field describing a valid operation
  getPerson: Person!
}
```

#### 3. The executable definition (request)

On the **executable definition** there must be a valid [Operation Definition](http://spec.graphql.org/June2018/#sec-Language.Operations) specifying the `OperationType` (query, mutation, subscription) and a [SelectionSet](http://spec.graphql.org/June2018/#sec-Selection-Sets) describing the [Fields](http://spec.graphql.org/June2018/#sec-Language.Fields) describing data graph we want to receive.

```graphql
query { ## OperationType
  getPerson { ## parent SelectionSet Field
    name, ## child SelectionSet Field
    id ## parent SelectionSet Field
  }
}
```

A detailed description of the query operation definition is described at **[The Anatomy of a GraphQL Query](https://blog.apollographql.com/the-anatomy-of-a-graphql-query-6dffa9e9e747)** - by Sashko Stubailo

### Execution of a query operation

Now what?
It gets way more interesting!

Once you send the request to the server with the **query operation definition** (usually using the POST verb as GraphQL doesn't quite follow the HTTP protocol), your query will go through 3 phases during execution:

#### 1. Parsing the incoming request

Since the incoming request is just a string and GraphQL can't understand it as is, it has to **parse** it into an **AST** (Abstract Syntax Tree) **in order to perform** any necessary **validation** against the document before moving forward. (Read this interesting article [Understanding the GraphQL AST](https://medium.com/@adamhannigan81/understanding-the-graphql-ast-f7f7b8e62aa4) - by Adam Hannigan)

Here's an example of our query operation definition as an AST (a part of it as it's long):

![query operation definition AST sample](assets/query_ast_example.png)

Try it yourself here: [AST Explorer](https://astexplorer.net/#/gist/bc30ff1ae53ac33743c9a2786624719c/e6b95369aed2f6d0c083cbfe66dab08bfca3b035)

#### 2. Validation

Now is time to validate the produced AST:

> GraphQL does not just verify if a request is syntactically correct, but also ensures that it is unambiguous and mistake‐free in the context of a given GraphQL schema.
>
> An invalid request is still technically executable, and will always produce a stable result as defined by the algorithms in the [Execution](http://spec.graphql.org/June2018/#sec-Execution) section, however that result may be ambiguous, surprising, or unexpected relative to a request containing validation errors, so execution should only occur for valid requests.
>
> Typically validation is performed in the context of a request immediately before execution, however a GraphQL service may execute a request without explicitly validating it if that exact same request is known to have been validated before.
>
> Source: [GraphQL spec (June 2018) - Validation](http://spec.graphql.org/June2018/#sec-Validation)

The brilliant thing of the validation phase is that, as a developer, you have to do nothing about it!! The runtime will do that for you and in case of error it'll provide you a verbose error message so you can fix it.

What?!!! 🤔

-- will it check if a field is defined on the Query Type?
-- yes
-- will it check if the field accepts a given argument?
-- yes
-- will it check if the type of the argument is defined on the Query Type?
-- yes
-- will it recursively do those verification down to the last leaf?
-- yes
-- will it ...
-- enough
❤️

#### 3. Execution

Once validation is passed, the runtime will **transverse the AST invoking the resolver for each node of the graph and produce a result** (typically a JSON document reflecting the query operation hierarchical structure )

Let's see how that might look like:

![Query graph](assets/query_graph.png)

- The **Root Query Operation Definition** node is the entry point for traversing the graph typically using a [BFS (Breadth-first search) algorithm](https://en.wikipedia.org/wiki/Breadth-first_search) for the execution, meaning getting deeper 1 level at a time.
- One level down there's the `getPerson` root field.
- **Before executing each Field**, the selection set is converted to a **grouped field set** by calling [CollectFields()](http://spec.graphql.org/June2018/#sec-Field-Collection). The [DFS (Depth‐First‐Search)](https://en.wikipedia.org/wiki/Depth-first_search) order of the field groups produced by CollectFields() is maintained through execution, **ensuring that fields appear in the executed response in a stable and predictable order, mirroring the shape of the requested query.**
- Once `getPerson`  is resolved it's the time to get 1 level down again, `name`and `id` cannot be executed until `getPerson`is done.
- Once all leaf-nodes resolve to a Scalar Type (or null), the execution is completed and the output is generated.

### The client's BIG BENEFITS

- **Self documented API:**
Your **schema** acts as an explicit contract which **determines**
  - **How** you can ask for data
  - **What** you're gonna get
  - **Shape** the response will look like
- Avoid client<->GraphQL over/under fetching by getting only what you asked for, not more nor less from GraphQL, **reducing significantly**:
  - **the traffic client<->GraphQL server**
  - **the number of operations** to handle the data required when you have more or less info than you need (e.g filtering, additional fetching)
  - **the response time**: client/server communication is a big bottle neck for your app, reducing the payload is a game changer (obviously if the bottle neck is from on your data layer under graphql, you'll need to work it out there)
  - **the app required memory**: again, you'll need to handle only the data you asked for.

### Breaking Changes

The schema **"contract"** is one of the most important, correction, we'd dare to say **IS THE MOST IMPORTANT** part of your GraphQL API, and keeping it consistent over time is critical. 
> You can change the way you solve a problem, or break down a solution into many pieces, or optimize things, granted you won't introduce a breaking change on your schema.

If you do so, you're gonna be in big trouble (people's gonna hurt you) since everyone is expecting additive changes without mayor versioning changes of the API and ideally an ad-vitam backwards compatible API.

Here's a list of things on the DO-NOT-EVER-EVER-BY-ANY-MEANS-DO list:

- Remove or rename (which technically is a removal and an addition) a type or field
- Add, remove nullability to a field
- Remove a field's arguments

## Resolvers

We talked briefly about resolvers on our [Introduction](../../introduction/introduction.md#Resolvers) chapter, so let start breaking it down.

..... WIP

**IMPORTANT NOTE:**

The execution flow is [non-deterministic](https://en.wikipedia.org/wiki/Nondeterministic_algorithm) because

- Even though **BFS** is a well known algorithm, the **execution order** for each sibling node is **NOT GUARANTEED**, it' will depend on the runtime implementation.
- Since resolvers can be asynchronous, the **resolution order** for each sibling node or an entire branch is **NOT GUARANTEED**

So, **defining** your **resolvers** as **atomic** and **pure functions** is critical. 
