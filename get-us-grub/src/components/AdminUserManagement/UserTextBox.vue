<template>
    <div>
      {{ responseData }}
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
      <!-- <v-alert id="user-text-box-alert" icon="new_releases" class="text-xs-center" :value=showAlert>
        {{this.response}}
      </v-alert> -->
    </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'UserTextBox',
  props: ['viewType'],
  data: () => ({
    validIdentificationInput: false,
    // showAlert: false,
    userAccount: {
      username: '',
      password: ''
    },
    responseDataStatus: '',
    responseData: ''
  }),
  methods: {
    userSubmit: function (viewType) {
      if (viewType === 'DeactivateUser') {
        axios.put('http://localhost:8081/User/DeactivateUser', {
          username: this.userAccount.username
        }).then(response => {
          // showAlert = true
          this.responseDataStatus = 'Success! User has been deactivated: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
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
        axios.put('http://localhost:8081/User/ReactivateUser', {
          username: this.userAccount.username
        }).then(response => {
          this.responseDataStatus = 'Success! User has been reactivated: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
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
        axios.delete('http://localhost:8081/User/DeleteUser', {
          data: {username: this.userAccount.username}
        }).then(response => {
          this.responseDataStatus = 'Success! User has been deleted: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
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
