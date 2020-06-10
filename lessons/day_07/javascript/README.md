# [A walk in GraphQL](/README.md)

## Day 7 exercise - Javascript

Read the instructions on the [Day 7 exercise](../day_07.md#exercise) definition

### Run the server

#### Using `YARN` >= 1.0.0 (with workspaces)

1. open a terminal on the root directory
2. run `yarn` to install the dependencies if you didn't before
3. run `yarn js_06` to start the GraphQL server

#### Using `NPM`

1. open a terminal
2. go to the javascript exercise directory
3. run `npm install` to install the dependencies if you didn't before
4. run `npm start` to start the GraphQL server

#### GraphQL Playground

Open your browser and type `http://localhost:4000/` to display the GraphQL playground so you can run the queries against the server

### The project

- [Data source](../datasource/data.json)
- [Server app](src/server.js)
- Type Definitions
  - [Common Schema](src/schema/schema.gql)
  - [Person](src/schema/person.gql)
  - [Skill](src/schema/skill.gql)
  - [Global Search](src/schema/globalSearch.gql)
- [Resolver map](src/resolvers/resolvers.js)
- [Skill entity model](src/db/skill.js)
- [Person entity model](src/db/person.js)
- [db abstraction](src/db/index.js)
