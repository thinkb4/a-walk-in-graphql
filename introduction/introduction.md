# [A walk in GraphQL](/README.md)

## Introduction

- What's a graph?
- What's GraphQL and what's the `graph` part all about
- GraphQL vs RESTfull
- Schema basics (Server side)
  - SDL
  - Type Definitions
  - Resolvers
  - Query definitions
  - Mutation definitions
- Client Side
  - Queries
  - Mutations
  - Fragments
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

### The relationships

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

`Person` is clearly a `vertex`or `node`, which has many properties related to it, hence we can describe the properties as `nodes` and the relationships as `edges`!!! All the way down! until no edges ar found and all the data structure is returned.  

Also did you noticed that `friend` property ( only one possible friend? that's cruel ) can hold a `Person`? That means that:

```txt
+ A [friend of]-> B [friend of]-> C +
|                                   |
+ <----------[friend of]------------+
```

Got the point? We could query the server:

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
