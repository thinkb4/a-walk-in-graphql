
/**
 * Setup LowDB
 * @see https://openbase.io/js/lowdb
 */
const low = require('lowdb');
const FileSync = require('lowdb/adapters/FileSync');

// using common data source for all examples
const cwd = process.cwd();
const adapter = new FileSync(`${cwd}/../datasource/data.json`);
const db = low(adapter);

const Skill = require('./skill')(db);
const Person = require('./person')(db);

module.exports = {
  models: {
    Skill,
    Person
  }
}
