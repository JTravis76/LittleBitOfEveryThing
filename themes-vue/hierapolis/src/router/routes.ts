export default [
  {
    path: "/:pathMatch(.*)*",
    name: "Not Found",
    component: () => import("../views/NotFound.vue"),
    meta: {
      description: "Page Missing",
      breadcrumbs: [{ icon: "", link: "" }],
    },
  },
  {
    path: "/home",
    name: "Home",
    component: () => import("../views/TheHome.vue"),
    meta: {
      description: "Home Sweet Home",
      breadcrumbs: [{ icon: "", link: "" }],
    },
  },
];
