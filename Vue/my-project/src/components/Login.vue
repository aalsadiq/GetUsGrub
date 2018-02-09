<template>
  <div id="app">
    <div id="top">
      <a href="/#/">
        <img src="../assets/logo.png" />
      </a>
      <nav>
        <ul>
          <li><a href="#/">Home</a></li>
          <li><a>Registration</a></li>
          <li><a href="#/Login">Login</a></li>
          <li><a>Logout</a></li>
          <li><a href="#/RestaurantBillSplitter">Restaurant Bill Splitter</a></li>
        </ul>
      </nav>
    </div>

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
  </div>
</template>

<script>
export default {
    name: 'Login',
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
<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }

  .wrapper {
    margin: 10px;
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }
</style>
