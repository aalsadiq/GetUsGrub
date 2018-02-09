<template>
  <div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: 'App'
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

div#top > a > img {
  max-height: 150px;
  margin-bottom: 20px;
}

ul {
  padding: 0px;
  list-style-type: none;
}

li{
  padding: 10px;
  display: inline-block;
  background-color: #000000;
  font-weight: bold;
}

li > a{
  color: #FFFFFF;
}

li > a:link || a:visited{
  color: #FFFFFF;
}

li > a:hover{
  color: #808080;
  text-decoration: none;
}

</style>
