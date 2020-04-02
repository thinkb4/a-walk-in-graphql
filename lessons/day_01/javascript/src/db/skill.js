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

      const randSkill = skills[Math.floor(Math.random() * skills.length)];

      /**
       * resolver's param is for a DAY 2 exercise
       * let's populate the parent by hand here
       */
      randSkill.parent = skills.find(({id}) => id === randSkill.parent);

      return randSkill;
    },
  }
}

module.exports = skillModel;
