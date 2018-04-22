<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <app-header/>
      <v-container id="profile">
        <div v-if="profileType === 'user'">
          <user-profile/>
        </div>
        <div v-if="profileType === 'restaurant'">
          <restaurant-profile/>
        </div>
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
      profileType: null
    }
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({ path: '/Unauthorized' })
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUserProfile === 'True') {
      } else if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True') {
        console.log('this is a restaurantprofile')
      } else {
        this.$router.push({ path: '/Forbidden' })
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  },
  created () {
    if (jwt.decode(this.$store.state.authenticationToken).ReadUserProfile === 'True') {
      this.profileType = 'user'
      console.log('this is a user profile')
      console.log(this.profileType)
    } else if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True') {
      this.profileType = 'restaurant'
      console.log('this is a restaurantprofile')
      console.log(this.profileType)
    } else {
      this.$router.push({ path: '/Forbidden' })
    }
  }
}
</script>

<style scoped>
</style>
