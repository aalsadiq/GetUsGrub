<template>
    <div>
      <v-app id="inputUser">
          <v-container fluid>
                <v-flex xs6 sm3 offset-sm3>
                  <v-subheader>Input User Name</v-subheader>
                </v-flex>
                <v-divider></v-divider>
                <v-spacer/>
                <v-flex xs6 sm3 offset-sm3>
                   <v-form v-model="validIdentificationInput">
                     <v-text-field label="username" v-model="username" :rules="usernameRules" required >
                     </v-text-field>
                   </v-form>
                    <v-btn color="primary" @submit.prevent="userSubmit" :disabled="!validSecurityInput">Submit</v-btn>
                </v-flex>
          </v-container>
        </v-app>
    </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'UserTextBox',
  data: () => ({
    validIdentificationInput: false,
    username: '',
    usernameRules: [
      v => !!v || 'Username is required',
      v => /^[A-Za-z\d]+$/.test(v) || 'Username must contain only letters and numbers',
      v => /^[8,]+$/ || 'Username must be greater than 8 characters'
    ]
  }),
  methods: {
    userSubmit () {
      if (this.$refs.form.validate()) {
        // Native form submission is not yet supported
        axios.post('/User/DeactivateUser', {
          username: this.username,
          headers: {
            'Content-type': 'application/json'
          }
        })
      }
    }
  }
}
</script>
