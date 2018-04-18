<template>
    <div id="home">
      <app-header/>
      <div v-show="show()">
        <restaurant-selection-unregistered-user-main/>
      </div>
      <div v-show="!show()">
        <img src="@/assets/GetUsGrub.png">
        <p>Welcome!</p>
      </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import RestaurantSelectionUnregisteredUserMain from '@/components/RestaurantSelection/UnregisteredUser/Main.vue'
import jwt from 'jsonwebtoken'

export default {
  name: 'Home',
  components: {
    AppHeader,
    RestaurantSelectionUnregisteredUserMain,
    AppFooter
  },
  beforeCreate () {
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
        this.$router.push({path: '/User/Admin'})
      }
    } catch (ex) {}
  },
  methods: {
    show () {
      if (this.$store.state.authenticationToken === null) {
        return true
      } else {
        return false
      }
    }
  }
}
</script>
