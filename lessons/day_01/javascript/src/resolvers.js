/**
 * Here are your Resolvers for your Schema.
 * They must match the type definitions in your schema
 */

module.exports = {
  Query: {
    randomSkill(obj, { input }, context, info) {
      return context.models.Skill.randomSkill(input)
    }
  }
}
