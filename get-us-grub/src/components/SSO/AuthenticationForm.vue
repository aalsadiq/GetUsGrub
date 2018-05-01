<template>
  <v-form v-model="value.isValid" v-on:input="$emit('input', value)">
    <!-- USERNAME FIELD -->
    <v-text-field
      label="Username"
      :value='value.userAccount.username'
      required
      :disabled=true
    />
    <!-- PASSWORD FIELD -->
    <v-text-field
      label="Please enter your password"
      v-model="value.userAccount.password"
      :rules="$store.state.rules.passwordRules"
      :min="8"
      :counter="64"
      :append-icon="visible ? 'visibility' : 'visibility_off'"
      :append-icon-cb="() => (visible = !visible)"
      :type=" visible ? 'text' : 'password'"
      :disabled="disabled"
      required
    />
  </v-form>
</template>
<script>
export default {
  name: 'SSOAuthenticationForm',
  components: {
  },
  data: () => ({
    validIdentificationInput: false,
    visible: false
  }),
  props: [
    'value',
    'disabled'
  ],
  // Set the username to the field in the store
  created () {
    this.value.userAccount.username = this.$store.state.username
    this.$emit('input', this.value)
  }
}
</script>
