# [A walk in GraphQL](../../README.md)

## Day 6: Extending SDL definitions

- Extend
- Scaling our Schema
  - Principled GraphQL
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
— hey! I could use `extend` instead of dealing with `interface` complexity  
— well, sort of, if you don't want the implementation contract of the interface, but maybe you didn't want an `interface` at all from the beginning.

— hey! I may extend the Operation Type Definitions in order to organize it more clearly on my file adding the queries, mutations and subscriptions close to the related Object Types they're dealing with!  
— Now we're on the good track!!!

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

A pretty simple file with a simple domain complexity, still, if we start organizing it differently it's gonna be easier to understand.

```graphql

# ROOT OPERATIONS

"""
For the time being  
it's invalid to define a type  
without fields
"""
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

An underlying pattern is becoming evident, we have at least two potential domains, right?, now imagine if `Sword`, `Realm`, `City` types are added ... and this is still a ridiculously small app!!! and as a seasoned engineer you might want to separate them into specific files, maybe in specific directories and so on; well, here's where things become complicated.

## Scaling your Schema

There's so much to say about this argument, and the considerations will vary depending on the prerequisites and circumstances, e.g. the project type (personal, enterprise, experimental), the scale (single person, small team, multiple teams, distributed teams) and many other! So in a high level overview we'll mention some of those considerations and leave you the links to the most valuable documents I've seen so far which represent the best practices determined by the industry after years of designing GraphQL at scale. One of the most relevant documents is the "[Principled GraphQL](https://principledgraphql.com/)", written by [Geoff Schmidt](https://twitter.com/GeoffQL) and [Matt DeBergalis](https://twitter.com/debergalis), which we'll mention several times in this chapter, but it's not the only one as many great engineers are sharing with all of us their invaluable experience.

### Graph Breakdown

One of the first things that will hit you is the "[One Graph](https://principledgraphql.com/integrity#1-one-graph)" principle which by any means is referred to have one single file; it means that you have to have a single source of truth (one unified graph) that represents the interactions between the actors and the data through [Aggregates](https://khalilstemmler.com/articles/typescript-domain-driven-design/aggregate-design-persistence/#Aggregates) (Objects & Relationships), Views (Queries) and Commands (Mutations); here is where we have 2 primary options, the **Monolithic architecture** (e.g. **Schema Stitching**), and the **Federated architecture**, (e.g. **Apollo Federation**). Other options like [graphql-modules](https://github.com/Urigo/graphql-modules) combine the declaration and execution code all together but we won't describe it here. While the former is considered deprecated or not recommended for many reasons we'll see later, we still encourage you to know it and practice it because:

  1. It's still applies to small project
  2. You'll still find it alive and kicking in many projects
  3. The migration from schema stitching to federation is [thoroughly documented](https://www.apollographql.com/docs/apollo-server/federation/migrating-from-stitching/), and you may consider this process the natural evolution of small or legacy projects when the need for scale arise.

Clearly this discussion is around architecture and not about GraphQL explicitly, therefore the actual implementation will depend on the technology. We'll see the concept as detached from the technologies as possible but when a particular example will require it, we'll use Apollo's related technologies as it's leading the trend for now.

Here some of the most evident differences
| [Schema Stitching](https://www.apollographql.com/docs/graphql-tools/schema-stitching/) | [Apollo Federation](https://www.apollographql.com/docs/apollo-server/federation/federation-spec/) |
|:-:|:-:|
| Compose 2 or more schemas into 1 (stitching them together) | Compose 2 or more services into 1 (gateway) |
| Typically organized by Type (e.g. a team controlling a Type) | Typically organized by concerns (e.g. a team controlling a domain) |
| A relationship is imperatively resolved at runtime. | An implementing service must add the `@key` directive to a type's definition in order to declare how the relationship will be established. |
| Optimizations and metrics are harder to separate | Each service can be optimized and monitored independently |

Another summarized high level description we like can be read in [GraphQL Federation vs Stitching](https://gunargessner.com/graphql-federation-vs-stitching) by [Gunar Gessner](https://medium.com/@gunar).

All above is only achievable thanks to `extend`, a simple keyword on the spec is the door that opens towards hell or heaven depending on how you use it.

## Schema Stitching example

> In order to keep the learning path more natural and start with the simplest example we will go for the Stitching technique for now and dedicate a whole day for Federation in the future.

### SDL

So how our example would look like if we use the stitching technique?

**schema.gql**

```graphql

# ROOT OPERATIONS
"""
For the time being  
it's invalid to define a type  
without fields
"""
type Query {
    # Empty field obligatory
    _empty: String
}

type Mutation {
  # Empty field obligatory
  _empty: String
}
```

**character.gql**

```graphql
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

```

**magicalCreature.gql**

```graphql

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

### Resolvers

And the resolvers?

Well, you might separate them into specific files too or keep them in one place, again, that's an engineering concern and will depend on the technology, GraphQL just doesn't care, granted they're passed along with the type definitions to the server.

### Server

See here how our implementation might look like using Apollo Server in an oversimplified example.

```javascript
const { ApolloServer } = require('apollo-server');
const resolvers = require('./whatever_path/resolvers');

/// ... import the schemas here

const server = new ApolloServer({
  /**
   * Actually you can pass along an array of GraphQL SDL Documents
   * @see https://www.apollographql.com/docs/apollo-server/api/apollo-server/#constructoroptions-apolloserver
   */
  typeDefs: [
    schema, // from schema.gql
    character, // from character.gql
    magicalCreature // from magicalCreature.gql
  ],
  resolvers,
})

server.listen(4000)
  .then(() => console.log('listening'))
```

### When things go wrong (︶︹︶)

> a.k.a. how to resolve conflicts

So far our example worked smoothly just because we were moving a working SDL document to 3 documents, but what happens when the number of documents and lines of code scale and you need to handle fields, types or other conflicts? Clearly just importing the SDL files won't work anymore. In this case you'll have to leverage the existing tools available for your technology or eventually write your own if nothing fit your needs.

- `graphql-tools/merge`
  - [onTypeConflict](https://www.graphql-tools.com/docs/schema-stitching#ontypeconflict)
  - [mergeTypeDefs](https://www.graphql-tools.com/docs/merge-typedefs#manually-import-each-type)

You might still find some previous implementations using `graphql-tools/mergeSchemas` which has been deprecated in favor of the above mentioned.

See below the migration tutorial and some old documentation as you might still find it in a project.

- [Migration from Merge GraphQL Schemas](https://www.graphql-tools.com/docs/migration-from-merge-graphql-schemas)
- [Merging Schemas](https://www.advancedgraphql.com/content/schema-stitching#merging-schemas) with `graphql-tools/mergeSchemas`
  - [No conflicts](https://www.advancedgraphql.com/content/schema-stitching/ex1)
  - [Conflicts for root fields](https://www.advancedgraphql.com/content/schema-stitching/ex2)
  - [Conflicts for types](https://www.advancedgraphql.com/content/schema-stitching/ex3)
  - [Merging an executable with a non-executable schema](https://www.advancedgraphql.com/content/schema-stitching/ex4)

## Exercise

For a given datasource ([abstracted as json here](datasource/data.json)) containing `n` rows of `skills` and `n` rows of `persons` we provided a sample implementation of a GraphQL server for each technology containing:

- a server app
- a schema
- a resolver map
- an entity model
- a db abstraction

The code contains the solution for previous exercises so you can have a starting point example.

### Exercise requirements

This exercise might require additional instructions depending on the technology, please read them carefully before starting.

#### Schema

- Identify and separate the SDL document into 4 documents in a per-type basis
- Here a completely arbitrary example (you can experiment different setups if you want)
  - globalSearch.gql
  - person.gql
  - schema.gql
  - skill.gql
- Update the server app in order to include and stitch all schemas together
- You can directly import the *.gql files or use a technology specific utility like `apollo-tools` or whatever you want

As you can see, the challenge here will depend on the technology and we encourage you to experiment and try different approaches.

#### Operations list

All previous operations MUST work regardless the stitching technique implemented.

#### Technologies

Select the exercise on your preferred technology:

- [JavaScript](javascript/README.md)
- [Java](java/README.md)
- [Python](python/README.md)
- [NetCore](netcore/README.md)

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
- Apollo Blog
  - [Introducing Apollo Federation](https://www.apollographql.com/blog/apollo-federation-f260cf525d21)
- Apollo GraphQL
  - [Schema Stitching](https://www.apollographql.com/docs/graphql-tools/schema-stitching/)
  - [Apollo Federation](https://www.apollographql.com/docs/apollo-server/federation/introduction/)
  - [Migrating from schema stitching](https://www.apollographql.com/docs/apollo-server/federation/migrating-from-stitching/)
- graphql-tools
  - [Schema Delegation](https://www.graphql-tools.com/docs/schema-delegation)
  - [Remote Schemas](https://www.graphql-tools.com/docs/remote-schemas)
  - [Schema Wrapping](https://www.graphql-tools.com/docs/schema-wrapping)
  - Schema Merging
    - [Type definitions (SDL) merging](https://www.graphql-tools.com/docs/merge-typedefs)
    - [Resolvers merging](https://www.graphql-tools.com/docs/merge-resolvers)
    - [GraphQLSchema merging](https://www.graphql-tools.com/docs/merge-schemas)
    - [Schema stitching](https://www.graphql-tools.com/docs/schema-stitching)

- Advanced GraphQL dot com
  - [Schema Stitching](https://www.advancedgraphql.com/content/schema-stitching)
  - [Schema Federation](https://www.advancedgraphql.com/content/schema-federation)
- Youtube
  - [GraphQL Schema Design @ Scale](https://youtu.be/pJamhW2xPYw) by Marc-André Giroux (**amazing video**)
  - [Migrating Apollo’s Data Graph from Schema stitching to Federation](https://www.youtube.com/watch?v=ra5WuUvQRIM) by Adam Zionts
  
- Other articles
  - [Principled GraphQL](https://principledgraphql.com/) by Geoff Schmidt and Matt DeBergalis
  - [Domain-Driven GraphQL Schema Design | Enterprise GraphQL](https://khalilstemmler.com/articles/graphql/ddd/schema-design/) by Khalil Stemmler (**highly recommended**)
  - [GraphQL Federation vs Stitching](https://gunargessner.com/graphql-federation-vs-stitching) by Gunar Gessner
  - [GraphQL Stitching versus Federation](https://seblog.nl/2019/06/04/2/graphql-stitching-versus-federation) by Sebastiaan Andeweg
  - [Advice from a GraphQL Expert](https://www.netlify.com/blog/2020/01/21/advice-from-a-graphql-expert/) by Sarah Drasner (and Francesca Guiducci)
  - The Guild
    - [graphql-tools - Schema Stitching](https://www.graphql-tools.com/docs/schema-stitching)
    - [GraphQL Tools is back - next generation schema stitching and new leadership](https://the-guild.dev/blog/graphql-tools-v6)
    - [The Guild is taking over maintenance of merge-graphql-schemas](https://the-guild.dev/blog/taking-over-merge-graphql-schemas)
  - GraphCMS
    - [GraphQL Schema Stitching](https://graphcms.com/blog/graphql-schema-stitching)
  - Ariadne
    - [Apollo Federation](https://ariadnegraphql.org/docs/apollo-federation)
