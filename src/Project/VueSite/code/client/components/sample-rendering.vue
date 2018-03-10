<template>
    <div>
        <h1 class="h1">{{ fields.title.value }}</h1>
        <p></p>
        <p></p>
        <div v-html="fields.text.value">
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {

        }
    },
        props: {
            fields: {
                type: Object,
                required: true
            }
        }
}
</script>
<style scoped>
</style>
