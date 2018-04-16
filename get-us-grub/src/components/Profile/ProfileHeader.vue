<template>
  <div id="profile-header">
    <h1>{{displayName}}</h1>
  </div>
</template>

<script>

  export default {
    name: 'ProfileHeader',
    components: {
      
    },
    props: ['displayName'],
    data () {
      return {
      }
    }
  }
</script>

<style scoped>

</style>
