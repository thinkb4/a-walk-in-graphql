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
    skill(obj, { id }, { models: { Skill } }) {
      return Skill.find({ id });
    },
    /**
     * 
     */
    skills(obj, { id }, { models: { Skill } }) {
      return Skill.filter({ id });
    },
    /**
     * 
     */
    person(obj, { id }, { models: { Person } }) {
      return Person.find({ id });
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
    friends({ friends = [] }, { id }, { models: { Person } }) {
      return Person.filter({ id }, friends);
    },
    /**
     * 
     */
    skills({ skills = [] }, { id }, { models: { Skill } }) {
      return Skill.filter({ id }, skills);
    },
    /**
     * 
     */
    favSkill({ favSkill: id }, args, { models: { Skill } }) {
      return Skill.find({ id });
    },
  }
}
