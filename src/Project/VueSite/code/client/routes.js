import AppPage from "components/app-page";

export const routes = [
    { path: "/", component: AppPage, display: "Home", style: "fa fa-home" },
    { path: "*", component: AppPage }
];
