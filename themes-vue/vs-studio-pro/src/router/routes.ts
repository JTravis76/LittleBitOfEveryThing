export default [
  {
    path: "/:pathMatch(.*)*",
    name: "Not Found",
    component: () => import("../views/NotFound.vue"),
    meta: { description: "Page Missing" },
  },
  {
    path: "/",
    redirect: "/home",
  },
  {
    path: "/home",
    name: "Home",
    component: () => import("../views/TheHome.vue"),
    meta: { description: "Home Sweet Home" },
  },
];
