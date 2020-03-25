
const fs = require('fs');
const path = require('path');

const { ApolloServer } = require('apollo-server');

const resolvers = require('./resolvers/resolvers');
const typeDefs = fs.readFileSync('./src/schema/schema.gql', "utf8").toString();

const { models } = require('./db');

/**
 * 
 * @see https://www.apollographql.com/docs/apollo-server/api/apollo-server/
 */
const server = new ApolloServer({
  typeDefs,
  resolvers,
  context() {
    return { models };
  }
});

server.listen(4000).then(({ url }) => {
  console.log(`💥 BANG! I'm listening to ${url}`);
});
