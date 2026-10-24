import type { DifferOptions, DiffResult, ArrayDiffFunc } from '../differ';
declare const diffObject: (lhs: Record<string, any>, rhs: Record<string, any>, level: number, options: DifferOptions, arrayDiffFunc: ArrayDiffFunc) => [DiffResult[], DiffResult[]];
export default diffObject;
