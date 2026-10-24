import { ComponentCustomProperties } from "vue";
// import { Store } from "vuex";
// import apiManager from "../api";
// import { RootState } from "../store";

declare module "@vue/runtime-core" {
  // provide typings for `this.$store`
  interface ComponentCustomProperties {
    // $store: Store<RootState>;

    // /** HTTP client */
    // $http: typeof apiManager;
    // // $http: AxiosStatic;

    /** Check user profile if role is assigned */
    $UserInRole(role: string[] | string): boolean;

    // $filters: {
    //   /** format date to "Thu Oct 10 2019" */
    //   toDateString(value: string | null): string | null;
    //   /** format date to "11/1/2019" */
    //   toLocaleDateString(value: string | null): string | null;
    //   /** format date to "11/1/2019" */
    //   shortDateString(value: string | null): string | null;
    //   /** format date to "Dec 12, 2022" */
    //   longDateString(value: string | null): string | null;
    //   /** format date to "2019-09-11"
    //    * To be used to prefill  \<input type="date" \/\>
    //    */
    //   dateTypeString(value: string | null): string;
    //   /** format date to "2019-09-11T04:09"
    //    * To be used to prefill  <input type="datetime-local" />
    //    */
    //   datetimelocalTypeString(value: string | null): string;
    //   shortTimeString(value: string | null): string | null;
    //   shortDateTimeString(value: string | null): string | null;
    //   monthYearString(value: string | null, long?: boolean): string | null;
    //   truncateTen(value: string | null): string | null;
    //   lessThan(value: string | null): string | null;
    //   greaterThan(value: string | null): string | null;
    //   currency(value: string | number | null): string | null;
    // };

    // /** Determine if selected feature is activiated */
    // isFeatureActive(id: number): boolean;

    // $modal(id: string): {
    //   show(): void;
    //   hide(): void;
    //   toggle(): void;
    // };
  }
}
