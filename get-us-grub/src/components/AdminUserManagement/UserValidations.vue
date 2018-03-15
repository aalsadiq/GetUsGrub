<template>
    <div id="app">
    <!-- <v-app id="inspire"> -->
        <v-container grid-list-md text-xs-center>
        <v-layout row wrap>
            <!-- <v-flex xs10 order-lg21> -->
                <v-form v-model="validIdentificationInput">
                    <v-text-field label="Enter new username" v-model="username" :rules="usernameRules" required></v-text-field>
                    <v-text-field label="Enter new display name" v-model="displayName" :rules="displayNameRules" required></v-text-field>
                    <v-text-field label="Enter new password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
                    <v-text-field label="Confirm password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
                </v-form>
            <!-- </v-flex> -->
        </v-layout>
        </v-container>
    <!-- </v-app> -->
    </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    tabs: null,
    userStep: 0,
    restaurantStep: 0,
    validIdentificationInput: false,
    visibile: false,
    username: '',
    usernameRules: [
      v => !!v || 'Username is required',
      v => /^[A-Za-z\d]+$/.test(v) || 'Username must contain only letters and numbers'
    ],
    displayName: '',
    displayNameRules: [
      v => !!v || 'Display name is required'
      // Should display name have spaces and special characters?
    ],
    password: '',
    passwordRules: [
      v => !!v || 'Password is required',
      v => v.length > 8 || 'Password must be at least 8 characters',
      v => v.length < 64 || 'Password must be at most 64 characters'
    ]
  })
}
</script>
