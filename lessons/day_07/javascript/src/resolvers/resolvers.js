/**
 * Here are your Resolvers for your Schema.
 * They must match the type definitions in your schema
 */

const genericPersonFieldLevelResolvers = {
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
};

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
    /**
     * 
     */
    search(obj, { input }, { models: { Person, Skill } }) {
      const searchTerm = input.name;
      return [
        ...Person.searchByName(searchTerm),
        ...Skill.searchByName(searchTerm)
      ];
    },
  },
  Mutation: {
    /**
     * 
     */
    createSkill(obj, { input }, { models: { Skill } }) {
      return Skill.create(input);
    },
    /**
     * 
     */
    createPerson(obj, { input }, { models: { Person } }) {
      return Person.create(input);
    },
    /**
     * 
     */
    createCandidate(obj, { input }, { models: { Person } }) {
      return Person.create(input);
    },
    /**
     * 
     */
    createEngineer(obj, { input }, { models: { Person } }) {
      return Person.create(input);
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
  GlobalSearch: {
    __resolveType({ grade, targetGrade, parent }, context, info, returnType) {

      if (undefined !== parent) {
        return 'Skill';
      }

      if (undefined !== grade) {
        return 'Engineer';
      }

      if (undefined !== targetGrade) {
        return 'Candidate';
      }

      return 'Contact';
    },
  },
  Person: {
    __resolveType({ grade, targetGrade }, context, info, returnType) {

      if (undefined !== grade) {
        return 'Engineer';
      }

      if (undefined !== targetGrade) {
        return 'Candidate';
      }

      return 'Contact';
    },
  },
  Employee: {
    __resolveType({ grade }, context, info, returnType) {

      if (undefined !== grade) {
        return 'Engineer';
      }

      return null;
    },
  },
  /**
   * 
   */
  Contact: { ...genericPersonFieldLevelResolvers },
  /**
   * 
   */
  Candidate: { ...genericPersonFieldLevelResolvers },
  /**
   * 
   */
  Engineer: { ...genericPersonFieldLevelResolvers }
}
