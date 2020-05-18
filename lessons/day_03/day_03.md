# [A walk in GraphQL](/README.md)

## Day 3: Input Objects and Enums

- Input Type
- Enum
- Exercise
- Learning resources

## Input Object

Simple operation arguments are relatively simple to define and control and scale as we saw on [Day 02](../day_02/day_02.md#arguments) but what if we need to pass along more than a simple Scalar?

What if we need to:

- pass along an object
- make sure all props belong to a specific type
- control the nullability for each prop and for the object itself
- reuse the same input and its validations across multiple operations
- add annotations to both the object and each individual prop

The SDL specifies a type for this case and states the following:

> Some kinds of types, like Scalar and Enum types, can be used as both input types and output types; other kinds types can only be used in one or the other. **Input Object types can only be used as input types.** Object, Interface, and Union types can only be used as output types.
>
> Source: [GraphQL spec (June 2018) - Input and Output Types](http://spec.graphql.org/June2018/#sec-Input-and-Output-Types)

Now imagine you want to always be able to filter a certain Object Type by more than 1 field ... let's say 3

```graphql
type Character {
  name: String
  surname: String
  age: Int
  homeland: String
  kind: String
  friends (kind: String, homeland: String, skill: String): [Character!]
  progenitor (kind: String, homeland: String, skill: String): [Character!]
  skill: String
}

type Query {
  characters (kind: String, homeland: String, skill: String): [Character]
}
```

**(⊙＿⊙')**  that's absurd!! And we're talking about 14 lines of type definitions!!! Can you imagine scale and maintain this on a real world application?

This is one of the cases where Input Object Type shines.

```graphql
input InputCharacter {
  homeland: String
  kind: String
  skill: String
}

type Character {
  name: String
  surname: String
  age: Int
  homeland: String
  kind: String
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
}

type Query {
  characters (input: InputCharacter): [Character]
}
```

Note that using the name `input` for the argument and using the name `InputCharacter` for the type definition is completely arbitrary, you can use whatever the team agrees to use.

Here a <span id="sample-query-with-input-object">sample query</span> you could request against the server for the type definition above.

```graphql
query {
  characters(
    input: { kind: "hobbit", homeland: "The Shire", skill: "the ringy thing" }
  ) {
    name
    skill
    homeland
    progenitor {
      name
    }
    friends(input: { homeland: "Buckland" }) {
      name
      skill
      progenitor(input: { skill: "boating" }) {
        name
        homeland
      }
    }
  }
}
```

and here the response

```json
{
  "data": {
    "characters": [
      {
        "name": "Frodo",
        "skill": "the ringy thing",
        "homeland": "The Shire",
        "progenitor": [
          {
            "name": "Drogo"
          }
        ],
        "friends": [
          {
            "name": "Meriadoc",
            "skill": "Noldorin dagger",
            "progenitor": [
              {
                "name": "Esmeralda",
                "homeland": "Buckland"
              }
            ]
          }
        ]
      }
    ]
  }
}
```

### Fields validation

If you try to use an unknown field for the Input Object you'll have an error from the server (and a working from your preferred dev tool if you have one)

```graphql
query {
  characters(
    input: { kind: "hobbit", hairColor: "blond"}
  ) {
    name
  }
}

### RESPONSE
{
  "error": {
    "errors": [
      {
        "message": "Field \"hairColor\" is not defined by type InputCharacter.",
        "locations": [
          {
            "line": 2,
            "column": 33
          }
        ],
        ...
        }
    ]
  }
}
```

And, as any type validation, the server will cry if a field value doesn't match with the defined type.

### Non-nullable fields

You can define non-nullable fields on an Input Object (or the whole set as non-nullable)

```graphql
input InputCharacter {
  homeland: String
  kind: String
  skill: String!
}
```

but it'll make it mandatory on every place you use it as input, otherwise it'll thrown an error `"Field InputCharacter.skill of required type String! was not provided."` ...  **Use it wisely**

### Extending Input Objects?

Unfortunately you cannot do something like `input InputCharacter extends Character` or `input InputCharacter2 extends InputCharacter1` ... you'll have to explicitly define it yourself. Nothing stops you from creating it dynamically with your preferred programming language, in that case it's suggested to generate a static output of your type definitions so you can leverage the development tool's static code analysis (local or remote).

### Nested Input Objects

Input Objects can't have fields that are other objects, only basic Scalar types, List types, and other Input Objects types.

```graphql
input InputWhatever {
  a: String
  b: Int
}

type Character {
  name: String
}

## This is VALID
input InputCharacter {
  homeland: String
  whatever: InputWhatever
}

## THIS IS NOT!!!
input InputCharacter2 {
  homeland: String
  character: Character
}
```

Why is that? Remember?
Because "*Object, Interface, and Union types can only be used as output types.*"

### A note on reusing Input Objects

Input Objects are perfect for reuse, but it's in your hands to determinate if, when and how to reuse them. It's perfectly fine to define an Input Object without reusing it!!

- Operations of the same type may evolve differently, reusing the same input might become a stick in the wheel.
- Avoid reusing the same Input Object for different operation types (query, mutation). They're likely to have different requirements for non-nullable fields

## Enum

This is a particularly interesting type that represents a finite set of values. It's values are represented as unquoted names (usually defined in all caps) that must be [valid](http://spec.graphql.org/June2018/#sec-Names) and unique within the set and there must be at least 1 value. They are (like Scalar Types) the leaf values in GraphQL.

```graphql
enum Invalid_Enum_1 { ## At least 1 value MUST be defined
}

enum Invalid_Enum_2 {
  A
  B
  A-B ## Invalid name, should match /[_A-Za-z][_0-9A-Za-z]*/
}

enum Kind {
  HOBBIT
  ELF
  HALF_ELF
}

```

On the [sample query defined here](#sample-query-with-input-object) we could pass any arbitrary value to the `kind` field (e.g `input: { kind: "whatever", homeland: "The Shire", skill: "the ringy thing" }`) without problems, the query can be performed and no errors will be thrown, probably returning no record. But, in the following example, we'll add an enum definition forcing the value to be one of the set (or null in this case) and throw an error if it's not.

```graphql
input InputCharacter {
  homeland: String
  kind: Kind
  skill: String
}

enum Kind {
  HOBBIT
  ELVEN
  HALF_ELVEN
}

type Character {
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
}
```

Our query should be as follow

```graphql
query {
  characters(
    input: { kind: HOBBIT } ## instead of { kind: "hobbit" }
  ) {
    name
    skill
    homeland
  }
}
```

A secondary, but not less important advantage of enums is that now the value is not only validated against your typedef and it's auto-completed in your preferred dev tools, but it's also **abstracted**!! Means the client is no longer dealing with actual values but with abstracted representations which is more secure and scalable.

### Language specific support for enums and internal values

The language specific implementation of enums will affect how you will have to handle the discrepancies on your side and sometimes a backend forces a different value for an enum internally than in the public API.

In our example we have that discrepancy because the backend representation (internal representation) of the `kind` field is `half-elven` which doesn't correspond to a valid name, so we used `HALF_ELVEN`.

The strategy to solve these cases will depend on the language and the server app you're using.

E.g. [Apollo Server allows the addition of custom values to enums](https://www.apollographql.com/docs/apollo-server/schema/scalars-enums/#internal-values) as shown below.

```javascript
const resolvers = {
  // This will be transparent and won't show up on the public API
  Kind:{
    HOBBIT: 'hobbit',
    ELVEN: 'elven',
    HALF_ELVEN: 'half-elven'
  },
  Query: {
    characters(_, { input: { kind, homeland, skill } = {} }) {
      //... resolver's code
    }
  },
  Character: {
    friends(obj, { input: { kind, homeland, skill } = {} }) {
      //... resolver's code
    },
    progenitor(obj, { input: { kind, homeland, skill } = {} }) {
      //... resolver's code
    }
  }
}
```

## Exercise

For a given datasource ([abstracted as json here](datasource/data.json)) containing `n` rows of `skills` and `n` rows of `persons` we provided a sample implementation of a GraphQL server for each technology containing:

- a server app
- a schema
- a resolver map
- an entity model
- a db abstraction

The code contains the solution for previous exercises so you can have a starting point example.

### Exercise requirements

- Update the type definition and the resolvers to be be able to perform the query operations listed below (can you provide other sample queries when your code is completed?).
- Discuss with someone else which would be the best way to use Input Objects and Enums and try some. (there's always a tricky question)

#### Operations list

Provide the necessary code so that a user can perform the following `query` operations (the argument's values are arbitrary, your code should be able to respond for any valid value consistently):

The typedef should declare the following behavior and the resolvers should behave consistently for the arguments' logic:

- All arguments for all queries are optional
- All arguments MUST be passed along as Input Objects
- All Input Object values MUST be either Scalar or Enum values
- All filtering rules must follow what specified on [Day 02 exercise](../day_02/day_02.md#exercise)

```graphql

## Part 01
query singlePerson{
  person(
    input: {
      id: 5,
      age: 36,
      eyeColor: BLUE,
      favSkill: 102
    }
  ) {
    id
    age
    eyeColor
    fullName
    email
  }
}

## Part 02
query multiplePersons{
  persons(
    input: {
      id: 4,
      eyeColor: BROWN
    }
  ) {
    id
    age
    eyeColor
    fullName
    email
    friends(
      input: {
        favSkill: 15
      }
    ) {
      id
      name
      favSkill {
        name
      }
    }
    skills(
      input: {
        id: 47,
        name: "Doctype"
      }
    ) {
      id
      name
    }
    favSkill {
      id
      name
      parent {
        id
        name
      }
    }
  }
}

## Part 03
query singleSkill{
  skill(
    input: {
      id: 47,
      name: "Doctype"
    }
  ) {
    id
    name
    parent {
      id
      name
    }
  }
}

## Part 04
query multipleSkills{
  skills (
    input: {
      id: 4,
      name: "Design Principles"
    }
  ){
    id
    name
    parent{
      id
      name
    }
  }
}

```

Select the exercise on your preferred technology:

- [JavaScript](javascript/README.md)
- [Java](java/README.md)
- [Python](python/README.md)

## Learning resources

- GraphQL Spec (June 2018)
  - [Input Objects](http://spec.graphql.org/June2018/#sec-Input-Objects)
    - [Values](http://spec.graphql.org/June2018/#sec-Input-Object-Values)
    - [Field Names](http://spec.graphql.org/June2018/#sec-Input-Object-Field-Names)
    - [Field Uniqueness](http://spec.graphql.org/June2018/#sec-Input-Object-Field-Uniqueness)
    - [Required Fields](http://spec.graphql.org/June2018/#sec-Input-Object-Required-Fields)
  - [Enums](http://spec.graphql.org/June2018/#sec-Enums)
    - [Values](http://spec.graphql.org/June2018/#sec-Enum-Value)
    - [Type Kind](http://spec.graphql.org/June2018/#sec-Enum)
- GraphQL Org
  - [Input Types](https://graphql.org/learn/schema/#input-types)
  - [Enumeration Types](https://graphql.org/learn/schema/#enumeration-types)
