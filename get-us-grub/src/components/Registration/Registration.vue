<template>
  <div>
    <app-header/>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="registration">
        <app-create-user/>
      </v-container>
    </v-container>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import AppCreateUser from './CreateUser'

export default {
  name: 'AdminHome',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'app-create-user': AppCreateUser
  },
  beforeCreate () {
    try {
      if (this.$store.state.authenticationToken === null) {
      } else {
        this.$router.push({path: '/'})
      }
    } catch (ex) {
      this.$router.push({path: '/'})
    }
  }
}
</script>

<style>
#scroll-target {
  max-height: 50.5em;
  margin: -0.8em 1em 0em 1em;
}
#registration {
  max-width: 1200px;
  padding-top: -2em;
  margin: -2em 0em 0em 7%;
}
</style>
