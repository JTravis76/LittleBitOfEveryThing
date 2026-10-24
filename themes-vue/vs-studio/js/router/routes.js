export default [
  {
    path: "/:pathMatch(.*)*",
    name: "Not Found",
    component: () => import("../views/not-found.js"),
    meta: { description: "Page Missing" },
  },
  {
    path: "/",
    redirect: "/home",
  },
  {
    path: "/home",
    name: "Home",
    component: () => import("../views/home.js"),
    meta: { description: "Home Sweet Home" },
  },
];
