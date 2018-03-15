// ReSharper disable InconsistentNaming
import "bootstrap";
import Vue from "vue";
import axios from "axios";
import router from "./router";
import Application from "components/app-root";
import SitecoreController from "components/sitecore-controller";
import NotFound from "components/not-found";
import Placeholder from "components/sitecore-placeholder";
import SitecoreLoader from "components/sitecore-loader";
import SampleSublayout from "components/sample-sublayout";
import SampleInnerSublayout from "components/sample-inner-sublayout";
import SampleRendering from "components/sample-rendering";

// ReSharper enable InconsistentNaming

Vue.prototype.$http = axios;
Vue.component("sitecore-controller", SitecoreController);
Vue.component("not-found", NotFound);
Vue.component("placeholder", Placeholder);
Vue.component("sitecore-loader", SitecoreLoader);
Vue.component("SampleSublayout", SampleSublayout);
Vue.component("SampleInnerSublayout", SampleInnerSublayout);
Vue.component("SampleRendering", SampleRendering);

const app = new Vue({
    router,
    ...Application
});

export {
    app,
    router
    }