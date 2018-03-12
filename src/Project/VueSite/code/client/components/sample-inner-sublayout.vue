<template>
    <div class="centercolumn">
        <placeholder placeholderKey="content" v-bind:placeholders="placeholders" v-bind:item="item" />
    </div>  
</template>
<script>
    import rendering from "components/rendering";
    export default {
        extends: rendering
    }
</script>
<style scoped>
    .centercolumn {
        padding: 50px;
    }
</style>
