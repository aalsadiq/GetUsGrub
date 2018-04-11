<template>
    <div id="home">
      <app-header/>
      <img src="@/assets/GetUsGrub.png">
      <restaurant-selection-unregistered-user-main/>
      <app-footer/>
    </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import RestaurantSelectionUnregisteredUserMain from '@/components/RestaurantSelection/UnregisteredUser/Main.vue'

export default {
  name: 'Home',
  components: {
    AppHeader,
    RestaurantSelectionUnregisteredUserMain,
    AppFooter
  }
}
</script>
