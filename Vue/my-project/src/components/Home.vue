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

    <p> {{name}} </p>

  </div>
</template>

<script>
  export default {
    name: 'Home',
    data() {
      return {
        msg: 'Welcome to GetUsGrub!'
      }
    },
    methods: {
      toLogin: function () {
        this.$router.push('Login')
      }
    }
  }
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }

  div#top > a > img {
    max-height: 150px;
    margin-bottom: 20px;
  }

  ul {
    padding: 0px;
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
</style>
