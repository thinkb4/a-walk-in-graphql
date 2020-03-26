# [A walk in GraphQL](/README.md)

## Day 1: Queries and Resolvers

- Queries and Resolvers
  - Query
  - Resolver
  - Learning resources
  - Exercise
    - JavaScript
    - Python
    - Java

## Query

Before jumping to the code let's break down the "Query" concept into meaningful details.

What does **query** mean in GraphQL?

Generally speaking, **a "query"** is **not "a thing"** but **a process** that involves several building blocks in order to complete the operation:

### 1 - A GraphQL document

One or more [GraphQL Documents](http://spec.graphql.org/June2018/#sec-Language.Document) containing **executable** or **representative** definitions of a GraphQL type system.

### 2 - A representative definition (schema)

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

### 3 - An executable definition (request)

On the **executable definition** there must be a valid [Operation Definition](http://spec.graphql.org/June2018/#sec-Language.Operations) specifying the `OperationType` (query, mutation, subscription) and a [SelectionSet](http://spec.graphql.org/June2018/#sec-Selection-Sets) describing the [Fields](http://spec.graphql.org/June2018/#sec-Language.Fields) describing data graph we want to receive.

```graphql
query { ## OperationType
  getPerson { ## parent SelectionSet Field
    name, ## child SelectionSet Field
    id ## parent SelectionSet Field
  }
}
```

A detailed description of the query operation definition is described at **"The Anatomy of a GraphQL Query"**

Now what?
It gets way more interesting!

Once you send the request to the server with the **query operation definition** (usually using the POST verb as GraphQL doesn't quite follow the HTTP protocol), your query will go through 3 phases:

### 1 - Parsing the incoming request

Since the incoming request is just a string and GraphQL can't understand it as is, it hast to **parse** into an **AST** (Abstract Syntax Tree)it before moving forward and **in order to perform** any necessary **validation** against the document. (Read this interesting article [Understanding the GraphQL AST](https://medium.com/@adamhannigan81/understanding-the-graphql-ast-f7f7b8e62aa4) - by Adam Hannigan)

Here an example of our query operation definition as an AST (a part of it as it is long):

![query operation definition AST sample](assets/query_ast_example.png)

Try it yourself here: [AST Explorer](https://astexplorer.net/#/gist/bc30ff1ae53ac33743c9a2786624719c/e6b95369aed2f6d0c083cbfe66dab08bfca3b035)

### 2 - Validation

Now is time to validate the produced AST:

> GraphQL does not just verify if a request is syntactically correct, but also ensures that it is unambiguous and mistake‐free in the context of a given GraphQL schema.
>
> An invalid request is still technically executable, and will always produce a stable result as defined by the algorithms in the Execution section, however that result may be ambiguous, surprising, or unexpected relative to a request containing validation errors, so execution should only occur for valid requests.
>
> Typically validation is performed in the context of a request immediately before execution, however a GraphQL service may execute a request without explicitly validating it if that exact same request is known to have been validated before.
>
> Source: [GraphQL spec (June 2018) - Validation](http://spec.graphql.org/June2018/#sec-Validation)

### 3 - Execution

Once validation is passed, the runtime will **transverse the AST invoking the resolver for each node of the graph and produce a result** (typically a JSON document reflecting the query operation hierarchical structure )

Let's see how that might look like:

![Query graph](assets/query_graph.png)

- The **Root Query Operation Definition** node is the entry point for traversing the graph typically using a [BFS (Breadth-first search) algorithm](https://en.wikipedia.org/wiki/Breadth-first_search), meaning getting deeper 1 level at a time.
- One level down there's the `getPerson` root field.
- Once `getPerson`  is resolved it's the time to get 1 level down again, `name`and `id` cannot be executed until `getPerson`is done.
- Once all leaf-nodes resolve to a Scalar Type (or null), the execution is completed and the output is generated.

**IMPORTANT NOTE:**

The execution flow is [non-deterministic](https://en.wikipedia.org/wiki/Nondeterministic_algorithm) because

- Even though BFS is a well known algorithm, the **execution order** for each sibling node is **NOT GUARANTEED**, it' will depend on the runtime implementation.
- Since resolvers can be asynchronous, the **resolution order** for each sibling node or an entire branch is **NOT GUARANTEED**

So, **defining** your **resolvers** as **atomic** and **pure functions** is critical. 

## Resolvers

