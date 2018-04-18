<template>
  <div>
    <v-form v-model="validIdentificationInput">
      <v-text-field
        label="Username"
        :value="$store.state.getters.getUsername"
        :rules="$store.state.rules.usernameRules"
        required
        :disabled=true
        ></v-text-field>
      <v-text-field
        label="Enter a password"
        v-model="userAccount.password"
        :rules="$store.state.rules.passwordRules"
        :min="8"
        :counter="64"
        :append-icon="visible ? 'visibility' : 'visibility_off'"
        :append-icon-cb="() => (visibile = !visible)"
        :type=" visible ? 'text' : 'password'"
        :error-messages="passwordErrorMessages"
        @input="validatePassword"
        required
        :disabled=false
        ></v-text-field>
    </v-form>
  </div>
</template>
<script>
export default {
  name: 'SSOAuthenticationForm',
  components: {
  },
  props: [
    'userAccount'
  ]
}
</script>
