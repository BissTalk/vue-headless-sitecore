<template>
    <div class="page">
        <div class="container">
            <keep-alive>
                <sitecore-controller v-bind:path="path" />
            </keep-alive>
        </div>
    </div>
</template>

<script>
    export default {
        computed: {
            path: function () {
                return this.$route.path;
            }
        }
}
</script>

<style>
  #Header{
  padding: 40px;
  }
</style>
