<template>
    <div id="home">
      <app-header/>
      <img src="@/assets/GetUsGrub.png">
      <p>General Error</p>
      <app-footer/>
    </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'

export default {
  name: 'Home',
  components: {
    AppHeader,
    AppFooter
  }
}
</script>
