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
  }
}

module.exports = skillModel;
