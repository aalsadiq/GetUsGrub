<template>
  <div>
    <app-header></app-header>

    <div>
      <h1> {{ name }} </h1>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script lang="ts">
  import Header from './Header.vue'
  import Footer from './Footer.vue'

export default {
    name: 'Registration',
    components: {
      'app-header': Header,
      'app-footer': Footer
    },
    data() {
      return {
        }
      },
    methods: {
      register() {

      }
    },
    computed: {

    }
}
</script>
<style scoped>

</style>
