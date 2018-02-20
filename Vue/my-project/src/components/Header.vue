<template>
  <header>
    <router-link to="/" exact>
      <img src="src\assets\logo.png" />
    </router-link>
    <nav>
      <ul>
        <li><router-link to="/" exact>Home</router-link></li>
        <li><router-link to="/Registration" exact>Registration</router-link></li>
        <li><router-link to="/Login" exact>Login</router-link></li>
        <li><router-link to="/Logout" exact>Logout</router-link></li>
        <li><router-link to="/RestaurantBillSplitter" exact>Split Your Bill!</router-link></li>
      </ul>
    </nav>
  </header>
</template>

<script lang="ts">
  export default {
    data() {
      return {

      }
    }
  }
</script>

<style scoped>
  header {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

    header > a > img {
      max-height: 150px;
      margin-bottom: 20px;
    }

  ul {
    list-style-type: none;
  }

  li {
    padding: 10px;
    display: inline-block;
    background-color: #000000;
    font-weight: bold;
  }

  li > a {
    color: #FFFFFF;
  }

  li > a:link || a:visited {
    color: #FFFFFF;
  }

  li > a:hover {
    color: #808080;
    text-decoration: none;
  }

  .router-link-active {
    background: #ffffff;
    color: #000000;
  }
</style>
