<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <app-header/>
      <v-container id="profile">
        <template v-if="profileType === 'individual'">
          <user-profile/>
        </template>
        <template v-if="profileType === 'restaurant'">
          <restaurant-profile/>
        </template>
      </v-container>
      <app-footer/>
    </v-container>
  </div>
</template>

<script>
import UserProfile from './User/UserProfile'
import RestaurantProfile from './Restaurant/RestaurantProfile'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import jwt from 'jsonwebtoken'
export default {
  name: 'Profile',
  components: {
    AppHeader,
    AppFooter,
    UserProfile,
    RestaurantProfile
  },
  data () {
    return {
      profileType: 'restaurant'
    }
  },
  beforeCreate () {
    if (jwt.decode(this.$store.state.authenticationToken).ReadIndividual === 'True') {
      this.profileType = 'individual'
    } else if (jwt.decode(this.$store.state.authenticationToken).ReadIndividual === 'True') {
      this.profileType = 'individual'
    } else {
      this.$router.push({ path: '/Forbidden' })
    }
    /*
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({ path: '/Unauthorized' })
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadIndividual === 'True') {
      } else {
        this.$router.push({ path: '/Forbidden' })
      }
    } catch (ex) {
      this.$router.push({ path: '/Forbidden' })
    }
    */
  },
  created () {
  }
}
</script>

<style scoped> {

}
</style>
