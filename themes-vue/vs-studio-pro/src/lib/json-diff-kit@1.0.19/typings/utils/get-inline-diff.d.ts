export interface InlineDiffOptions {
    mode?: 'char' | 'word';
    wordSeparator?: string;
}
export interface InlineDiffResult {
    type: 'add' | 'remove' | 'equal';
    text: string;
}
declare const getInlineDiff: (l: string, r: string, options: InlineDiffOptions) => InlineDiffResult[][];
export default getInlineDiff;
