
const fs = require('fs');
const path = require('path');

const { ApolloServer } = require('@apollo/server');
const { startStandaloneServer } = require('@apollo/server/standalone');

const resolvers = require('./resolvers/resolvers');
const typeDefs = fs.readFileSync('./src/schema/schema.gql', "utf8").toString();

const { models } = require('./db');

/**
 * 
 * @see https://www.apollographql.com/docs/apollo-server/api/apollo-server/
 */
const server = new ApolloServer({ typeDefs, resolvers });

(async () => {
  const { url } = await startStandaloneServer(server, {
    context: async () => ({ models }),
    listen: { port: 4000 },
  });

  console.log(`💥 BANG! I'm listening to ${url}`);
})();
