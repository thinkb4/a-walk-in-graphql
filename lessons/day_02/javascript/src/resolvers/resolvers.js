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
    randomSkill(obj, args, context, info) {
      return context.models.Skill.randomSkill();
    },
    /**
     * 
     */
    randomPerson(obj, args, context, info) {
      return context.models.Person.randomPerson();
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
    parent({ parent }, args, context) {
      return context.models.Skill.find({ id: parent });
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
    friends({ friends }, args, context) {
      return context.models.Person.filter((friend) => friends.includes(friend.id));
    },
    /**
     * 
     */
    skills({ skills }, args, context) {
      return context.models.Skill.filter((skill) => skills.includes(skill.id));
    },
    /**
     * 
     */
    favSkill({ favSkill }, args, context) {
      return context.models.Skill.find({ id: favSkill });
    },
  }
}
