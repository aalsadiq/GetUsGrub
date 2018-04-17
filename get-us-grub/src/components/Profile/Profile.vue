<template>
  <div id='profile'>
    <app-header/>
    <profile-header :displayName='displayName'/>
    <app-footer/>
  </div>
</template>

<script>
import axios from 'axios'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import ProfileHeader from '@/components/Profile/ProfileHeader'
export default {
  name: 'Profile',
  components: {
    AppHeader,
    ProfileHeader,
    AppFooter
  },
  data () {
    return {
      username: '',
      displayName: null
    }
  },
  created () {
    // retrieve claim to check if they can view a user profile or a restaurant profile
    // if the claim is view user profile
    // make the username the store's username
    this.username = this.$store.state.username
    axios
      .get('http://localhost:8081/Profile/User', {
        headers: {
          'Access-Control-Allow-Origin': '*'
        },
        params: {
          username: this.username
        }
      })
      .then(response => {
        console.log(response)
        this.displayName = response.data.displayName
      })
      .catch(error => {
        console.log('An error has occurred')
        throw error
      })
  },
  methods: {
    GetProfile: function () {
      // retrieve claim to check if they can view a user profile or a restaurant profile
      // if the claim is view user profile
      // make the username the store's username
      this.username = this.$store.state.username
      axios
        .get('http://localhost:8081/Profile/User', {
          params: {
            username: this.username
          }
        })
        .then(response => {
          console.log(response)
          this.profile = response
        })
        .catch(error => {
          console.log('An error has occurred')
          throw error
        })
    }
  }
}
</script>

<style scoped>

</style>
