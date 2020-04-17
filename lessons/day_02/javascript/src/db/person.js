const nanoid = require('nanoid');

const personModel = db => {
  return {
    /**
     * 
     * @returns {Object}
     */
    randomPerson() {
      /**
       * @see https://openbase.io/js/lowdb
       */
      const persons = db.get('persons').value();

      return Reflect.get(persons, Math.floor(Math.random() * persons.length));
    },
    /**
     * 
     * @param {Object} filter { key: value [, key: value] }
     * 
     * @returns {Object|Undefined}
     */
    find(filter) {
      return db.get('persons')
        .find(filter)
        .value()
    },
    /**
     * 
     * @param {Object|Function} filter { key: value [, key: value] } | predicate
     * 
     * @returns {Array<Object>}
     */
    filter(filter) {
      return db.get('persons')
        .filter(filter)
        .value()
    },
  }
}

module.exports = personModel;
