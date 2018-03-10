<template>
    <div :id="placeholderKey">
        <template v-for="rendering in getRenderings">
            <template v-if="isValid(rendering.componentName)">
                <component v-bind:is="rendering.componentName" v-bind:item="item" v-bind:fields="getFields(item , rendering)" v-bind:placeholders="rendering.placeholders" />
            </template>
        </template>
    </div>
</template>
<script>
    import Vue from 'vue';
    export default {
        props: {
            placeholders: {
                type: Object,
                required: false
            },
            placeholderKey: {
                type: String,
                default: 'not-set'
            },
            item: {
                type: Object,
                required: true
            }
        },
        data() {
            return {
                loadedTypes : Object.keys(Vue.options.components)
            };
        },
        computed: {
            getRenderings: function () {
                if (!this.placeholders) {
                    return [];
                }
                var result = this.placeholders[this.placeholderKey];
                if (Array.isArray(result)) {
                    return result;
                }
                return [];
            }
        },
        methods: {
            isValid: function (name) {
                return Object.keys(Vue.options.components).indexOf(name) > -1;
            },
            getFields: function(item, rendering) {
                if (rendering.fields && isNaN(rendering.fields)) {
                    return rendering.fields;
                }
                if (item.fields && isNaN(item.fields)) {
                    return item.fields;
                }
                return {};
            }
        }
    }
</script>
<style scoped>
</style>
