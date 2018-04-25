<template>
    <div>
      <div id="success">
        <v-layout>
          <v-flex xs12>
            <v-alert type="success" :value="showSuccess">
            <span>
               {{ responseData }}
            </span>
            </v-alert>
          </v-flex>
        </v-layout>
      </div>
      <div v-show="showError" id="error-div">
        <v-layout>
        <v-flex xs12>
          <!-- Title bar for the restaurant selection -->
          <v-alert id="registration-error" :value=true icon='warning'>
            <span id="error-title">
              An error has occurred
            </span>
          </v-alert>
        </v-flex>
        </v-layout>
        <v-layout>
          <v-flex xs12>
            <v-card id="error-card">
              <p v-for="error in errors" :key="error">
                {{ error }}
              </p>
            </v-card>
          </v-flex>
        </v-layout>
      </div>
      <div id="inputUser">
          <div fluid>
            <v-spacer/>
              <v-flex  xs5 sm2 offset-sm5>
                <h2>Input User Name </h2>
                <v-form v-model="validIdentificationInput">
                  <v-text-field label="username" v-model="userAccount.username"  :rules="$store.state.rules.usernameRules" required />
                </v-form>
              </v-flex>
              <v-btn id ="submit-button" color="warning" v-on:click="userSubmit(viewType)">Submit</v-btn>
          </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'UserTextBox',
  props: ['viewType'],
  data: () => ({
    validIdentificationInput: false,
    userAccount: {
      username: '',
      password: ''
    },
    responseData: '',
    showError: false,
    showSuccess: false
  }),
  methods: {
    userSubmit: function (viewType) {
      if (viewType === 'DeactivateUser') {
        axios.put(this.$store.state.urls.userManagement.deactivateUser, {
          username: this.userAccount.username
        }).then(response => {
          this.responseData = response.data
          this.showSuccess = true
          this.showError = false
        }).catch(error => {
          this.responseData = error.response.data
          this.showSuccess = false
          this.showError = true
          try {
            if (error.response.status === 401) {
              // Route to Unauthorized page
              this.$router.push({path: '/Unauthorized'})
            }
            if (error.response.status === 403) {
              // Route to Forbidden page
              this.$router.push({path: '/Forbidden'})
            }
            if (error.response.status === 404) {
              // Route to ResourceNotFound page
              this.$router.push({path: '/ResourceNotFound'})
            }
            if (error.response.status === 500) {
              // Route to InternalServerError page
              this.$router.push({path: '/InternalServerError'})
            } else {
              this.errors = JSON.parse(JSON.parse(error.response.data.message))
            }
            Promise.reject(error)
          } catch (ex) {
            this.errors = error.response.data
            Promise.reject(error)
          }
        })
      }
      if (viewType === 'ReactivateUser') {
        axios.put(this.$store.state.urls.userManagement.reactivateUser, {
          username: this.userAccount.username
        }).then(response => {
          this.responseData = response.data
          this.showSuccess = true
          this.showError = false
        }).catch(error => {
          this.responseData = error.response.data
          this.showSuccess = false
          this.showError = true
          try {
            if (error.response.status === 401) {
              // Route to Unauthorized page
              this.$router.push({path: '/Unauthorized'})
            }
            if (error.response.status === 403) {
              // Route to Forbidden page
              this.$router.push({path: '/Forbidden'})
            }
            if (error.response.status === 404) {
              // Route to ResourceNotFound page
              this.$router.push({path: '/ResourceNotFound'})
            }
            if (error.response.status === 500) {
              // Route to InternalServerError page
              this.$router.push({path: '/InternalServerError'})
            } else {
              this.errors = JSON.parse(JSON.parse(error.response.data.message))
            }
            Promise.reject(error)
          } catch (ex) {
            this.errors = error.response.data
            Promise.reject(error)
          }
        })
      }
      if (viewType === 'DeleteUser') {
        axios.delete(this.$store.state.urls.userManagement.deleteUser, {
          data: {username: this.userAccount.username}
        }).then(response => {
          this.responseData = response.data
          this.showSuccess = true
          this.showError = false
          console.log(response)
        }).catch(error => {
          this.responseData = error.response.data
          this.showSuccess = false
          this.showError = true
          try {
            if (error.response.status === 401) {
              // Route to Unauthorized page
              this.$router.push({path: '/Unauthorized'})
            }
            if (error.response.status === 403) {
              // Route to Forbidden page
              this.$router.push({path: '/Forbidden'})
            }
            if (error.response.status === 404) {
              // Route to ResourceNotFound page
              this.$router.push({path: '/ResourceNotFound'})
            }
            if (error.response.status === 500) {
              // Route to InternalServerError page
              this.$router.push({path: '/InternalServerError'})
            } else {
              this.errors = JSON.parse(JSON.parse(error.response.data.message))
            }
            Promise.reject(error)
          } catch (ex) {
            this.errors = error.response.data
            Promise.reject(error)
          }
        })
      }
    }
  }
}
</script>

<style>
#create-user-div {
  padding: 2em 6em 0em 10em;
}
#card {
  padding: 0 0.7em 0 0.7em;
  margin: 0 0 1em 0;
}
#user-text-box-alert{
  background-color: #e26161 !important
}
</style>
