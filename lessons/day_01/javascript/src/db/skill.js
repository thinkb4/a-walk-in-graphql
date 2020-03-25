const nanoid = require('nanoid')

const skillModel = db => {
  return {
    randomSkill() {
      const skills = db.get('skills').value();
      return skills[Math.floor(Math.random()*skills.length)];
    },
  }
}

module.exports = skillModel;
