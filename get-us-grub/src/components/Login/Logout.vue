<template>
<div>
    <h1> Logout </h1>
</div>
</template>

<script>
export default {
  mounted () {
    this.$router.push({ path: '/' })
  }
}
</script>
