/* eslint-disable @typescript-eslint/no-explicit-any */

// Note: PropertyKey is a builtIn type alias of
// type PropertyKey = string | number | symbol
// This lets us use "Record<PropertyKey, any>" to represent any object
// but is slightly nicer to use than the "object" type
export function sortBy<T extends Record<PropertyKey, any>>(
  arr: T[],
  key1: keyof T,
  key2: keyof T,
): T[] {
  return arr.sort((a, b) => {
    if (a[key1] > b[key1]) return 1;
    if (a[key1] < b[key1]) return -1;

    const c = parseInt(a[key2]?.toString(), 10);
    const d = parseInt(b[key2]?.toString(), 10);
    if (c > d) return 1;
    if (c < d) return -1;

    return 0;
  });
}

export default sortBy;
