# [A walk in GraphQL](/README.md)

## Day 6: Extending SDL definitions

- Extend
- Schema Stitching
- Federation
- Exercise
- Learning resources

## Extend

The `extend` keyword in SDL provides the ability to add properties  to existing SDL definitions.

What kind of definitions can you extend and what can you add to them?

### Syntax summary from the GraphQL spec

[GraphQL spec - June 2018 - B.3 Document](http://spec.graphql.org/June2018/#sec-Appendix-Grammar-Summary.Document)

<div style="font-family: monospace; font-size:.8em">

***[SchemaExtension](http://spec.graphql.org/June2018/#sec-Schema-Extension)***
  `extend` `schema` [_Directives_](http://spec.graphql.org/June2018/#Directives) *{ [_OperationTypeDefinitions_](http://spec.graphql.org/June2018/#OperationTypeDefinition) }*
  `extend` `schema` [_Directives_](http://spec.graphql.org/June2018/#Directives)

***[ScalarTypeExtension](http://spec.graphql.org/June2018/#sec-Scalar-Extensions)***
  `extend` `scalar` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives)

***[ObjectTypeExtension](http://spec.graphql.org/June2018/#sec-Object-Extensions)***
  `extend` `type` [*Name*](http://spec.graphql.org/June2018/#Name) [_ImplementsInterfaces_](http://spec.graphql.org/June2018/#ImplementsInterfaces) [_Directives_](http://spec.graphql.org/June2018/#Directives) [_FieldsDefinitions_](http://spec.graphql.org/June2018/#FieldsDefinition)
  `extend` `type` [*Name*](http://spec.graphql.org/June2018/#Name) [_ImplementsInterfaces_](http://spec.graphql.org/June2018/#ImplementsInterfaces) [_Directives_](http://spec.graphql.org/June2018/#Directives)
  `extend` `type` [*Name*](http://spec.graphql.org/June2018/#Name) [_ImplementsInterfaces_](http://spec.graphql.org/June2018/#ImplementsInterfaces)

***[InterfaceTypeExtension](http://spec.graphql.org/June2018/#sec-Interface-Extensions)***
  `extend` `interface` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives) [_FieldsDefinitions_](http://spec.graphql.org/June2018/#FieldsDefinition)
  `extend` `interface` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives)

***[UnionTypeExtension](http://spec.graphql.org/June2018/#sec-Union-Extensions)***
  `extend` `union` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives) [_UnionMemberTypes_](http://spec.graphql.org/June2018/#UnionMemberTypes)
  `extend` `union` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives)

***[EnumTypeExtension](http://spec.graphql.org/June2018/#sec-Enum-Extensions)***
  `extend` `enum` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives) [_EnumValuesDefinitions_](http://spec.graphql.org/June2018/#EnumValuesDefinition)
  `extend` `enum` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives)

***[InputObjectTypeExtension](http://spec.graphql.org/June2018/#sec-Input-Object-Extensions)***
  `extend` `input` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives) [_InputFieldsDefinition_](http://spec.graphql.org/June2018/#InputFieldsDefinition)
  `extend` `input` [*Name*](http://spec.graphql.org/June2018/#Name) [_Directives_](http://spec.graphql.org/June2018/#Directives)

</div>

Does it mean we can extend pretty much everything declared in an existing schema?

Yup

### With great power comes great responsibility &#128376;

Clearly this has specific **rules**:

- What you're extending must be already declared.
- Anything you add must not already apply to the original definition.

And comes at a great **cost**:

- As opposite to what `extend` concept implies for other languages, **in SLD there's no place for inheritance, subclassing, etc.**, you're actually extending the original thing ... by modifying it.
E.g. you cannot do something like `type MyType extends MyOtherType`, you just `extend type MyType` and now the original `MyType` has been modified with the new thing you added to it.
- Again, `extend` is a modifier.

### What's the use?

Now that we know the syntax, the rules and the cost ... gimme the benefits dude!!!!

**You may say:**
– hey! I could use `extend` instead of dealing with `interface` complexity
– well, sort of, if you don't want the implementation contract of the interface, but maybe you didn't want an `interface` at all from the beginning.

– hey! I may extend the Operation Type Definitions in order to organize it more clearly on my file adding the queries, mutations and subscriptions close the related Object Types they're dealing with!
– Now we're on the good track!!!

Let's go back to our previous example:

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

interface MagicalCreature {
  magicPowers: [String!]
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
  ##
  mathoms: [String!]
}

type Elvish implements Character & MagicalCreature{
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
  ##
  ageLess: Boolean
  magicPowers: [String!]
}

type Istari implements Character & MagicalCreature{
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
  ##
  maiarName: String!
  magicPowers: [String!]
}

type Query {
  characters (input: InputCharacter): [Character]
  magical: [MagicalCreature]
}

type Mutation {
  moveKindToHomeland (input: InputCharacterUpdateHomelandByKind): [Character]
}
```

A pretty simple file with a simple domain complexity, still, if we start organizing it differently it's gonna me easier to understand.

```graphql

# ROOT OPERATIONS

""" For the time being it's invalid to define a type without fields """
type Query {
    # Empty field obligatory
    _empty: String
}

type Mutation {
  # Empty field obligatory
  _empty: String
}

## Character –––––––––––––––––

enum Kind {
  HOBBIT
  ELVEN
  HALF_ELVEN
  ISTARI
}

input InputCharacter {
  homeland: String
  kind: Kind
  skill: String
}

input InputCharacterUpdateHomelandByKind {
  homeland: String!
  kind: Kind!
}

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
  ##
  mathoms: [String!]
}

extend type Query {
  characters (input: InputCharacter): [Character]
}

extend type Mutation {
  moveKindToHomeland (input: InputCharacterUpdateHomelandByKind): [Character]
}

## Magical Creatures –––––––––––––––––

interface MagicalCreature {
  magicPowers: [String!]
}

type Elvish implements Character & MagicalCreature{
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
  ##
  ageLess: Boolean
  magicPowers: [String!]
}

type Istari implements Character & MagicalCreature{
  id: ID
  name: String
  surname: String
  age: Int
  homeland: String
  kind: Kind
  friends (input: InputCharacter): [Character!]
  progenitor (input: InputCharacter): [Character!]
  skill: String
  ##
  maiarName: String!
  magicPowers: [String!]
}

extend type Query {
  magical: [MagicalCreature]
}
```



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
  - [Type system Extension](http://spec.graphql.org/June2018/#sec-Type-System-Extensions)
  - [Schema Extensions](http://spec.graphql.org/June2018/#sec-Schema-Extension)
  - [Types Extensions](http://spec.graphql.org/June2018/#sec-Type-Extensions)
    - [Scalar Extensions](http://spec.graphql.org/June2018/#sec-Scalar-Extensions)
    - [Object Extensions](http://spec.graphql.org/June2018/#sec-Object-Extensions)
    - [Interface Extensions](http://spec.graphql.org/June2018/#sec-Interface-Extensions)
    - [Union Extensions](http://spec.graphql.org/June2018/#sec-Union-Extensions)
    - [Enum Extensions](http://spec.graphql.org/June2018/#sec-Enum-Extensions)
    - [Input Extensions](http://spec.graphql.org/June2018/#sec-Input-Object-Extensions)
- GraphQL Org
- Other articles
