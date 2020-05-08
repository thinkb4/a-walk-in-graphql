
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

const { prepareFilter } = require('./prepareFilter');

const Skill = require('./skill').model({ db, prepareFilter });
const Person = require('./person').model({ db, prepareFilter });

module.exports = {
  models: {
    Skill,
    Person
  }
}
