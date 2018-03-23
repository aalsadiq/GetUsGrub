<template>
    <div id="app">
        <v-container grid-list-md text-xs-center>
        <v-layout row wrap>
                <v-form v-model="validIdentificationInput">
                    <v-text-field label="Enter new username" v-model="username" :rules="usernameRules" ></v-text-field>
                    <v-text-field label="Enter new display name" v-model="displayName" ></v-text-field>
                    <v-text-field label="Enter new password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
                </v-form>
        </v-layout>
        </v-container>
    </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'UserValidation',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    validIdentificationInput: false,
    username: '',
    displayName: '',
    password: '',
    usernameRules: [
      username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
    ],
    passwordRules: [
      password => password.length >= 8 || 'Password must be at least 8 characters',
      password => password.length < 64 || 'Password must be at most 64 characters'
    ]
  })
}
</script>
