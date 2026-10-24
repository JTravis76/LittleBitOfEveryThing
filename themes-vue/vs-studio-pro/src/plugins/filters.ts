import { App } from "vue";

export interface IFilter {
  /** format date to "Thu Oct 10 2019" */
  toDateString(value?: string | null): string;
  /** format date to "11/1/2019" */
  toLocaleDateString(value?: string | null): string;
  /** format date to "11/1/2019" */
  shortDateString(value?: string | null): string;
  /** format date to "Dec 12, 2022" */
  longDateString(value?: string | null): string;
  /** format date to "2019-09-11"
   * To be used to prefill  \<input type="date" \/\>
   */
  dateTypeString(value?: string | null): string;
  /** format date to "2019-09-11T04:09"
   * To be used to prefill  <input type="datetime-local" />
   */
  datetimelocalTypeString(value?: string | null): string;
  shortTimeString(value?: string | null): string;
  shortDateTimeString(value?: string | null): string;
  /** format date to "1/11/2023 2:00:00 PM" utc to local */
  utcLocalDateTimeString(value: string | null): string;
  monthYearString(value?: string | null, long?: boolean): string;
  truncateTen(value?: string | null): string;
  lessThan(value?: string | null): string;
  greaterThan(value?: string | null): string;
  currency(value?: string | number | null, prefix?: boolean): string;
}

const shortMonths = [
  "Jan",
  "Feb",
  "Mar",
  "Apr",
  "May",
  "Jun",
  "Jul",
  "Aug",
  "Sep",
  "Oct",
  "Nov",
  "Dec",
];

const longMonths = [
  "January",
  "Febuary",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];

/** add a prefix zero to any single digit number */
function prependZero(num: number) {
  return num < 10 ? `0${num}` : num;
}

/** format date to "Thu Oct 10 2019" */
function toDateString(value: string | null): string {
  if (value) {
    return new Date(value).toDateString(); // -> Thu Oct 10 2019
  }
  return "";
}
/** format date to "11/1/2019" */
function shortDateString(value: string | null): string {
  if (value) {
    const d = new Date(Date.parse(value));
    return d.getMonth() + 1 + "/" + d.getDate() + "/" + d.getFullYear();
  }
  return "";
}
/** format date to "Dec 12, 2022" */
function longDateString(value: string | null): string {
  if (value) {
    if (!value.includes("T")) {
      value += "T00:00";
    }
    const d = new Date(Date.parse(value));
    return `${shortMonths[d.getMonth()]} ${d.getDate()}, ${d.getFullYear()}`;
  }
  return "";
}
/** format date to "2019-09-11"
 * To be used to prefill  <input type="date" />
 */
function dateTypeString(value: string | null): string {
  // https://css-tricks.com/prefilling-date-input/
  if (value) {
    const d = new Date(Date.parse(value));
    return `${d.getFullYear()}-${prependZero(d.getMonth() + 1)}-${prependZero(
      d.getDate(),
    )}`;
  }
  return "";
}
/** format date to "2019-09-11T04:09" yyyy-MM-ddThh:mm [optional (:ss.SSS)]
 * To be used to prefill  <input type="datetime-local" />
 */
function datetimelocalTypeString(value: string | null): string {
  // https://css-tricks.com/prefilling-date-input/
  if (value) {
    const d = new Date(Date.parse(value));
    return `${d.getFullYear()}-${prependZero(d.getMonth() + 1)}-${prependZero(
      d.getDate(),
    )}T${prependZero(d.getHours())}:${prependZero(d.getMinutes())}`;
  }
  return "";
}
/** format date to "11/1/2019" */
function toLocaleDateString(value?: string | null): string {
  if (value) {
    const d = new Date(Date.parse(value));
    return d.toLocaleDateString();
  }
  return "";
}
/** format date to '2:46 PM" */
function shortTimeString(value: string | null): string {
  if (value) {
    const d = new Date(Date.parse(value));
    return d.toLocaleTimeString("en-US");
  }
  return "";
}
/** format date to "1/28/2020 12:17:02 PM" */
function shortDateTimeString(value: string | null): string {
  if (value) {
    // let d = "\/Date(1576156189806)\/";
    const d = new Date(Date.parse(value));
    return (
      d.getMonth() +
      1 +
      "/" +
      d.getDate() +
      "/" +
      d.getFullYear() +
      " " +
      d.toLocaleTimeString()
    );
  }
  return "";
}
/** format date to "1/11/2023 2:00:00 PM" utc to local */
function utcLocalDateTimeString(value: string | null): string {
  if (value) {
    return shortDateTimeString(`${value}${"Z"}`);
  }
  return "";
}
/** format date to "Jan 2020" */
function monthYearString(value: string | null, long?: boolean): string {
  if (value) {
    const d = new Date(Date.parse(value));
    if (long && long === true) {
      return longMonths[d.getMonth()] + " " + d.getFullYear().toString();
    }
    return shortMonths[d.getMonth()] + " " + d.getFullYear().toString();
  }
  return "";
}
function truncateTen(value: string | null): string {
  if (value) {
    if (String(value).length > 10)
      return String(value).substring(0, 10) + "...";
    else return value;
  }
  return "";
}
function lessThan(value: string | null): string {
  if (value) {
    return String.fromCharCode(0x003c) + value;
  }
  return "";
}
function greaterThan(value: string | null): string {
  if (value) {
    return String.fromCharCode(0x2265) + value;
  }
  return "";
}
function currency(value?: string | number | null, prefix = true): string {
  if (value) {
    let v = 0;

    if (typeof value === "string") {
      v = parseFloat(value);
    } else if (typeof value === "number") {
      v = value;
    }

    if (prefix) {
      return new Intl.NumberFormat("en", {
        style: "currency",
        currency: "USD",
        minimumFractionDigits: 2,
      }).format(v);

      // return parseInt(value.toString()).toLocaleString("en",
      // {
      //   style: "currency",
      //   minimumFractionDigits: 2,
      //   currency: 'USD',
      // });
    }

    return parseFloat(value.toString()).toFixed(2);
  }
  return "";
}

const filters = {
  toDateString,
  toLocaleDateString,
  shortDateString,
  longDateString,
  dateTypeString,
  datetimelocalTypeString,
  shortTimeString,
  shortDateTimeString,
  utcLocalDateTimeString,
  monthYearString,
  truncateTen,
  lessThan,
  greaterThan,
  currency,
};

export default filters;

export const filterPlugin = {
  install: (app: App) => {
    app.config.globalProperties.$filters = filters;
  },
};
