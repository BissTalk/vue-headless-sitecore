<template>
    <div>
        <div class="hero-image">
            <div class="hero-text">
                <div id="Header">
                    <img src="/-/media/Default Website/sc_logo.png" alt="Sitecore" id="scLogo">
                </div>
            </div>
        </div>
        <placeholder placeholderKey="centercolumn" v-bind:placeholders="placeholders" v-bind:item="item" />
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
