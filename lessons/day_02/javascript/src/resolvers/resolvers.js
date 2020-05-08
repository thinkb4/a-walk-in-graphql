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
      return Skill.randomRecord();
    },
    /**
     * 
     */
    randomPerson(obj, args, { models: { Person } }) {
      return Person.randomRecord();
    },
    /**
     * 
     */
    persons(obj, { id }, { models: { Person } }) {
      return Person.filter({ id });
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
    parent({ parent: id }, args, { models: { Skill } }) {
      return Skill.find({ id });
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
      return Person.filter(args, friends);
    },
    /**
     * 
     */
    skills({ skills }, args, { models: { Skill } }) {
      return Skill.filter(args, skills);
    },
    /**
     * 
     */
    favSkill({ favSkill: id }, args, { models: { Skill } }) {
      return Skill.find({ id });
    },
  }
}
