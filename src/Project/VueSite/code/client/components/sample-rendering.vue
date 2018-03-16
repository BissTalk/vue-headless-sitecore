<template>
    <div>
        <h1 class="h1" v-if="fields.title">{{ fields.title.value }}</h1>
        <p></p>
        <p></p>
        <div v-if="fields.text" v-html="fields.text.value">
        </div>
    </div>
</template>
<script>
    import rendering from "components/rendering";
    export default {
        extends: rendering
    }
</script>
<style scoped>
</style>
