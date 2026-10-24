/* eslint-disable @typescript-eslint/no-explicit-any */
// https://github.com/pearofducks/mount-vue-component/blob/master/index.js
import { createVNode, render, VNode, VNodeProps } from "vue";

interface MountOptions {
  props?: VNodeProps | any;
  children?: unknown;
  element?: any;
  app?: any;
}

export const mount = (
  component: VNode | any,
  { props, children, element, app }: MountOptions = {},
) => {
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
