<template>
  <div id='firsttimeregistration'>
    <app-header/>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="registration">
        <create-user />
      </v-container>
    </v-container>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import CreateUser from './CreateUser'

export default {
  name: 'FirstTimeRegistration',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'create-user': CreateUser
  }
}
</script>
