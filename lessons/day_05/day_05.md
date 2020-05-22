# [A walk in GraphQL](/README.md)

## Day 5: Interfaces and Unions

- Interface
  - Inline fragments
  - Meta Fields
- Union
- Exercise
- Learning resources

If you are here and followed the previous lessons you pretty much covered the basics of GraphQL, most small projects won't really need what's next, but they certainly will as soon as you have to scale.

## Interfaces

Underestimating things is dangerous, especially when it comes to software engineering, and particularly when the same term is used across technologies and you're tempted to assume they work just like the other one works. **Interfaces** is one of these cases.

Let's start with the spec and gradually break it down and understand how the "Interface" has similarities and differences from e.g OOP Interface implementation in Java or other language.

> GraphQL interfaces represent a list of named fields and their arguments. GraphQL objects can then implement these interfaces which requires that the object type will define all fields defined by those interfaces.
>
>Fields on a GraphQL interface have the same rules as fields on a GraphQL object; their type can be Scalar, Object, Enum, Interface, or Union, or any wrapping type whose base type is one of those five.
>
> Source: [GraphQL spec - June 2018 - Interfaces](http://spec.graphql.org/June2018/#sec-Interfaces)

> Interfaces are an abstract type where there are common fields declared. Any type that implements an interface must define all the fields with names and types exactly matching.
>
> Source: [GraphQL spec - June 2018 - Interface](http://spec.graphql.org/June2018/#sec-Interface)

So far is pretty much the same concept you'll see in OOP:
> you'll might be tempted to replace the `Type` word with `Class` for a mental map but I'd discourage that, it might mislead you

- A common shape abstracted in an `Interface` without the concrete implementation
- Many different `Types`, having a **shape intersection** (common fields) and individual **specific characteristics** (distinctive fields) which **can be identified as individuals AND as part of a wider group**, can `implement` the common `Interface` (this is a design decision, having common fields doesn't mean they're necessarily part of a group and sharing and interface)
- The `Interface` **cannot be used directly** but only through `Types` implementing it
- The `Types` **must implement all fields** defined in the `Interface`

In a concrete example we'll start seeing how GraphQL Interfaces are similar to OOP and how they're not.

We started with the following example:

```graphql

input InputCharacter {
  homeland: String
  kind: Kind
  skill: String
}
input InputCharacterUpdateHomelandByKind {
  homeland: String!
  kind: Kind!
}

enum Kind {
  HOBBIT
  ELVEN
  HALF_ELVEN
  ISTARI
}

type Character {
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
}

type Query {
  characters (input: InputCharacter): [Character]
}
```

But what if later on, we have the need to distinguish the characters?
Can we do that in a backwards compatible way? (remember, a "breaking change" is like Chucky, Freddy Krueger, CandyMan and COVID-19 all knocking at your door whilst the Bogyman is coming down the chimney in a Santa's suit ... you ain't do it).

So?

We change the `Type` `Character` into an `Interface` and create the new types implementing it.

```graphql
interface Character {
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
}

type Hobbit implements Character {
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
  ## Specific Hobbit props
  mathoms: [String!]
}

type Elvish implements Character {
  ... ## for brevity imagine all the Character fields here
  ## Specific Elvish props
  ageLess: Boolean
}

type Istari implements Character {
  ... ## for brevity imagine all the Character fields here
  ## Specific Istari props
  maiarName: String!
}

type Query {
  characters (input: InputCharacter): [Character]
}
```

At this point you might start making questions like:

1. Should I have to repeat all common fields?
   - Yes, (◔_◔) so annoying, I know, but remember, you're **implementing** an interface, you'll see in the next 2 questions how that's relevant.
2. I'm still seeing `Character` directly referenced as `Output type` on the `characters` query operation. Is that correct?
   - Yup, that's great though! whoever is using the API won't have a breaking change, it'll be transparent!
3. Oh, so where the heck the "cannot be directly used" thing fits here?
   - (⌐■_■) Exactly !!!!! remember every field in a type will eventually execute a resolver function either explicit or implicit (default)? There you go, the disambiguation happens at resolver level and there's where you end up not-using the Interface directly.

I know, the documentation is not really enlighten on this topic, furthermore, there's not a definition on how to resolve an abstract type on the spec, concretely because is a concern of the server implementation to deal with that.

On chapter [6.4.3 Value Completion](http://spec.graphql.org/June2018/#sec-Value-Completion) of the June2018 spec we can read:

> After resolving the value for a field, it is completed by ensuring it adheres to the expected return type. If the return type is another Object type, then the field execution process continues recursively.
> ...
>
> - CompleteValue(`fieldType`, `fields`, `result`, `variableValues`)
>   ....
>   - Let objectType be ResolveAbstractType(`fieldType`, `result`)

and

> **Resolving Abstract Types**
>
> When completing a field with an abstract return type, that is an Interface or Union return type, first the abstract type must be resolved to a relevant Object type. This determination is made by the internal system using whatever means appropriate.
>
> Note: A common method of determining the Object type for an objectValue in object‐oriented environments, such as Java or C#, is to use the class name of the objectValue.
>
> - ResolveAbstractType(`abstractType`, `objectValue`)
>   - Return the result of calling the internal method provided by the type system for determining the Object type of abstractType given the value objectValue.

... but that's still delegating the concrete resolution to the server implementation.

We'll see how to do it with Apollo due to it's simplicity, taking in consideration other server implementations might defer but they're very similar.

Originally, before adding the interface, we had this:

```javascript
const resolvers = {
  Kind: {
    // our Kind enum values mapping
  },
  Query: {
    characters(obj, args, context, info) {
      // our characters operation body
    }
  },
  Character: {
    friends(obj, args, context, info) {
      // our field resolver body
    },
    progenitor(obj, args, context, info) {
      // our  field resolver body
    }
  },
  Mutation: {
    /// our mutation operations
  }
}
```

But now we cant use the `Character` fields resolvers, it's an `Interface`, hence we need first to identify the concrete Type and then let GraphQL know that because it doesn't know it at this point. I know it's not really beautiful (it might feel like going back to the dark ages) but the proposed way is to rely on what makes each type different from the others based on the shape.

```javascript
const resolvers = {
  Kind: {
    // our Kind enum values mapping
  },
  Query: {
    characters(obj, args, context, info) {
      // our characters operation body
    }
  },
  Character: {
    __resolveType(obj, context, info, resolveType) {

      const {
        maiarName,
        mathoms,
        ageLess
      } = obj;

      if (mathoms) {
        return 'Hobbit';
      }

      if (ageLess) {
        return 'Elvish';
      }

      if (maiarName) {
        return 'Istari';
      }

    },
  },
  Hobbit: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Elvish: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Istari: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Mutation: {
    /// our mutation operations
  }
}
```

I'm oversimplifying this, but if you see how it's described on [Apollo's documentation](https://www.apollographql.com/docs/apollo-server/schema/unions-interfaces/#interface-type) is pretty much that.

Now in order to make a query operation and get the specific values for each type we have to use the same old props for the common fields and [inline fragments](https://graphql.org/learn/queries/#inline-fragments) for the type specific ones and you may also add a specific [meta field](https://graphql.org/learn/queries/#meta-fields) `__typename` to know what's the type of each record:

```graphql
query Characters{
  characters {
    __typename
    name
    homeland
    ... on Hobbit {
      mathoms
    }
    ... on Elvish {
      ageLess
    }
    ... on Istari {
      maiarName
    }
  }
}
```

This is what you'll get:

```json
{
  "data": {
    "characters": [
      {
        "__typename": "Hobbit",
        "name": "Frodo",
        "homeland": "The Shire",
        "mathoms": [
          "spoon",
          "lamp"
        ]
      },
      {
        "__typename": "Hobbit",
        "name": "Sam",
        "homeland": "The Shire",
        "mathoms": [
          "shovel",
          "bucket"
        ]
      },
      {
        "__typename": "Hobbit",
        "name": "Meriadoc",
        "homeland": "Buckland",
        "mathoms": [
          "table",
          "chairs"
        ]
      },
      {
        "__typename": "Hobbit",
        "name": "Peregrin",
        "homeland": "The Shire",
        "mathoms": [
          "door"
        ]
      },
      {
        "__typename": "Elvish",
        "name": "Arwen",
        "homeland": "Rivendell",
        "ageLess": true
      },
      {
        "__typename": "Elvish",
        "name": "Elrond",
        "homeland": "Rivendell",
        "ageLess": true
      },
      {
        "__typename": "Elvish",
        "name": "Celebrían",
        "homeland": "Valinor",
        "ageLess": true
      },
      {
        "__typename": "Hobbit",
        "name": "Drogo",
        "homeland": "The Shire",
        "mathoms": []
      },
      {
        "__typename": "Hobbit",
        "name": "Saradoc",
        "homeland": "Buckland",
        "mathoms": []
      },
      {
        "__typename": "Hobbit",
        "name": "Esmeralda",
        "homeland": "Buckland",
        "mathoms": []
      },
      {
        "__typename": "Istari",
        "name": "Gandalf",
        "homeland": "",
        "maiarName": "Olórin"
      },
      {
        "__typename": "Istari",
        "name": "Saruman",
        "homeland": "",
        "maiarName": "Curumo"
      },
      {
        "__typename": "Istari",
        "name": "Radagast",
        "homeland": "",
        "maiarName": "Aiwendil"
      },
      {
        "__typename": "Elvish",
        "name": "Galadriel",
        "homeland": "Lothlorien",
        "ageLess": true
      }
    ]
  }
}
```

Alternatively you can query which types are implementing a certain Interface using [Schema Introspection](http://spec.graphql.org/June2018/#sec-Schema-Introspection):

```graphql
query UnionInterfaceTypes {
  __type(name: "Character") {
    possibleTypes {
      name
      kind
    }
  }
}
```

```json
{
  "data": {
    "__type": {
      "possibleTypes": [
        {
          "name": "Hobbit",
          "kind": "OBJECT"
        },
        {
          "name": "Elvish",
          "kind": "OBJECT"
        },
        {
          "name": "Istari",
          "kind": "OBJECT"
        }
      ]
    }
  }
}
```

So far so go?  Let's make it a little harder.

Ready?

A **type** can **implement multiple interfaces**

You may say "**yeah... (◔_◔) whatever, piece of cake**".

Not so fast!

```graphql
## we add a new interface
interface MagicalCreature {
  magicPowers: [String!]
}

### and update 2 types to implement it


type Elvish implements Character & MagicalCreature{
  ... ## all previous fields remain unchanged
  ## the new field
  magicPowers: [String!]
}

type Istari implements Character & MagicalCreature{
  ... ## all previous fields remain unchanged
  ## the new field
  magicPowers: [String!]
}

type Query {
  ... ## all previous query operations
  magical: [MagicalCreature]
}
```

Now the resolvers

```javascript
const resolvers = {
  Kind: {
    // our Kind enum values mapping
  },
  Query: {
    characters(obj, args, context, info) {
      // our operation body
    },
    magical(obj, args, context, info) {
      // our operation body
    }
  },
  Character: {
    __resolveType({
      maiarName,
      mathoms,
      ageLess
    }, context, info, returnType) {

      if (mathoms) {
        return 'Hobbit';
      }

      if (ageLess) {
        return 'Elvish';
      }

      if (maiarName) {
        return 'Istari';
      }

    },
  },
  MagicalCreature: {
    __resolveType({
      maiarName,
      ageLess
    }, context, info, returnType) {

      if (ageLess) {
        return 'Elvish';
      }

      if (maiarName) {
        return 'Istari';
      }

    },
  },
  Hobbit: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Elvish: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Istari: {
    friends, // field resolver
    progenitor, // field resolver
  },
  Mutation: {
    /// our mutation operations
  }
}
```

As you can see, we start having a LOT of repeated code, and because of the `__resolveType` implementation forcing you to return **a string** with the name of a type, you might end having to deal with the return order (probably due to a "not very refined" design on your type definition). A lot of attention and planning is required to avoid derailing here, and trust me, it can happen really fast.

## Unions

> Unions are an abstract type where no common fields are declared. The possible types of a union are explicitly listed out in possibleTypes. Types can be made parts of unions without modification of that type.
>
> Source: [GraphQL spec - June 2018 - Union](http://spec.graphql.org/June2018/#sec-Union)

> GraphQL Unions represent an object that could be one of a list of GraphQL Object types, but provides for no guaranteed fields between those types. They also differ from interfaces in that Object types declare what interfaces they implement, but are not aware of what unions contain them.
>
> With interfaces and objects, only those fields defined on the type can be queried directly; to query other fields on an interface, typed fragments must be used. This is the same as for unions, but unions do not define any fields, so no fields may be queried on this type without the use of type refining fragments or inline fragments.
>
> Source: [GraphQL spec - June 2018 - Unions](http://spec.graphql.org/June2018/#sec-Unions)

### Let's summarize Unions so we can concentrate on the hot stuff :)

- Abstract types
- Represent a list of arbitrary Object types
- No common fields are declared
- Provides for no guaranteed fields between Object Types listed in a Union type
- Object types not aware of unions containing them.
- Being an abstract type implies disambiguation happening at runtime on your resolvers (depending on the technology but usually achieved as Interfaces do, e.g `__resolveType(...)`).

### Declaring a Union:

```graphql
union MyUnion = ObjectA | ObjectB | ObjectC
```

Also Union members may be defined with an optional leading `|` character to aid formatting when representing a longer list of possible types:

```graphql
union MyUnion =
  | ObjectA
  | ObjectB
  | ObjectC
  | ObjectD
  | ObjectE
```

### Some insights before moving forward:

**What's the point of having a type for potentially unrelated Objects?**

Let's say, in our <abbr title="Lord Of The Rings">LOTR</abbr> example, you want to create a global search query operation for your blog.

You'll have:

- Objects **sharing** a common **interface** like `Character`
- Objects with **shared fields** (e.g. `name`) but not sharing a logical hierarchy like `Sword`, `Realm`, `City`, `Author`, `Reviewer`, `User`
- Objects with completely different shapes like `Review`, `Comment`

Doing something like this is completely valid!

```graphql
union GlobalSearchResult =
  | Character
  | Sword
  | Realm
  | City
  | Author
  | Reviewer
  | User
  | Review
  | Comment

type Query {
    globalSearch: [GlobalSearchResult]
}

```

and

```javascript
const resolvers = {
  GlobalSearchResult: {
    __resolveType(obj, context, info, returnType){

      if (obj.bladeLength){
        return 'Sword';
      }
      /// and so on
    },
  },
  Query: {
    globalSearch: () => { ... }
  },
};
```

**Can I combine a Union and an Interface to guarantee an intersection?**

Technically yes.

```graphql
interface Searchable {
  name: String
}

union GlobalSearchResult =
  | Character ## implements Searchable
  | Sword ## idem
  | Realm ## idem
  | City ## idem
  | Author ## idem
  | Reviewer ## idem
  | User ## idem
  | Review ## OOPS, what here?
  | Comment ## OOPS, what here?

type Query {
    globalSearch: [GlobalSearchResult]
}

```

You stated every `Searchable` Object should implement the `name` property, ... that's fine, it's declarative, but **what's the point of having a Union** here? You could make it completely restrictive and get rid of the union entirely ... but ... you'll miss the `Review` and `Comment` because it doesn't make any sense to have a `name` property there at all.
...

## Exercise

For a given datasource ([abstracted as json here](datasource/data.json)) containing `n` rows of `skills` and `n` rows of `persons` we provided a sample implementation of a GraphQL server for each technology containing:

- a server app
- a schema
- a resolver map
- an entity model
- a db abstraction

The code contains the solution for previous exercises  so you can have a starting point example.

### Exercise requirements

#### Operations list

```graphql


```

Select the exercise on your preferred technology:

- [JavaScript](javascript/README.md)
- [Java](java/README.md)
- [Python](python/README.md)

## Learning resources

- GraphQL Spec (June 2018)
- GraphQL Org
- How to GraphQL
- Other articles
