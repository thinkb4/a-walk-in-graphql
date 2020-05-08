/**
 * Here are your Resolvers for your Schema.
 * They must match the type definitions in your schema
 */

module.exports = {
  EyeColor: {
    BLUE: 'blue',
    GREEN: 'green',
    BROWN: 'brown',
    BLACK: 'black',
  },
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
    skill(obj, { input }, { models: { Skill } }) {
      return Skill.find(input);
    },
    /**
     * 
     */
    skills(obj, { input }, { models: { Skill } }) {
      return Skill.filter(input);
    },
    /**
     * 
     */
    person(obj, { input }, { models: { Person } }) {
      return Person.find(input);
    },
    /**
     * 
     */
    persons(obj, { input }, { models: { Person } }) {
      return Person.filter(input);
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
    friends({ friends = [] }, { input }, { models: { Person } }) {
      return Person.filter(input, friends);
    },
    /**
     * 
     */
    skills({ skills = [] }, { input }, { models: { Skill } }) {
      return Skill.filter(input, skills);
    },
    /**
     * 
     */
    favSkill({ favSkill: id }, args, { models: { Skill } }) {
      return Skill.find({ id });
    },
  }
}
