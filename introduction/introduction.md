# [A walk in GraphQL](/README.md)

## Introduction

- What's a graph?
- What's GraphQL and what's the `graph` part all about
- GraphQL vs RESTful
- Schema Basics
  - SDL - Schema Definition Language
  - Named Types
  - Input and Output Types
  - Root operation Types
    - Query
    - Mutation
    - Subscription
- Resolvers
- Learning resources

## What's a graph?

Without being to strict or too loose, we can say a **graph** is an **abstract model** to describe at least a **pair of objects and their relationship**.

> In mathematics, and more specifically in [graph theory](https://en.wikipedia.org/wiki/Graph_theory), a **graph** is a structure amounting to a set of objects in which some pairs of the objects are in some sense "related". The objects correspond to mathematical abstractions called [vertices](https://en.wikipedia.org/wiki/Vertex_(graph_theory)) (also called nodes or points) and each of the related pairs of vertices is called an [edge](https://en.wikipedia.org/wiki/Edge_(graph_theory)) (also called link or line). Typically, a graph is depicted in diagrammatic form as a set of dots or circles for the vertices, joined by lines or curves for the edges. Graphs are one of the objects of study in discrete mathematics.
>
> The edges may be directed or undirected. For example, if the vertices represent people at a party, and there is an edge between two people if they shake hands, then this graph is undirected because any person *A* can shake hands with a person *B* only if *B* also shakes hands with *A*. In contrast, if any edge from a person *A* to a person *B* corresponds to *A* owes money to *B*, then this graph is directed, because owing money is not necessarily reciprocated. The former type of graph is called an **undirected graph** while the latter type of graph is called a **directed graph**.
>
> Source: [Wikipedia - Graph (discrete mathematics)](https://en.wikipedia.org/wiki/Graph_(discrete_mathematics))

|Diagrammatic form |Description|
|:-:|:-:|
|<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bf/Undirected.svg/220px-Undirected.svg.png" width="220"  />|A graph with three vertices and three edges.|
|<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Directed.svg/220px-Directed.svg.png" width="220"  />| A directed graph with three vertices and four directed edges (the double arrow represents an edge in each direction). |

## What's GraphQL and what's the `graph` part all about

Let's try to go from the surface down to the rabbit hole.

### The interface

Since repeating again and again the *"unified interface to access data from different sources"* thing ( we're polluting Google results with that phrase, lame ) feels kinda dumb at this point; let's try another analogy.

Imagine a **[shape sorter toy](https://www.google.com/search?q=shape+sorter&tbm=isch)**, that's your contract.

1. Anything going through (in/out) the shape sorter MUST match one of the defined shapes.
2. Everything is described in terms of a shape, a shape's property is a shape as well.
3. It doesn't care about how you get the blocks, granted #1
4. It doesn't care how you compose the blocks, granted #1

Without going in details with things like partial matches, etc., the above statement makes clear that.

- From the **client side** you'll know **upfront** the **shapes** you can **request** and **retrieve**.
- From the **server side** you'll know **upfront** the **shapes** you will be **requested** and have to **deliver**.

### The relationships (`graph`)

Here it comes the `graph` thing.

If you go to the [Thinking in Graphs](https://graphql.org/learn/thinking-in-graphs/) section at [graphql.org](https://graphql.org/) you'll see the phrase **"It's Graphs All the Way Down"** with a citation to the ["Turtles all the way down"](https://en.wikipedia.org/wiki/Turtles_all_the_way_down) expression referring to the infinite regress problem. Despite the philosophical implications that you can explore if you want, what we can derive from that phrase is that it's all about graphs!

Let's see a small example:

```graphql

type Person {
  id: ID!
  name: String!
  surname: String!
  email: String!
  friend: Person
}

```

`Person` is clearly a `vertex` or `node`, which has many properties related to it, hence we can describe the properties as `nodes` and the relationships as `edges`!!! All the way down! until no edges are found and all the data structure is returned.  

Also did you noticed that `friend` property ( only one possible friend? that's cruel ) can hold a `Person`?  

That means:

```txt
+ A [friend of]-> B [friend of]-> C +
|                                   |
+ <----------[friend of]------------+
```

Got the point?  
We could perform the following query:

```graphql
me {
  name
  friend {
    name
    friend {
      name
      friend {
        name
        friend {
          name
        }
      }
    }
  }
}
```

and obtain

```json
{
  "data": {
    "me": {
      "name": "A",
      "friend": {
        "name": "B",
        "friend": {
          "name": "C",
          "friend": {
            "name": "A",
            "friend": {
              "name": "B"
            }
          }
        }
      }
    }
  }
}
```

All the way down until the max depth defined on the server is reached (that'd be the only limitation)

Let's take a look at the best explanation about this topic we've found:  [GraphQL Concepts Visualized](https://blog.apollographql.com/the-concepts-of-graphql-bc68bd819be3)  by [Dhaivat Pandya](https://blog.apollographql.com/@dpandya)

## GraphQL vs RESTful

There are a [lot of documents on the web](https://www.google.com/search?q=restful+vs+graphql) describing the "differences" between them, some are very precise, detailed and descriptive, but a lot are misleading or inaccurate (that's why the quotes). It's extremely important, before moving forward, to make some of those statements clear.

It:

- WON'T lower down you're database load, it doesn't care how/where you persist your data
- WON'T lower down the network traffic between your graphQL server and your persistence infrastructure per-se, that's business logic and not graphql ... but
- WILL tend to lower down the network traffic between the clients and the graphQL server
- WON'T solve your messy/inconsistent data structure or your legacy mixed persistance infrastructure ... but
- WILL help you provide a consistent, unified and scalable data structure from graphql to the clients.

That said, we'd like to share with you some enlightening insights from a Facebook Engineering platform's post, see an extract below.

> We evaluated our options for delivering News Feed data to our mobile apps, including RESTful server resources and FQL tables (Facebook’s SQL-like API). **We were frustrated with the differences between the data we wanted to use in our apps and the server queries they required**. We don’t think of data in terms of resource URLs, secondary keys, or join tables; we think about it in terms of a graph of objects and the models we ultimately use in our apps like NSObjects or JSON.
>
> There was also a considerable amount of code to write on both the server to prepare the data and on the client to parse it. This frustration inspired a few of us to start the project that ultimately became GraphQL. GraphQL was our opportunity to rethink mobile app data-fetching from the perspective of product designers and developers. It moved the focus of development to the client apps, where designers and developers spend their time and attention.
>
> Source: [GraphQL: A data query language](https://engineering.fb.com/core-data/graphql-a-data-query-language/)

It's clear that the perspective shift from the persistance to the consumption point of view is at the GraphQL DNA, and that's VERY CLEAR in the spec!

> **GraphQL has a number of design principles:**
>
> **Hierarchical:** Most product development today involves the creation and manipulation of view hierarchies. To achieve congruence with the structure of these applications, a GraphQL query itself is structured hierarchically. The query is shaped just like the data it returns. It is a natural way for clients to describe data requirements.
>
> **Product‐centric:** GraphQL is unapologetically driven by the requirements of views and the front‐end engineers that write them. GraphQL starts with their way of thinking and requirements and builds the language and runtime necessary to enable that.
>
> **Strong‐typing:** Every GraphQL server defines an application‐specific type system. Queries are executed within the context of that type system. Given a query, tools can ensure that the query is both syntactically correct and valid within the GraphQL type system before execution, i.e. at development time, and the server can make certain guarantees about the shape and nature of the response.
>
> **Client‐specified queries:** Through its type system, a GraphQL server publishes the capabilities that its clients are allowed to consume. It is the client that is responsible for specifying exactly how it will consume those published capabilities. These queries are specified at field‐level granularity. In the majority of client‐server applications written without GraphQL, the server determines the data returned in its various scripted endpoints. A GraphQL query, on the other hand, returns exactly what a client asks for and no more.
>
> **Introspective:** GraphQL is introspective. A GraphQL server’s type system must be queryable by the GraphQL language itself, as will be described in this specification. GraphQL introspection serves as a powerful platform for building common tools and client software libraries.
>
> Source: [GraphQL spec (June 2018 Edition)](http://spec.graphql.org/June2018/#sec-Overview)

And even tough it tends to be agnostic/silent regarding many aspects it's still strongly opinionated regarding best practices as you can see here:

- [Serving over HTTP](https://graphql.org/learn/serving-over-http/)
- [Response data type](https://graphql.org/learn/best-practices/#json-with-gzip)
- [API versioning](https://graphql.org/learn/best-practices/#versioning)
- [Nullability](https://graphql.org/learn/best-practices/#nullability)
- [Pagination](https://graphql.org/learn/best-practices/#pagination)

## Schema Basics

### SDL - Schema Definition Language

Even though GraphQL was **internally used since 2012** and **publickly released in 2015** we had to wait until [2018](https://github.com/graphql/graphql-spec/pull/90#event-1465541388) for the **Schema Definition Language** (SDL) to became part of the specification. ( Source: [Wikipedia GraphQL](https://en.wikipedia.org/wiki/GraphQL))

The Schema is a text document that follows the SDL syntax defined on the GraphQL specification, essentially a contract that **declares**, in the form of **Types**, the **shape of your data graph** and the **operations** you can perform with it.

For the time being (March, 2020) type definitions belong to one of the following categories.

### Named Types

- [Scalar Type](http://spec.graphql.org/June2018/#sec-Scalars)
Scalar types represent primitive leaf values in a GraphQL type system. GraphQL responses take the form of a hierarchical tree; the leaves on these trees are GraphQL scalars.
  - [Int](http://spec.graphql.org/June2018/#sec-Int)
  Signed 32‐bit integer
  - [Float](http://spec.graphql.org/June2018/#sec-Float)
  Signed double-precision floating-point value
  - [String](http://spec.graphql.org/June2018/#sec-String)
  UTF‐8 character sequence
  - [Boolean](http://spec.graphql.org/June2018/#sec-Boolean)
  true or false
  - [ID](http://spec.graphql.org/June2018/#sec-ID)
  Serialized in the same way as a String; however, it is not intended to be human‐readable.
- [Enum Type](http://spec.graphql.org/June2018/#EnumTypeDefinition)
GraphQL Enum types, like scalar types, also represent leaf values in a GraphQL type system. However Enum types describe the set of possible values.
- [Object Type](http://spec.graphql.org/June2018/#sec-Objects)
While Scalar types describe the leaf values of these hierarchical queries, Objects describe the intermediate levels.
- [Input Object Type](http://spec.graphql.org/June2018/#InputObjectTypeDefinition)
A GraphQL Input Object defines a set of input fields; the input fields are either scalars, enums, or other input objects. This allows arguments to accept arbitrarily complex structs.
- [Interface Type](http://spec.graphql.org/June2018/#InterfaceTypeDefinition) *(abstract type)*
GraphQL interfaces represent a list of named fields and their arguments. GraphQL objects can then implement these interfaces which requires that the object type will define all fields defined by those interfaces.
- [Union Type](http://spec.graphql.org/June2018/#UnionTypeDefinition) *(abstract type)*
GraphQL Unions represent an object that could be one of a list of GraphQL Object types, but provides for no guaranteed fields between those types. They also differ from interfaces in that Object types declare what interfaces they implement, but are not aware of what unions contain them.

### Input and Output Types

Types are used throughout GraphQL to describe both the values accepted as input to arguments and variables as well as the values output by fields. These two uses categorize types as input types and output types. Some kinds of types, like Scalar and Enum types, can be used as both input types and output types; other kinds types can only be used in one or the other. Input Object types can only be used as input types. Object, Interface, and Union types can only be used as output types. [Lists](http://spec.graphql.org/June2018/#sec-Type-System.List) and [Non‐Null](http://spec.graphql.org/June2018/#sec-Type-System.Non-Null) types may be used as input types or output types depending on how the wrapped type may be used.

### Root Operation Types

>A schema defines the initial root operation type for each kind of operation it supports: **query**, **mutation**, and **subscription**; this determines the place in the type system where those operations begin.
>
>The `query` root operation type must be provided and must be an Object type.
>
>The `mutation` root operation type is optional; if it is not provided, the service does not support mutations. If it is provided, it must be an Object type.
>
>Similarly, the subscription root operation type is also optional; if it is not provided, the service does not support subscriptions. If it is provided, it must be an Object type.
>
> [Source: GraphQL Spec (June 2018 )](http://spec.graphql.org/June2018/#sec-Root-Operation-Types)

Despite the fact that they act as the entry points into the schema, they are just another Object Type, meaning their fields work the same way.

#### [Query](http://spec.graphql.org/June2018/#sec-Query)

The contract that defines how you can ask for the data and how it'll be returned. (a read‐only fetch)

```graphql
type Query {
  me: Person!
}
```

#### [Mutation](http://spec.graphql.org/June2018/#sec-Mutation)

The contract that defines how you can change the data and how it'll be returned. (a write followed by a fetch)

```graphql
type Mutation {
  newPerson(
    name: String!,
    surname: String!,
    email: String!
  ): Person!
}
```

#### [Subscription](http://spec.graphql.org/June2018/#sec-Subscription)

If the operation is a subscription, the result is an event stream called the “Response Stream” where each event in the event stream is the result of executing the operation for each new event on an underlying “Source Stream”.

```graphql
type Subscription {
  personAdded: Person
}
```

## Resolvers

So far we've seen the **"what"** (type declarations on SDL form) but nothing about the **"how"**.

GraphQL doesn't know **how to turn an operation** (query, mutation, subscription) **into data** unless you define those instructions by providing a set of functions or **"resolver map"** matching the same shape of the data specified in your schema. This functions cannot be included on the schema, they have to be added separately and it will depend on your server application, the language you'll use to define them, but they all MUST follow the same structure.

The simplest version of a set of resolvers for the previous examples might look like this (in javascript):

```javascript

const resolvers = {
  Query: {
    // should return a Person Type shape as defined on the schema
    me() {
      return {
        id: 'HERE_THE_ID',
        name: 'A',
        surname: 'Person',
        email: 'a.person@domain.tld',
        friend: null // make it simple for now, no friends
      }
    }
  },
  Mutation: {
    // should return a Person Type shape as defined on the schema
    newPerson(root, {name, surname, email}, context, info){
      return {
        id: 'HERE_THE_ID',
        name,
        surname,
        email,
        friend: null
      }
    }
  }
}

```

We'll get into details on the next chapter, but for now it's important to know that once you define your *top-level* resolvers (they have only the Root Operation above on the hierarchy), GraphQL will fall back to the default resolver and ultimately fail if the operation cannot be completed ... and yes, resolvers can be asynchronous.

## Learning Resources

- Wikipedia
  - [GraphQL](https://en.wikipedia.org/wiki/GraphQL)
  - [Graph theory](https://en.wikipedia.org/wiki/Graph_theory)
  - [Vertex](https://en.wikipedia.org/wiki/Vertex_(graph_theory))
  - [Edge](https://en.wikipedia.org/wiki/Edge_(graph_theory))
  - [Graph (discrete mathematics)](https://en.wikipedia.org/wiki/Graph_(discrete_mathematics))
  - ["Turtles all the way down"](https://en.wikipedia.org/wiki/Turtles_all_the_way_down)
- [GraphQL Concepts Visualized](https://blog.apollographql.com/the-concepts-of-graphql-bc68bd819be3)
- [GraphQL: A data query language](https://engineering.fb.com/core-data/graphql-a-data-query-language/)
- [graphql.org](https://graphql.org/)
  - [Thinking in Graphs](https://graphql.org/learn/thinking-in-graphs/)
  - [Serving over HTTP](https://graphql.org/learn/serving-over-http/)
  - [Response data type](https://graphql.org/learn/best-practices/#json-with-gzip)
  - [API versioning](https://graphql.org/learn/best-practices/#versioning)
  - [Nullability](https://graphql.org/learn/best-practices/#nullability)
  - [Pagination](https://graphql.org/learn/best-practices/#pagination)
- [GraphQL spec (June 2018 Edition)](http://spec.graphql.org/June2018/#sec-Overview)
  - [Scalar Type](http://spec.graphql.org/June2018/#sec-Scalars)
    - [Int](http://spec.graphql.org/June2018/#sec-Int)
    - [Float](http://spec.graphql.org/June2018/#sec-Float)
    - [String](http://spec.graphql.org/June2018/#sec-String)
    - [Boolean](http://spec.graphql.org/June2018/#sec-Boolean)
    - [ID](http://spec.graphql.org/June2018/#sec-ID)
    - [Enum Type](http://spec.graphql.org/June2018/#EnumTypeDefinition)
  - [Object Type](http://spec.graphql.org/June2018/#sec-Objects)
  - [Input Object Type](http://spec.graphql.org/June2018/#InputObjectTypeDefinition)
  - [Interface Type](http://spec.graphql.org/June2018/#InterfaceTypeDefinition)
  - [Union Type](http://spec.graphql.org/June2018/#UnionTypeDefinition)
  - [Lists](http://spec.graphql.org/June2018/#sec-Type-System.List)
  - [Non‐Null](http://spec.graphql.org/June2018/#sec-Type-System.Non-Null)
  - [Root Operation Types](http://spec.graphql.org/June2018/#sec-Root-Operation-Types)
    - [Query](http://spec.graphql.org/June2018/#sec-Query)
    - [Mutation](http://spec.graphql.org/June2018/#sec-Mutation)
    - [Subscription](http://spec.graphql.org/June2018/#sec-Subscription)
