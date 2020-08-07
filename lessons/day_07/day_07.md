# [A walk in GraphQL](../../README.md)

## Day 7: Errors

- Error
- Exercise
- Learning resources

If you're new to GraphQL you might find this section very surprising —but believe us— you'll need to re-think **"what an error is"** to comfortably navigate GraphQL's waters.

### TL;DR

1. [**One Graph** to rule them all](#1-one-graph-to-rule-the-all--them-spec)  
_The spec. is the spec._
2. [**One Place** to find them](#2-one-place-to-find-them--200-ok--errors)  
_`200 OK` & `errors`_
3. [**One Object** to bring them all](#3-one-object-to-bring-them-all--data--errors-are-siblings)  
_`{ "errors": [...], "data": {...}}`_
4. [and in your mind **bind them**](#4-and-in-your-mind-bind-them--errors-as-unrequested-results)  
_"errors" as "unrequested results"_
5. [in the land of the **runtime** where the **implementation** lies.](#5-in-the-land-of-the-runtime-where-the-implementation-lies--the-runtime-handles-the-rest)  
_The runtime handles the rest._

### 1. One Graph to rule them all — The Spec.

As GraphQL is —technically— something "in between", a **flaw** may come from:

- the client,
- within the GraphQL server
- the services/persistence providers;

GraphQL will treat them all in a consistent way —as defined by the spec.— but before digging deeper into the spec. let's organize them.

#### By Severity (roughly)

- Fatal
  - The service is unresponsive
- Non Fatal
  - A **fault** occurred but an "operable" response can still be provided.

#### By Origin

- Client/Request
  - Whoever is consuming it
- Provider
  - Whatever layer is providing a service to GraphQL —e.g. persistence layer
- Server
  - Runtime

#### By GraphQL Phase

- [Parse](../day_01/day_01.md#1-parsing-the-incoming-request)
  - Malformed GraphQL request
- [Validation](../day_01/day_01.md#2-validation)
  - Type checking error
- [Execution](../day_01/day_01.md#3-execution)
  - Resource not found,
  - Bug

And in the spec. the rules are quiet consistent:  
> It'll try —at any cost— to propagate the error and continue operating whenever possible.  

If you want to see all the cases, one of the most educational ways is to browse the spec. and search for "errors", you'll obtain about 170 different places related to the [type definitions](http://spec.graphql.org/June2018/#sec-Types), the [validation](http://spec.graphql.org/June2018/#sec-Validation), the [execution](http://spec.graphql.org/June2018/#sec-Execution), the [non-nullability cases](http://spec.graphql.org/June2018/#sec-Errors-and-Non-Nullability) and particularly how the [Response](http://spec.graphql.org/June2018/#sec-Response) should be provided, formatted and even [serialized](http://spec.graphql.org/June2018/#sec-Serialization-Format)!

### 2. One Place to find them — `200 OK` & `errors`

Unless something goes really bad  with the server, you probably won't see other than a 200 OK HTTP status code.

Do you remember, "It's Graphs **All the Way Down**" sentence?  
If you have a reference error inside a resolver's code, your server will run perfectly until that resolver is executed, then the reference error will be thrown, but the server won't fail, it won't stop executing, it'll simply propagate the error up and up until the response is being prepared, and there, an `errors` node will be added to the JSON response.

Shocking? (◉ω◉)

 The `errors` node is a list of error objects containing the following structure:

 ```json5

{
  "errors": [
    {
      "message": "String",
      "locations": [{ "line": Number, "column": Number }],
      "path":[...],
      "extensions": {
        "code": "String",
        "exception": {
          "stacktrace": [...]
        }
      }
    }
  ]
}

```

Now let's break down the `errors` entry and the properties of each error in the list.  
In the [GraphQL spec June 2018 - 7.1.2 Errors](http://spec.graphql.org/June2018/#sec-Errors) section everything is described in great detail, check below some highlights.

#### `errors` entry

- is a non-empty list of errors
- shouldn't be present in the response if no errors were found
- if `data` entry is absent, then `errors` MUST be present
- `data` and `errors` entries can **coexist** in the response
- each **error** in the list is a **map** containing the following properties
  - `message`
    - **mandatory**
    - a descriptive string of the error
    - directed to developers
  - `locations`
    - **mandatory**
    - a list of locations
    - each **location** is a `{ "line": Number, "column": Number }` where `Number` is a 1‐indexed positive integer
  - `path`
    - **mandatory**
    - path **segments** from root to the identified field which experienced the error
      - if a segment represents a field, it should be a string containing the field name
      - if a segment represents an index of a list, it should be a 0‐indexed integer
  - `extensions`
    - **optional**
    - implementation dependent
    - unrestricted arbitrary content
    - since **adding other entries to the errors map are highly discouraged** —but not prohibited— this might be the RIGHT PLACE for engineers to add extra info whenever required **(see example below)**

```json
{
  "errors": [
    {
      "message": "Name for character with ID 1002 could not be fetched.",
      "locations": [ { "line": 6, "column": 7 } ],
      "path": [ "hero", "heroFriends", 1, "name" ],
      "extensions": {
        "code": "CAN_NOT_FETCH_BY_ID",
        "timestamp": "Fri Feb 9 14:33:09 UTC 2018"
      }
    }
  ]
}
```

> GraphQL services may provide an additional entry to errors with key `extensions`. This entry, if set, must have a map as its value. **This entry is reserved for implementors to add additional information to errors however they see fit, and there are no additional restrictions on its contents**.
>
> GraphQL services should not provide any additional entries to the error format since they could conflict with additional entries that may be added in future versions of this specification.
>
> Source: [GraphQL spec June 2018 - Example nº187](https://spec.graphql.org/June2018/#example-fce18)

### 3. One Object to bring them all — `data` & `errors` are siblings

You may have both a `data` and an `errors` properties in the Response body, containing expected data as well as 1 or more errors.

Going back to our characters list example, let's say the `name` property is **nullable**  for `Istary` but **non-nullable** for `Hobbit`  and one of the records has a null value; this is what we get as a response:

**TypeDef**

```graphql
interface Character {
  id: ID
  name: String
  ....
}

type Hobbit implements Character {
  id: ID
  name: String!
  ....
}

type Istari implements Character{
  id: ID
  name: String
}
```

**Query**

```graphql
query Characters{
  characters {
    name
  }
}
```

**Response**

```json5
{
  "errors": [
    {
      "message": "Cannot return null for non-nullable field Hobbit.name.",
      "locations": [
        {
          "line": 3,
          "column": 5
        }
      ],
      "path": [
        "characters", // the query operation name
        0, // the index of the record related to the list of characters returned
        "name" // the field name
      ],
      "extensions": {
        "code": "INTERNAL_SERVER_ERROR",
        "exception": {
          "stacktrace": [.....] // a lot of stuff here
        }
      }
    }
  ],
  "data": {
    "characters": [
      null, // <-- there you go, the first records is null due to the error but the rest can be delivered
      { "name": "Sam" },
      { "name": "Meriadoc" },
      { "name": "Peregrin" },
      { "name": "Arwen" },
      { "name": "Elrond" },
      { "name": "Celebrían" },
      { "name": "Drogo" },
      { "name": "Saradoc" },
      { "name": "Esmeralda" },
      { "name": "Gandalf" },
      { "name": null }, // this Istari has now a null name, but it's fine for GraphQL because it's nullable
      { "name": "Radagast" },
      { "name": "Galadriel" }
    ]
  }
}
```

### 4. and in your mind bind them — **"errors"** as **"unrequested results"**

Let's stay put for a moment and analyse what just happened above.

1. One of our records doesn't respect the contract for its type
2. An error is added to the response describing that particular fault
3. The execution could be performed for the rest of the records and they were added to the `data` entry as expected

So:

1. Should we consider the whole as an error?
2. Should we tell the user something about the fault? what? how? when?
3. Can/Should the user do anything about it?

... and many other questions can derive from this behavior!  

So, what do we do about this?  
There's no "the right answer" for that.  

In terms of community trends related to best practices there are different opinions:

— Intentionally throw errors!  
— Do Not! ୧༼ಠ益ಠ༽୨

— Format them!  
— Whatever! (◔_◔)

— Make Errors part of your schema!  
— Do Not! ୧༼ಠ益ಠ༽୨

— Leverage your GraphQL server app error features!  
— Do Not! ୧༼ಠ益ಠ༽୨

— Be careful with `extension` field!  
— Hell yeah! ᕦ( ͡° ͜ʖ ͡°)ᕤ

— Disable `stacktrace` for production! _(should be the default behaviour of your server app in production mode, but still...)_  
— Hell yeah! ᕦ( ͡° ͜ʖ ͡°)ᕤ

Unless the only thing you have is a fatal error, you'll need to start thinking of **"errors"** as **results** —which is true in the GraphQL mindset— even though they might not be the results you're expecting for.  

You can think of them as **requested results** —the `data` node— and **unrequested results** —the `errors` node— and **make sure all the organization is aligned on how to organize and treat them**.

### 5. In the land of the **runtime** where the implementation lies — The runtime handles the rest

Everything that exceeds what GraphQL defines in the spec. will be handled by the runtime. It delegates other implementation details (custom handling) to the server applications, the language and the execution environment; and it'll determine the level of interaction you will have with the error handling system.

## Exercise

Until now, we didn't care about several aspects outside of GraphQL's plate —like data consistency. An example?

If you run the following mutation:

```graphql
mutation createSkill($name: String!, $parent: ID) {
  createSkill(input: { name: $name, parent: $parent }) {
    id
    name
    parent {
      name
    }
  }
}
```

with the following variables:

```json
{
  "name": "BLAH",
  "parent": 8000
}
```

and there's no record with `ID` 8000, you may have different outcomes.  
Here some of them:

1. before/during insertion
   1. the persistence layer **complains** and returns an error
   2. the persistence layer **doesn't complain** but you do by checking it imperatively
   3. the insertion's performed "correctly" by silently ignoring the inconsistency
2. during query —after insertion—
   1. you complain about the missing record
   2. you silently ignore the inconsistency

If we take 1.3 and 2.2 together with the current schema

```graphql
# INPUTS

input InputSkill {
  id: ID
  name: String
}

input InputSkillCreate {
  name: String!
  parent: ID
}

# OBJECT TYPES

type Skill {
  id: ID
  parent: Skill
  name: String!
  now: String! @deprecated(reason: "This is just an example of a virtual field.")
}

# ROOT OPERATIONS

extend type Query {
  randomSkill: Skill!
  skill (input: InputSkill): Skill
  skills (input: InputSkill): [Skill!]
}

extend type Mutation {
  createSkill (input: InputSkillCreate): Skill!
}
```

you'll obtain this response:

```json
{
  "data": {
    "createSkill": {
      "id": "<WHATEVER THE NEW ID IS>",
      "name": "BLAH",
      "parent": null
    }
  }
}
```

The invalid parent id was persisted —or not, GraphQL doesn't care— and the parent's record couldn't be retrieved during the subsequent query, but neither an error was thrown nor an invalid type was provided —since `parent` is nullable— and therefore the `data` node came along with no `errors` sibling.

- Is this an error?
- who should take the responsibility for the inconsistency?
- when, where, how should we handle this situation?

Clearly the first one is an easy one to answer, OF COURSE IT IS! but, what about the rest? Well, that will depend on many factors but they're all related to engineering practices and not to GraphQL strictly speaking.

### Exercise requirements

All the mutations provided on the previous days intentionally LACK or DIVERGE on this kind of verification depending on their specific stack.  

Since this practice hasn't "A RIGHT SOLUTION", we propose you to review the current code and —*given the specific technology you're working with, and your criteria*— explore, propose and implement 3 solutions for the `createSkill` mutation —you can work on other mutations too if you want, that'll depend on your time—

1. A defensive one (must fail BEFORE insertion)
2. A reactive one (must fail DURING insertion)
3. An informative one (must include the error DURING QUERY)

Then take notes regarding the pros and cons of each approach and share it with your team mates.

> IMPORTANT:
> On the learning resources section there are several incredibly useful videos and links; take a look at them before putting your hands on the exercise.
>
> **Adding new types for this exercise is allowed but changing the current types is not.**

#### Technologies

Select the exercise on your preferred technology:

- [JavaScript](javascript/README.md)
- [Java](java/README.md)
- [Python](python/README.md)
- [NetCore](netcore/README.md)

## Learning resources

- GraphQL Spec (June 2018)
  - [Errors and Non-Nullability](http://spec.graphql.org/June2018/#sec-Errors-and-Non-Nullability)
  - [Errors](http://spec.graphql.org/June2018/#sec-Errors)
- Apollo Blog
  - [Full Stack Error Handling with GraphQL and Apollo](https://www.apollographql.com/blog/full-stack-error-handling-with-graphql-apollo-5c12da407210)
- Apollo GraphQL
  - [Error Handling](https://www.apollographql.com/docs/apollo-server/data/errors/)
  - [Apollo Link](https://www.apollographql.com/docs/link/)
- YouTube
  - [Err(or) on the side of awesome](https://youtu.be/w7FBfcD2o-0) by Christina Yu
  - [200 OK! Error Handling in GraphQL](https://www.youtube.com/watch?v=A5-H6MtTvqk) by Sasha Solomon @ GraphQL Conf 2019
  - [How to Format Errors in GraphQL](https://www.youtube.com/watch?v=7oLczJD6zZI)
- HowToGraphQL
  - Error Handling
    - [Java](https://www.howtographql.com/graphql-java/7-error-handling/)
    - [Python](https://www.howtographql.com/graphql-python/6-error-handling/)
- GraphQL .NET
  - [Error Handling](https://graphql-dotnet.github.io/docs/getting-started/errors/)
- Other articles
  - [The Definitive Guide to Handling GraphQL Errors](https://itnext.io/the-definitive-guide-to-handling-graphql-errors-e0c58b52b5e1) by Matt Krick
