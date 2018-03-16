<template>
    <keep-alive>
        <component :is="componentName" placeholderKey="main" v-bind:placeholders="placeholders" v-bind:item="item" />
    </keep-alive>
</template>
<script>
    export default {
    data() {
            return {
                item: {},
                placeholders: {},
                componentName: null
            };
        },
        props: {
            serviceRoot : {
                type: String,
                default: '/headless/website'
            },
            path: {
                type: String,
                default: ''
            },
            lang: {
                tyserviceRootpe: String,
                default: 'en'
            }
        },
        watch: {
            serviceRoot: function() {
                this.updateRenderings(); 
            },
            path: function() {
                this.updateRenderings(); 
            },
            lang: function() {
                this.updateRenderings(); 
            }
        },
        methods: {
            updateRenderings: function () {;
                let self = this;
                let url = this.serviceRoot + this.path + '/' + this.lang + '.json';
                this.$http.get(url)
                    .then(function (response) {
                        var pageData = response.data;
                        self.item['fields'] = pageData.fields;
                        self.item.name = pageData.name;
                        self.item.displayName = pageData.displayName;
                        self.placeholders = pageData.placeholders;
                        self.componentName = "placeholder";
                    })
                    .catch(function (error) {
                        if (error.response && error.response.status == "404") {
                            self.componentName = "not-found";
                        }
                        console.error(error);
                    });      
            }
        },
        mounted() {
            this.updateRenderings();    
        }
}
</script>
<style scoped>
</style>
