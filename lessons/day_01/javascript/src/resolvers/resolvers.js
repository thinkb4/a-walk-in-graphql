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
  },
  Skill: {
    /**
     * Field-level resolver
     * 
     * @param {Object} obj 
     * @param {Object} args 
     * @param {Object} context
     * @param {Object} info 
     * 
     * @returns {Number}
     */
    now (obj, args, context, info) {
      return Date.now();
    },
    /**
     * Field-level resolver
     * 
     * @param {Object} obj 
     * @param {Object} args 
     * @param {Object} context
     * @param {Object} info 
     * 
     * @returns {Object|Undefined}
     */
    parent({ parent: id }, args, { models: { Skill } }) {
      return Skill.find({ id });
    },
  }
}
