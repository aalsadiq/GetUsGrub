<template>
    <div id="home">
      <app-header/>
      <img src="@/assets/GetUsGrub.png">
      <app-footer/>
    </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'

export default {
  name: 'Home',
  components: {AppHeader, AppFooter}
}
</script>
