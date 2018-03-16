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
    var self = this;
    export default {
        props: {
            path: {
                type: String,
                required: true
            }
        }
    }
</script>

<style>
  #Header{
  padding: 40px;
  }
</style>
