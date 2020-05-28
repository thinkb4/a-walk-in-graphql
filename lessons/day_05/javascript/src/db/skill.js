const nanoid = require('nanoid');
const DATASET_KEY = 'skills';

/**
 * @param {Object} Ø
 * @param {Object} Ø.db 
 * @param {Function} Ø.prepareFilter 
 * @param {String} Ø.datasetKey
 */
const model = ({ db, prepareFilter, datasetKey = DATASET_KEY } = {}) => {
  return {
    /**
     * 
     * @returns {Object}
     */
    randomRecord() {
      /**
       * @see https://openbase.io/js/lowdb
       */
      const records = db.get(datasetKey).value();

      return Reflect.get(records, Math.floor(Math.random() * records.length));
    },
    /**
     * 
     * @param {Object|Function} filter { key: value [, key: value] } | predicate
     * 
     * @returns {Object|Undefined}
     */
    find: prepareFilter((filter) => {
      return filter && db.get(datasetKey)
        .find(filter)
        .value()
    }),
    /**
     * 
     * @param {Object|Function} filter { key: value [, key: value] } | predicate
     * @param {Array<String>} subset an array of records id
     * 
     * @returns {Array<Object>}
     */
    filter: prepareFilter((filter, subset) => {
      return db.get(datasetKey)
        .filter(record => !subset || subset.includes(record.id))
        .filter(filter)
        .value()
    }),
    /**
     * 
     * @param {Object} input
     * @returns {Object}
     */
    create(input) {
      const record = { ...input, id: nanoid() };

      db.get(DATASET_KEY)
        .push(record)
        .write()

      return record;
    },
    /**
     * Case insensitive substring search by name
     * @param {String} searchTerm
     * 
     * @returns {Array<Object>}
     */
    searchByName(searchTerm = '') {
      const term = searchTerm.toLowerCase();
      return db.get(datasetKey)
        .filter(record => record.name.toLowerCase().includes(term))
        .value()
    },
  }
}

module.exports = {
  model,
  DATASET_KEY
};
