// https://github.com/pearofducks/mount-vue-component/blob/master/index.js
import { createVNode, render } from "../deps.js";

export const mount = (component, { props, children, element, app } = {}) => {
  let el = element;

  const vNode = createVNode(component, props, children);
  if (app && app._context) vNode.appContext = app._context;
  if (el) render(vNode, el);
  else if (typeof document !== "undefined")
    render(vNode, (el = document.createElement("div")));

  const destroy = () => {
    if (el) render(null, el);
    el = null;
    vNode.appContext = null;
  };

  return { vNode, destroy, el };
};
