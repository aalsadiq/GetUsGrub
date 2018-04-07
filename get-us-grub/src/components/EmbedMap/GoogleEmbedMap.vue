<template>
  <div id="google-embed-map">
    <app-header />
    <div id="vue-frame">
      <!--iframe src="https://www.google.com/maps/embed/v1/directions?key=AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg&amp;origin=Oslo+Norway&amp;destination=Telemark+Norway"></iframe>-->
      <iframe width="600" height="450" frameborder="0" style="border:0" src="https://www.google.com/maps/embed/v1/directions?key=AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg&amp;origin=Oslo+Norway&amp;destination=Telemark+Norway" allowfullscreen></iframe>
      <br />
    </div>
    <app-footer />
  </div>
</template>

<script src="/dist/vue-frame.js"></script>
<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'GoogleEmbedMap',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  }
}
</script>

<!--API Key: AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg-->