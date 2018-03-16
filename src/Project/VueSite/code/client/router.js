import Vue from "vue"
import VueRouter from "vue-router"

import { routes } from "./routes"

Vue.use(VueRouter);

let router = new VueRouter({
    mode: "history",
    routes
});

router.afterEach((to) => {
    router.app.$emit("afterNavigate", to);
});



export default router
