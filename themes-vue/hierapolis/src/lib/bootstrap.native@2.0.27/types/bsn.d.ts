interface BSN {
  /**Initialize Data API */
  initCallback(): void;
  supports: any[];
  version: string;
}
interface Window {
  BSN: BSN
}
declare var BSN: BSN;

declare module "bootstrap.native" {
  export default BSN;
}