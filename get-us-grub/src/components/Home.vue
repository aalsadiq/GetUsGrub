<template>
    <div id="home">
      <app-header/>
      <div v-if="showUnauthenticated()">
        <restaurant-selection-unregistered-user-main/>
      </div>
      <div v-if="!showUnauthenticated() && !showRegisteredRestaurantSelection()">
        <profile/>
      </div>
      <div v-if="showRegisteredRestaurantSelection()">
        <restaurant-selection-registered-user-main/>
      </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import RestaurantSelectionUnregisteredUserMain from '@/components/RestaurantSelection/UnregisteredUser/Main.vue'
import RestaurantSelectionRegisteredUserMain from '@/components/RestaurantSelection/RegisteredUser/Main.vue'
import jwt from 'jsonwebtoken'
import Profile from '@/components/Profile/Profile.vue'

export default {
  components: {
    AppHeader,
    RestaurantSelectionUnregisteredUserMain,
    RestaurantSelectionRegisteredUserMain,
    Profile,
    AppFooter
  },
  beforeCreate () {
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
        this.$router.push({ path: '/User/Admin' })
      }
    } catch (ex) {}
  },
  methods: {
    showRegisteredRestaurantSelection () {
      try {
        if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
          return true
        }
      } catch (ex) {
        return false
      }
    },
    showUnauthenticated () {
      if (this.$store.state.authenticationToken === null) {
        return true
      } else {
        return false
      }
    }
  }
}
</script>

<style scoped>
</style>
