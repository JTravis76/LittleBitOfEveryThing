/* Vue Directives - Outside Click
 * https://shivam.dev/blog/outside-click
 */

// interface IDirective {
//   arg? : null;
//   value?: typeof Function;
// }

// eslint-disable-next-line
const instances = new Array<any>();

export const outsideClick = {
  // eslint-disable-next-line
  beforeMount(element: HTMLElement, binding: any) {
    const el = element;
    el.dataset.outsideClickIndex = instances.length.toString();

    if (binding.value === undefined || typeof binding.value !== "function") {
      // eslint-disable-next-line
      console.warn("outside-click directive is missing function");
      return;
    }

    const fn = binding.value;
    const click = (e: MouseEvent | TouchEvent) => {
      const eventTarget = e.target as HTMLElement;
      if (el !== eventTarget && !el.contains(eventTarget)) {
        fn(e);
      }
    };

    document.addEventListener("click", click);
    document.addEventListener("touchstart", click);

    if (instances.length > 0) {
      let found = false;
      for (let i = 0; i < instances.length - 1; i += 1) {
        if (instances[i] === null) {
          el.dataset.outsideClickIndex = i.toString();
          instances[parseInt(el.dataset.outsideClickIndex, 10)] = click;
          found = true;
          break;
        }
      }
      if (!found) {
        instances.push(click);
      }
    } else {
      instances.push(click);
    }
  },
  unmounted(element: HTMLElement) {
    if (element.dataset.outsideClickIndex) {
      const index = parseInt(element.dataset?.outsideClickIndex, 10);
      const handler = instances[index];
      document.removeEventListener("click", handler);
      document.removeEventListener("touchstart", handler);
      instances[index] = null;
    }
  },
};

export default outsideClick;
