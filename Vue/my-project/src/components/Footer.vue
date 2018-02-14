<template>
  <footer>
    <p>{{ copyright }}</p>
  </footer>
</template>

<script>
  export default {
    data() {
      return {
        copyright: "Copyright \u00A9 All Rights Reserved. Team GitGrub."
      }
    }
  }
</script>

<style scoped>
  footer {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
</style>
