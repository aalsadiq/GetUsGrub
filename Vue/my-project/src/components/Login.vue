<template>
  <div id="app">
    <app-header></app-header>

    <div>
      <h1> {{ name }} </h1>
      <div>
        <form name="signIn" action="http://localhost:8080/signIn" method="post">
          <div>
            <label for="email"> Username </label>
            <input type="text" />
          </div>
        </form>
      </div>
    </div>
    <div class="wrapper">
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
  import Header from './Header.vue'
  import Footer from './Footer.vue'

export default {
    name: 'Login',
    components: {
      'app-header': Header,
      'app-footer': Footer
    },
    data() {
      return {
        }
      },
    methods: {
      signIn() {
        if (this.$data.username !== 'test' || this.$data.password !== 'test') {
          this.$data.hasErrored = true
        }
      }
    },
    computed: {

    }
}
</script>
<style scoped>

</style>
