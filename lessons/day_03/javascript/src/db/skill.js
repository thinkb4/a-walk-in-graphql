const nanoid = require('nanoid');

const skillModel = db => {
  return {
    /**
     * 
     * @returns {Object}
     */
    randomSkill() {
      /**
       * @see https://openbase.io/js/lowdb
       */
      const skills = db.get('skills').value();

      return Reflect.get(skills, Math.floor(Math.random() * skills.length));
    },
    /**
     * 
     * @param {Object} filter { key: value [, key: value] }
     * 
     * @returns {Object|Undefined}
     */
    find(filter) {
      return db.get('skills')
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
      return db.get('skills')
        .filter(filter)
        .value()
    },
  }
}

module.exports = skillModel;
