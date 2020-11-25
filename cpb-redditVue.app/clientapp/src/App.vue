<template>
  <div id="app">
    <PostList title="VueReddit"/>
  </div>
</template>

<script>
    import PostList from './components/PostList.vue'
    import './assets/css/bootstrap.min.css'
    import 'bootstrap-vue/dist/bootstrap-vue.css'

    export default {
        name: 'app',
        components: {
            PostList
        },
        data: function () {
            return {};
        }
    }
</script>

<style>
#app {

}
</style>
