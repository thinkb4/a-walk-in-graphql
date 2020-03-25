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

      return skills[Math.floor(Math.random() * skills.length)];
    },
  }
}

module.exports = skillModel;
