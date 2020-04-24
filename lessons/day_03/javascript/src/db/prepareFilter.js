const isPlainObject = require('lodash/isPlainObject');

/**
 * 
 * @param {Object|Function} filter { key: value [, key: value] } | predicate
 */
const sanitize = (filter) => {

  if (!isPlainObject(filter)) {
    return filter;
  }

  const conf = Object.keys(filter).reduce((acc, key) => {

    let val = filter[key];

    switch (val) {
      case undefined:
        return acc; // avoid including keys with undefined values
      default:
        return {
          ...acc,
          [key]: val
        }
        break;
    }

  }, {});

  return Object.keys(conf).length ? conf : undefined;

}

/**
 * Returns a function with the first argument (filter) sanitized
 * @param {Function} fn HOR 
 */
const prepareFilter = fn => (filter, ...rest) => {
  return fn(sanitize(filter), ...rest)
}

module.exports = {
  prepareFilter
}
