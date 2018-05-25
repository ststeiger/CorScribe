define(['../internal/arrayCopy', '../internal/isLength', './isString', '../support', '../object/values'], function(arrayCopy, isLength, isString, support, values) {

  /**
   * Converts `value` to an array.
   *
   * @static
   * @memberOf _
   * @category Lang
   * @param {*} value The value to convert.
   * @returns {Array} Returns the converted array.
   * @example
   *
   * (function() {
   *   return _.toArray(arguments).slice(1);
   * }(1, 2, 3));
   * // => [2, 3]
   */
  function toArray(value) {
    var length = value ? value.length : 0;
    if (!isLength(length)) {
      return values(value);
    }
    if (!length) {
      return [];
    }
    return (support.unindexedChars && isString(value))
      ? value.split('')
      : arrayCopy(value);
  }

  return toArray;
});
