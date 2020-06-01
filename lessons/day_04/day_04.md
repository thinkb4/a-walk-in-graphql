# [A walk in GraphQL](/README.md)

## Day 4: Mutations

- Mutation
- Exercise
- Learning resources

## Mutation

What is `CRUD` without `CUD` huh?!!
In a REST-full API you have specific HTTP Verbs (like `PUT`) to create and/or update a resource, in GraphQL, strictly talking, you don't. Queries and Mutations are more similar to `POST` and `GET` in the aspect that nothing stops you from producing data side-effects through a `query` or a `mutation` even though they are, respectively, designed to have significant differences on their behavior.

Let's compare them.

| Query | Mutation |
|:----------------------------------------------------------------------:|:----------------------------------------------------------------------:|
| **Mandatory** Root Operation Type | **Optional** Root Operation Type |
| Object Type | Object Type |
| Accept arguments | Accept arguments |
| **Arguments** values be valid **input types** | **Arguments** values be valid **input types** |
| **Output** values be **valid output types** | **Output** values be **valid output types** |
| resolver's signature arity of 4 | resolver's signature arity of 4 |
| sample resolver signature <br> ```<resolverName>(root, args, context, info)``` | sample resolver signature <br> `<resolverName>(root, args, context, info)` |
| expected to be **side-effect free and idempotent** | well ... •͡˘㇁•͡˘ ... it's expected to **mutate** something  |
| `query` operation **root fields** executed and resolved in <br> **PARALLEL**  | `mutation` operation **root fields** executed and resolved in <br> **SERIAL ORDER** |

**( 0 _ 0 ) SAY AGAIN?!**

> If the operation is a **mutation**, the result of the operation is the result of executing the mutation’s top level selection set on the mutation root object type. **This selection set should be executed serially.**
>
> It is expected that the top level fields in a mutation operation perform side‐effects on the underlying data system. **Serial execution** of the provided mutations **ensures against race conditions** during these side‐effects.
>
> Source: [GraphQL Spec (June 2018) - Mutation](http://spec.graphql.org/June2018/#sec-Mutation)

That makes total sense.

> Normally the executor can execute the entries in a grouped field set in whatever order it chooses (normally in parallel). Because the resolution of fields other than top‐level mutation fields must always be side effect‐free and idempotent, the execution order must not affect the result, and hence the server has the freedom to execute the field entries in whatever order it deems optimal.
>
> When executing a mutation, the selections in the top most selection set will be executed in **serial order**, **starting with the first appearing field textually**.
>
> When executing a grouped field set serially, the executor must consider each entry from the grouped field set in the order provided in the grouped field set. It must determine the corresponding entry in the result map for each item to completion before it continues on to the next item in the grouped field set.
>
> Source: [GraphQL spec (June 2018) - Normal and Serial Execution](http://spec.graphql.org/June2018/#sec-Normal-and-Serial-Execution)

For example:

```graphql
query {
  buckLanders: characters(input: { kind: HOBBIT, homeland: "Buckland" }) {
    id
    name
  }
  shireLanders: characters(input: { kind: HOBBIT, homeland: "The Shire" }) {
    id
    name
  }
}
```

A valid GraphQL executor can resolve the query in whatever order it considers optimal including parallel (normal execution):

- Run ExecuteField() for `buckLanders` or `shireLanders` normally, which during CompleteValue() will execute the `{ id name }` sub‐selection set normally.
- Run ExecuteField() for the remaining field (`buckLanders` or `shireLanders`), which during CompleteValue() will execute the `{ id name }` sub‐selection set normally.

Even though the execution order cannot be determined a priori, the response can, and it'll reflect the lexical order of the query (as shown below). This response will be returned once all operations are completed.

```json
{
  "data": {
    "buckLanders": [
      {
        "id": "3",
        "name": "Meriadoc"
      },
      {
        "id": "9",
        "name": "Saradoc"
      },
      {
        "id": "10",
        "name": "Esmeralda"
      }
    ],
    "shireLanders": [
      {
        "id": "1",
        "name": "Frodo"
      },
      {
        "id": "2",
        "name": "Sam"
      },
      {
        "id": "4",
        "name": "Peregrin"
      },
      {
        "id": "8",
        "name": "Drogo"
      }
    ]
  }
}
```

As opposite to queries, in the following example of a mutation we'll see a completely different behavior:

```graphql
mutation {
  makeEmBuckLanders: moveKindToHomeland(input: { kind: HOBBIT, homeland: "Buckland" }) {
    id
    name
  }
  makeEmShireLanders: moveKindToHomeland(input: { kind: HOBBIT, homeland: "The Shire" }) {
    id
    name
  }
}
```

A valid GraphQL executor MUST resolve the mutation selection set SERIALLY:

- Resolve the `moveKindToHomeland(input: { kind: HOBBIT, homeland: "Buckland" })` field
- Execute the { id name } sub‐selection set of `makeEmBuckLanders` normally
- Resolve the `moveKindToHomeland(input: { kind: HOBBIT, homeland: "The Shire" })` field
- Execute the { id name } sub‐selection set of `makeEmShireLanders` normally

A correct executor must generate the following result for that selection set:

```json
{
  "data": {
    "makeEmBuckLanders": [
      {
        "id": "1",
        "name": "Frodo"
      },
      {
        "id": "2",
        "name": "Sam"
      },
      {
        "id": "4",
        "name": "Peregrin"
      },
      {
        "id": "8",
        "name": "Drogo"
      }
    ],
    "makeEmShireLanders": [
      {
        "id": "1",
        "name": "Frodo"
      },
      {
        "id": "2",
        "name": "Sam"
      },
      {
        "id": "3",
        "name": "Meriadoc"
      },
      {
        "id": "4",
        "name": "Peregrin"
      },
      {
        "id": "8",
        "name": "Drogo"
      },
      {
        "id": "9",
        "name": "Saradoc"
      },
      {
        "id": "10",
        "name": "Esmeralda"
      }
    ]
  }
}
```

Obviously the execution order is critical even in such a silly example. All **ShireLanders** where moved to **Buckland** first, then all **BuckLanders** were moved to **The Shire** and they had one of beer each at the Green Dragon back and forth. If the execution order wasn't predictable and respected, the owner wouldn't know beforehand if the minimum available stock should be 11 or 10 for this operation but that's another story ... is it? ... nope, that's exactly the point.

So far so good? Now, if you were attentive you might have noticed the following:

GraphQL spec determines **only top‐level mutation fields to be executed serially**; that means *every nested field level will be executed normally!!!!*

**... what if we define all operations to use a single entity?**
(not saying this is a right or wrong approach, it's just a possibility)

```graphql
mutation {
  Character {
    create(kind: HOBBIT, name: "Bilbo"){
      name
    }
    update(birthday: "September 22"){
      birthday
    }
  }
}
```

( ´◔ ω◔`) Can you spot the problem here?

OOPS! There's absolutely no guarantee the `create` field is executed before `update`, GraphQL won't do it for you. There are plenty of solutions and it'll depend on the language, architecture, underlying data system, etc. The important thing is that you're aware of this and design your solutions accordingly.

## Exercise

For a given datasource ([abstracted as json here](datasource/data.json)) containing `n` rows of `skills` and `n` rows of `persons` we provided a sample implementation of a GraphQL server for each technology containing:

- a server app
- a schema
- a resolver map
- an entity model
- a db abstraction

The code contains the solution for previous exercises  so you can have a starting point example.

### Exercise requirements

- Update the type definition and the resolvers to be be able to perform the mutation operations listed below (can you provide other sample mutations when your code is completed?).
- Discuss with someone else which would be the best way to use Input Objects here.
- You'll notice something about the new IDs, it's a good opportunity to ask, discuss and investigate the reasons behind the change.
- May the variable definitions be with you .... hummmm sorry, let the variable definitions in both operations help you with the Schema definition.

#### Operations list

```graphql

## Part 1
mutation createSkill($name: String!, $parent: ID) {
  createSkill(input: { name: $name, parent: $parent }) {
    id
    name
    parent {
      name
    }
  }
}

## Part 2
mutation createPerson(
  $name: String!
  $surname: String
  $email: String
  $age: Int
  $eyeColor: EyeColor
  $friends: [ID!]
  $skills: [ID!]
  $favSkill: ID
) {
  createPerson(
    input: {
      name: $name
      surname: $surname
      email: $email
      age: $age
      eyeColor: $eyeColor
      friends: $friends
      skills: $skills
      favSkill: $favSkill
    }
  ) {
    id
    fullName
    email
    age
    eyeColor
    friends {
      id
      name
    }
    skills {
      name
      parent {
        name
      }
    }
    favSkill {
      id
      name
      parent {
        name
      }
    }
  }
}

```

**NOTES:**

1. There are many topics around mutation bests practices, optimizations, "do's and don'ts"; some are excellent, all of them way beyond the scope of this stage of the course (we might get there), some super detailed and precise, some fuzzy or inaccurate. Usually the rule of thumb for finding that kind of info is **"first the authors, then the SMEs (subject matter expert), then the official community, then the rest"**, e.g. Apollo has a huge community and a YouTube channel where actual Apollo engineers and even the creators of GraphQL, like Lee Byron, do conferences and share tons of invaluable insights related to the technology itself and the engineering decisions you'll have to make on top and under GraphQL's layer.
2. Whenever you struggle finding the root cause of a problem whilst working with GraphQL step aside and make you this question:  « *Is this a GraphQL problem?* » MOST of the time the answer is NO, it's about engineering and probably related to a non-graph mental state (you might be still thinking it in REST)

Select the exercise on your preferred technology:

- [JavaScript](javascript/README.md)
- [Java](java/README.md)
- [Python](python/README.md)

## Learning resources

- GraphQL Spec (June 2018)
  - [Root Operation Types](http://spec.graphql.org/June2018/#sec-Root-Operation-Types)
  - [Mutation](http://spec.graphql.org/June2018/#sec-Mutation)
  - [Normal and Serial Execution](http://spec.graphql.org/June2018/#sec-Normal-and-Serial-Execution)
- GraphQL Org
  - [Mutations](https://graphql.org/learn/queries/#mutations)
  - [Mutations and Input Types JS tutorials](https://graphql.org/graphql-js/mutations-and-input-types/)
- How to GraphQL
  - [A simple mutation](https://www.howtographql.com/graphql-js/3-a-simple-mutation/)
- Other articles
  - [Nested GraphQL Resolvers & Separating Concerns](https://khalilstemmler.com/blogs/graphql/nested-graphql-resolvers/) by Khalil Stemmler
  - [Modeling GraphQL Mutations](https://techblog.commercetools.com/modeling-graphql-mutations-52d4369f73b1) by Oleg Ilyenko
  - [GraphQL Mutation Design: Anemic Mutations](https://medium.com/@__xuorig__/graphql-mutation-design-anemic-mutations-dd107ba70496) by Marc-André Giroux
