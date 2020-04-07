# [A walk in GraphQL](/README.md)

## Day 2: Arguments and Variables

- Argument
- Variable
- Exercise
- Learning resources

## Arguments

On [Day 01](../day_01/day_01.md) we saw what [queries](../day_01/day_01.md#query) and [resolvers](../day_01/day_01.md#resolver) are, now imagine a scenario where the underlying persistance system returns a collection of rows we usually filter with a url querystring param on a REST request.

Given the following request:

```txt
<scheme>://<authority>/users/?age=40
```

We'd assume the users' endpoint will respond with a list of users and if provided will include only `n` `User`s who's `age` is `40`, filtering every other out.

How does GraphQL provide that functionality?

> Object fields are conceptually functions which yield values.
>
> Source: [GraphQL spec (June 2018) - Field Arguments](http://spec.graphql.org/June2018/#sec-Field-Arguments)

| come again?  (ಠ_ಠ)  |
|:-:|
|All `Object Type`.`fields` will eventually be mapped to `resolver functions` and therefore, accepting arguments. _(including default `Root Operation` object [Query](http://spec.graphql.org/June2018/#sec-Query), [Mutation](http://spec.graphql.org/June2018/#sec-Mutation) and [Subscription](http://spec.graphql.org/June2018/#sec-Subscription) since they are [Object Types](http://spec.graphql.org/June2018/#sec-Objects) as well)_ |

SUPER POWERS ON ─=≡Σ((( つ◕ل͜◕)つ

for the given typeDef

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

Let's try this query

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

That's GraphQL saying: **You can pass no args if you ain't got no def.**

Everything MUST be declared on your schema.

```graphql
type Query {
  users (age: Int): [User]
}
```

Of course passing the param along without defining  handling it at resolver level won't do anything.

```javascript
const resolverMap = {
  Query: {
    users (obj, params, context, info) {
      return context.db.findUser({ age: params.age })
    }
  }
}
```
