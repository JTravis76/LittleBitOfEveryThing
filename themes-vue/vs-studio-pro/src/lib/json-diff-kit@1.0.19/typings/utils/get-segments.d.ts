import type { DiffResult } from '../differ';
import type { HideUnchangedLinesOptions } from '../viewer';
export interface SegmentItem {
    start: number;
    end: number;
    isEqual: boolean;
}
export interface HiddenUnchangedLinesInfo extends SegmentItem {
    hasLinesBefore: boolean;
    hasLinesAfter: boolean;
}
declare const getSegments: (l: DiffResult[], r: DiffResult[], options: HideUnchangedLinesOptions) => (SegmentItem | HiddenUnchangedLinesInfo)[];
export default getSegments;
