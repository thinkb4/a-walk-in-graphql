const low = require('lowdb');
const FileSync = require('lowdb/adapters/FileSync');

const adapter = new FileSync('./src/db/data.json');
const db = low(adapter);

const skillModel = require('./skill');

module.exports = {
  models: {
    Skill: skillModel(db),
  }
}
