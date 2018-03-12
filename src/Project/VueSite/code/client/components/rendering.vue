<template>
    <div></div>
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
            },
            item: {
                type: Object,
                required: true
            },
            placeholders: {
                type: Object,
                required: false
            }
        }
}
</script>
<style scoped>
</style>
