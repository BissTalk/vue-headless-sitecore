<template>
    <div id="app">
        <nav-menu params="route: route"></nav-menu>
        <app-page v-bind:path="$route.path"></app-page>
    </div>

</template>

<script>
import Vue from 'vue'
import AppPage from './app-page'
import NavMenu from './nav-menu'

Vue.component('app-page', AppPage);
Vue.component('nav-menu', NavMenu);

    export default {

        data() {
            return {
            };
        }
}
</script>

<style>
</style>
