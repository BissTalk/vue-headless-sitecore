<template>
    <div class="centercolumn">
        <placeholder placeholderKey="content" v-bind:placeholders="placeholders" v-bind:item="item" />
    </div>  
</template>
<script>
//import Vue from 'vue'
export default {
    data() {
        return {
        }
    },
    props: {
        item: {
            type: Object,
            required: true
        },
        placeholders: {
            type: Object,
            required: true
        }
    }
}
</script>
<style scoped>
    .centercolumn {
        padding: 50px;
    }
</style>
