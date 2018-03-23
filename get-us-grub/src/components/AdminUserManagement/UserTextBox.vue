<template>
    <div>
      {{ responseData }}
      <v-app id="inputUser">
          <v-container fluid>
            <v-spacer/>
              <v-flex  xs5 sm2 offset-sm5>
                <h2>Input User Name </h2>
                <v-form v-model="validIdentificationInput">
                  <v-text-field label="username" v-model="username" :rules="usernameRules" required />
                </v-form>
              </v-flex>
              <v-btn id ="submit-button" color="warning" v-on:click="userSubmit(viewType)">Submit</v-btn>
          </v-container>
        </v-app>
    </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'UserTextBox',
  props: ['viewType'],
  data: () => ({
    validIdentificationInput: false,
    username: '',
    usernameRules: [
      username => !!username || 'Username is required',
      username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
    ],
    responseData: ''
  }),
  methods: {
    userSubmit: function (viewType) {
      if (viewType === 'DeactivateUser') {
        console.log(this.username)
        axios.put('http://localhost:8081/User/DeactivateUser', {
          username: this.username
        }).then(response => {
          this.responseDataStatus = 'Success! User has been deactivated: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
      if (viewType === 'ReactivateUser') {
        axios.put('http://localhost:8081/User/ReactivateUser', {
          username: this.username
        }).then(response => {
          this.responseDataStatus = 'Success! User has been reactivated: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
        })
      }
      if (viewType === 'DeleteUser') {
        console.log(this.username)
        axios.delete('http://localhost:8081/User/DeleteUser', {
          username: this.username
        }).then(response => {
          this.responseDataStatus = 'Success! User has been deleted: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
        })
      }
    }
  }
}
</script>
