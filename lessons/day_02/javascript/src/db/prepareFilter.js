const isPlainObject = require('lodash/isPlainObject');

/**
 * 
 * @param {Object|Function} filter { key: value [, key: value] } | predicate
 */
const sanitize = (filter) => {

  if (!isPlainObject(filter)) {
    return filter;
  }

  const conf = Object.entries(filter).reduce((acc, entry) => {
    let [key, val] = entry;
    // avoid including keys with undefined values
    return val === undefined ? acc : { ...acc, [key]: val };

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
