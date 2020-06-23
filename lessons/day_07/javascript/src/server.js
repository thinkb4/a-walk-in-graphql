
const fs = require('fs');
const path = require('path');

const { ApolloServer } = require('apollo-server');

const resolvers = require('./resolvers/resolvers');
const TD_BASE = fs.readFileSync('./src/schema/schema.gql', "utf8").toString();
const TD_PERSON = fs.readFileSync('./src/schema/person.gql', "utf8").toString();
const TD_SKILL = fs.readFileSync('./src/schema/skill.gql', "utf8").toString();
const TD_GLOBAL_SEARCH = fs.readFileSync('./src/schema/globalSearch.gql', "utf8").toString();

const { models } = require('./db');

/**
 * 
 * @see https://www.apollographql.com/docs/apollo-server/api/apollo-server/
 */
const server = new ApolloServer({
  typeDefs: [TD_BASE, TD_PERSON, TD_SKILL, TD_GLOBAL_SEARCH],
  resolvers,
  context() {
    return { models };
  }
});

server.listen(4000).then(({ url }) => {
  console.log(`💥 BANG! I'm listening to ${url}`);
});
