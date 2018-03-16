<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand"></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item" v-for="route in routes">
                        <router-link :to="route.path" class="nav-link">
                            <i :class="route.style" aria-hidden="true"></i>
                            {{ route.display }}
                        </router-link>
                    </li>
                    <li class="nav-item" v-for="route in sc_routes">
                        <router-link :to="route.ItemName" class="nav-link">
                            <i aria-hidden="true"></i>
                            {{ route.DisplayName }}
                        </router-link>
                    </li>
                </ul>
            </div>
         </div>
    </nav>
</template>

<script>
import { routes } from '../routes'

export default {
    data() {
        return {
            routes,
            collapsed: true,
            sc_routes: null
        }
    },
    methods: {
        toggleCollapsed: function(event){
            this.collapsed = !this.collapsed;
        }
    },
    mounted() {
        var self = this;
        var url = '/sitecore/api/ssc/item/110d559f-dea5-42ea-9c1c-8a5df7e70ef9/children?fields=ItemName,DisplayName';
        this.$http.get(url)
            .then(function (response) {
                self.sc_routes = response.data;
            });
    }
}
</script>

<style scoped>
.slide-enter-active, .slide-leave-active {
  transition: max-height .35s
}
.slide-enter, .slide-leave-to {
  max-height: 0px;
}

.slide-enter-to, .slide-leave {
  max-height: 20em;
}

.navbar-brand {
    background-size: 100%;
    width: 80px;
    margin: 10px 30px 10px 10px;
    height: 30px;
}
</style>
