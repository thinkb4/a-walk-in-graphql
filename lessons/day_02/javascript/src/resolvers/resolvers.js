/**
 * Here are your Resolvers for your Schema.
 * They must match the type definitions in your schema
 */

module.exports = {
  /**
   * 
   */
  Query: {
    /**
     * Top-Level resolver
     * @see https://www.apollographql.com/docs/graphql-tools/resolvers/#resolver-function-signature
     * 
     * @param {Object} obj 
     * @param {Object} args 
     * @param {Object} context
     * @param {Object} info 
     * 
     * @returns {Object}
     */
    randomSkill(obj, args, { models: { Skill } }) {
      return Skill.randomSkill();
    },
    /**
     * 
     */
    randomPerson(obj, args, { models: { Person } }) {
      return Person.randomPerson();
    },
    /**
     * 
     */
    persons(obj, { id }, { models: { Person } }) {
      return Person.filter(person => id ? person.id == id : true);
    },
  },
  /**
   * 
   */
  Skill: {
    /**
     * 
     */
    now() {
      return Date.now();
    },
    /**
     * 
     */
    parent({ parent }, args, { models: { Skill } }) {
      return Skill.find({ id: parent });
    },
  },
  /**
   * 
   */
  Person: {
    /**
     * 
     */
    fullName({ name, surname }) {
      return `${name} ${surname}`;
    },
    /**
     * 
     */
    friends({ friends }, args, { models: { Person } }) {
      return Person.filter((friend) => friends.includes(friend.id));
    },
    /**
     * 
     */
    skills({ skills }, args, { models: { Skill } }) {
      return Skill.filter((skill) => skills.includes(skill.id));
    },
    /**
     * 
     */
    favSkill({ favSkill }, args, { models: { Skill } }) {
      return Skill.find({ id: favSkill });
    },
  }
}
