<template>
  <div id="google-embed-map">
      <iframe :src="mapUrl" width = 100% height = 100%></iframe>
  </div>
</template>

<script>
export default {
  name: 'GoogleEmbedMap',
  components: {
  },
  computed: {
    mapUrl () {
      return this.$store.getters.mapUrl
    }
  }
}
</script>

<!--API Key: AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg-->
