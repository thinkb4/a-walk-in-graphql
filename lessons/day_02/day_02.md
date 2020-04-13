# [A walk in GraphQL](/README.md)

## Day 2: Arguments and Variables

- Argument
- Variable
- Exercise
- Learning resources

## Arguments

On [Day 01](../day_01/day_01.md) we learned [queries](../day_01/day_01.md#query) and [resolvers](../day_01/day_01.md#resolver)., and we also learned how to ask for a subset of scalars from a given object (or each one of a list of objects). But what if we want to request a specific subset of records based on it's values (filtering) or tell the server to perform a specific data transformation on a specific field? Here's where `arguments`  comes into play.

Imagine a scenario where the underlying persistance system returns a collection of rows we usually filter with a url querystring param on a REST request.

Given the following request:

```txt
<scheme>://<authority>/users/?age=40
```

We'd assume the users' endpoint will respond with a list of users including only `n` `User`s who's `age` property is `40`, filtering every other out.

How does GraphQL provide that functionality?

> Object fields are conceptually functions which yield values.
>
> Source: [GraphQL spec (June 2018) - Field Arguments](http://spec.graphql.org/June2018/#sec-Field-Arguments)

| come again?  (ಠ_ಠ)  |
|:-:|
|All `Object Type`.`fields` will eventually be mapped to `resolver functions` and therefore, will accept arguments. _(including default `Root Operation` object [Query](http://spec.graphql.org/June2018/#sec-Query), [Mutation](http://spec.graphql.org/June2018/#sec-Mutation) and [Subscription](http://spec.graphql.org/June2018/#sec-Subscription) since they are [Object Types](http://spec.graphql.org/June2018/#sec-Objects) as well)_ |

### Simple example

For the given typeDef

```graphql
type User {
  name: String
  surname: String
  age: Int
}

type Query {
  users: [User]
}
```

.. let's try this query

```graphql
query {
  users (age: 40) {
    name
    surname
    age ## just to make sure :P
  }
}
```

BOOM! 💥 `"Unknown argument "age" on field "users" of type "Query"."`

That's GraphQL saying: **You can't pass no args if you ain't got no Type Def!**

Everything MUST be declared on your Type Definition.

```graphql
type Query {
  users (age: Int): [User]
}
```

Of course passing the param along without handling it at resolver level won't do the job.

```javascript
const resolvers = {
  Query: {
    users (obj, params, context, info) {
      return context.db.findUser({ age: params.age })
    }
  }
}
```

### Arguments, deep dive

So far we saw nothing

SUPER POWERS ON ─=≡Σ((( つ◕ل͜◕)つ

Before diving deeper it'd be great to have some starting point definitions and reminders so that we don't get lost and confused.

1. Any field of a `query`, `mutation` or `subscription` operation can pass `arguments` along
2. Any field of an `Object Type` can pass `arguments` along
3. Every `argument` of an operation MUST be declared on the Type Definition
4. `argument` names MUST NOT begin with "__" (double underscore), that naming convention is reserver for GraphQL's introspection system
5. `argument` names in an argument set for a field MUST be **unique**
6. `arguments` are **unordered**
7. `argument` values are **typed** and they MUST belong to a known type on the Type Definition as long as they're valid [input types](http://spec.graphql.org/June2018/#sec-Input-and-Output-Types), they being one of the default set of types or a custom type.
8. `arguments` can be `non-nullable` (aka required)
9. `arguments` can have a default value

#### Query nested and field level arguments

So far we've seen nothing worthy of the "legendary mighty awesomeness of arguments usage" award. That's about to change, like, forever.

What if we need to get a specific subset of records with a specific subset of related records like the following:

> get all users names whose kind is "hobbit" and whose homeland is "The Shire" and their friends names whose kind is "half-elven" and the progenitor whose skill is "foresight".

In a typical REST API we would do:

```txt
> request: /users/?homeland=The%20Shire

// for each result
> request: /users/?id=<list of friends ids>&kind=half-elven

// for each result
> request: /users/?id=<list of progenitor ids>&skill=foresight

// connect all results on the client side

```

And this is a silly example of querying the users endpoint with 3 different filters!!! Can you imagine a really complex relationship of data being asked to the server with multiple requests and handling those relationships on the client with a ton of garbage data and handling the business logic to orchestrate which method should be called in which order passing which params and validating all inputs?

Sure, you could define an ad-hoc endpoint for that but throw scalability and maintainability overboard.

In GraphQL the query would go:

```graphql
query {
  users (homeland: "The Shire") {
    name
    friends (kind: "half-elven") {
      name
      progenitor (skill: "foresight") {
        name
      }
    }
  }
}
```

Now, let's see how the Type definition might go for the previous operation.

```graphql
type User {
  name: String
  homeland: String
  kind: String
  friends (kind: String): [User!]
  progenitor (skill: String): [User!]
  skill: String
}

type Query {
  users (homeland: String): [User]
}
```

and the resolvers would go:

```javascript

// Note:
// Filtering functions are made up names for explanatory purpose

const resolvers = {
  Query: {
    /**
     * interested only on
     *  -> homeland property of the second arg (params)
     */
    users(obj, { homeland }) {
      return context.db
        .addFilter({ homeland })
        .fetchUsers();
    }
  },
  User: {
    /**
     * interested only on
     *  -> friends property of the first arg (obj)
     * and
     *  -> kind property of the second arg (params)
     */
    friends({ friends }, { kind }) {
      return context.db
        .addFilter({ friends })
        .addFilter({ kind })
        .fetchUsers();
    },
    /**
     * interested only on
     *  -> progenitor property of the first arg (obj)
     * and
     *  -> skill property of the second arg (params)
     */
    progenitor({ progenitor }, { skill }) {
      return context.db
        .addFilter({ progenitor })
        .addFilter({ skill })
        .fetchUsers();
    }
  }
}
```

and the response might look like:

```json
{
  "data": {
    "users": [
      {
        "name": "Frodo",
        "friends": [
          {
            "name": "Arwen",
            "progenitor": [
              {
                "name": "Elrond"
              }
            ]
          }
        ]
      },
      {
        "name": "Sam",
        "friends": []
      },
      {
        "name": "Peregrin",
        "friends": []
      },
      {
        "name": "Meriadoc",
        "friends": []
      }
    ]
  }
}
```

We defined:

- 3 params
  - 1 for the top-level query
    - `users`
      - `(homeland: String)`
  - 2 for the field-level queries
    - `friends`
      - `(kind: String)`
    - `progenitor`
      - `(skill: String)`

and mirroring that, we defined

- 3 resolvers
  - 1 for the top-level query
    - `users`
      - `(obj, { homeland })`
  - 2 for the field-level queries
    - `friends`
      - `({ friends }, { kind })`
    - `progenitor`
      - `({ progenitor }, { skill })`

At this point you might realize a big part of the architecture is provided out-of-the-box but still, the burden of the persistence layer is in our hands???!! We still have to let GraphQL know hot to retrieve the data? Potentially making the same 3 round trips to the DB??!!!

The answer is YUP! The implementation of the resolver's code is entirely up to you, GraphQL is about almost everything else.

That said, since a few cases are not tackled specifically by GraphQL but are really common ( e.g. [n+1](../day_01/day_01.md#nested-queries-and-the-n--1-problem) or [caching](https://graphql.org/learn/caching/)) you'll find several options and third-party libraries created to provide scalable solutions for that. We'll see some of them later on this course.

#### Arguments default values

Given the previous example, what if I want to define a default value for an argument?

There you go!

```graphql
type User {
  name: String
  surname: String
  age: Int
  homeland: String
  kind: String
  friends (kind: String): [User!]
  progenitor (skill: String = "foresight"): [User!]
  skill: String
}
```

#### Non-nullable arguments

Here you have!

```graphql
type User {
  name: String
  surname: String
  age: Int
  homeland: String
  kind: String
  friends (kind: String!): [User!]
  progenitor (skill: String = "foresight"): [User!]
  skill: String
}
```

also, if the argument is not provided it will yield 

BOOM! 💥 `"Field "friends" argument "kind" of type "String!" is required, but it was not provided."`

#### Arguments type validation

Yup, if you go:

```graphql
query {
  users (homeland: "The Shire") {
    name
    friends  (kind: 1){
      name
    }
  }
}
```

 you'll have

 BOOM! 💥 `"Expected type String!, found 1."`

 also many IDEs provide static code validation for some of this cases.

#### Coercing Field Arguments

In order to produce the right value for an argument it must go through a specific process. You can see how and when that happens here in the specification section dedicated to [Field arguments coercion](http://spec.graphql.org/June2018/#sec-Coercing-Field-Arguments)

## Variables

## Exercise

## Learning resources
