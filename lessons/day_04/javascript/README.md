# [A walk in GraphQL](/README.md)

## Day 4 exercise - Javascript

Read the instructions on the [Day 4 exercise](../day_04.md#exercise) definition

### Run the server

1. open a terminal
2. go to the javascript exercise directory
3. run `yarn` or `npm install` to install the dependencies if you didn't before
4. run `yarn server` or `npm run server` to start the GraphQL server
5. open your browser and type `http://localhost:4000/` to display the GraphQL playground so you can run the queries against the server

### The project

- [Data source](../datasource/data.json)
- [Server app](src/server.js)
- [Schema](src/schema/schema.gql)
- [Resolver map](src/resolvers/resolvers.js)
- [Skill entity model](src/db/skill.js)
- [Person entity model](src/db/person.js)
- [db abstraction](src/db/index.js)
