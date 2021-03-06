define(['../internal/isHostObject', '../internal/isObjectLike', './isPlainObject', '../support'], function(isHostObject, isObjectLike, isPlainObject, support) {

  /** Used for native method references. */
  var objectProto = Object.prototype;

  /**
   * Used to resolve the `toStringTag` of values.
   * See the [ES spec](https://people.mozilla.org/~jorendorff/es6-draft.html#sec-object.prototype.tostring)
   * for more details.
   */
  var objToString = objectProto.toString;

  /**
   * Checks if `value` is a DOM element.
   *
   * @static
   * @memberOf _
   * @category Lang
   * @param {*} value The value to check.
   * @returns {boolean} Returns `true` if `value` is a DOM element, else `false`.
   * @example
   *
   * _.isElement(document.body);
   * // => true
   *
   * _.isElement('<body>');
   * // => false
   */
  function isElement(value) {
    return (value && value.nodeType === 1 && isObjectLike(value) &&
      (support.nodeTag ? (objToString.call(value).indexOf('Element') > -1) : isHostObject(value))) || false;
  }
  // Fallback for environments without DOM support.
  if (!support.dom) {
    isElement = function(value) {
      return (value && value.nodeType === 1 && isObjectLike(value) && !isPlainObject(value)) || false;
    };
  }

  return isElement;
});
