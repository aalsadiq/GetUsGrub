<template>
  <v-form v-model="value.isValid" v-on:input="$emit('input', value)">
    <v-text-field
      label="Username"
      :value='value.userAccount.username'
      required
      :disabled=true
    />
    <v-text-field
      label="Please enter your password"
      v-model="value.userAccount.password"
      :rules="$store.state.rules.passwordRules"
      :min="8"
      :counter="64"
      :append-icon="visible ? 'visibility' : 'visibility_off'"
      :append-icon-cb="() => (visible = !visible)"
      :type=" visible ? 'text' : 'password'"
      required
      v-on:input="$emit('input', value)"
    />
  </v-form>
</template>
<script>
import jwt from 'jsonwebtoken'

export default {
  name: 'SSOAuthenticationForm',
  components: {
  },
  data: () => ({
    validIdentificationInput: false,
    visible: false
  }),
  props: [
    'value'
  ],
  created () {
    var username = jwt.decode(this.$store.state.firstTimeUserToken).Username
    console.log(username)
    console.log(jwt)
    this.value.userAccount.username = username
    this.$emit('input', this.value)
  },
  beforeCreate () {
    if (this.$store.state.firstTimeUserToken === null) {
      this.$router.push({path: '/Unauthorized'})
    }
  }
}
</script>
