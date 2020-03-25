
const fs = require('fs');
const path = require('path');

const { ApolloServer, gql } = require('apollo-server');
const resolvers = require('./resolvers');
const { models, db } = require('./db');

const typeDefs = fs.readFileSync('./src/schema/schema.gql', "utf8").toString();

const server = new ApolloServer({
  typeDefs,
  resolvers,
  context() {
    return { models, db };
  }
});

server.listen(4000).then(({ url }) => {
  console.log(`Server ready at ${url}`);
});
