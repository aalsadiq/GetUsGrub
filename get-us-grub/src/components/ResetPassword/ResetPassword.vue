<template>
  <div>
    <app-header/>
    <div id="reset-password-div">
      <div id="results-div">
        <div id="success">
        <v-layout align-center justify-center>
          <v-flex xs12 sm10 md6>
            <!-- Success messages for registration -->
            <v-alert type="success" :value="showSuccess">
              <span>
                Success! Your password has been has been resetted.
              </span>
            </v-alert>
          </v-flex>
        </v-layout>
      </div>
      <div v-if="showError" id="error-div">
        <v-layout align-center justify-center>
        <v-flex xs12 sm10 md6>
          <!-- Error messages for registration -->
          <v-alert id="registration-error" :value="true" icon='warning'>
            <span id="error-title">
              An error has occurred
            </span>
          </v-alert>
        </v-flex>
        </v-layout>
        <v-layout align-center justify-center>
          <!-- Card to show error messages -->
          <v-flex xs12 sm10 md6>
            <v-card id="error-card">
              <p v-for="error in errors" :key="error">
                {{ error }}
              </p>
            </v-card>
          </v-flex>
        </v-layout>
      </div>
    </div>
      <v-layout align-center justify-center>
        <v-flex xs12 sm10 md6>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Reset Password</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form v-model="isValid">
                  <v-text-field
                    prepend-icon="person"
                    v-model="username"
                    label="Enter username"
                    type="text"
                    :disabled="isDisabled"
                    :rules="$store.state.rules.usernameRules"
                    required
                  >
                  </v-text-field>
                <div v-if="isSecurityQuestionsForm">
                  <div v-for="(securityQuestion, index) in securityQuestions" :key="index">
                    <!-- <v-text-field
                      prepend-icon="https"
                      type="text"
                      v-model="securityQuestion.question"
                      :disabled="isDisabled"
                      :rules="$store.state.rules.securityQuestionRules"
                      required>
                    </v-text-field> -->
                    <v-select
                      :items="$store.state.constants.securityQuestion"
                      item-text="question"
                      item-value="id"
                      v-model="securityQuestions[index].question"
                      label="Select a security question"
                      single-line
                      auto
                      append-icon="https"
                      hide-details
                      :rules="$store.state.rules.securityQuestionRules"
                      required
                      :disabled=disable
                    ></v-select>
                    <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="securityQuestions[index].answer"
                      :rules="$store.state.rules.securityAnswerRules"
                      required
                      :disabled="isSubmitDisabled">
                      </v-text-field>
                  </div>
                </div>
                <div v-if="isConfirmPasswordForm">
                  <v-text-field
                    label="Enter a password"
                    v-model="password"
                    :rules="$store.state.rules.passwordRules"
                    :min="8"
                    :counter="64"
                    :append-icon="visible ? 'visibility' : 'visibility_off'"
                    :append-icon-cb="() => (visible = !visible)"
                    :type=" visible ? 'text' : 'password'"
                    :error-messages="passwordErrorMessages"
                    @input="validatePassword"
                    required
                    :disabled="isSubmitDisabled"
                  ></v-text-field>
                </div>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <div v-if="isUsernameForm">
                <v-btn @click="getSecurityQuestions" color="primary" :disabled="!isValid">Submit</v-btn>
              </div>
              <div v-if="isConfirmPasswordForm">
                <v-btn color="primary" :disabled="!isPasswordValid || !isValid">Submit</v-btn>
              </div>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
import PasswordValidation from '@/components/PasswordValidation/PasswordValidation'

export default {
  // Adding Vue components dependencies
  components: {
    AppHeader,
    AppFooter
  },
  data () {
    return {
      showError: false,
      showSuccess: false,
      visible: false,
      isPasswordValid: false,
      isValid: false,
      isDisabled: false,
      isSubmitDisabled: false,
      isUsernameForm: true,
      isSecurityQuestionsForm: false,
      isConfirmPasswordForm: false,
      username: '',
      securityQuestions: [],
      password: ''
    }
  },
  methods: {
    // Get user's security questions
    getSecurityQuestions () {
      this.isSubmitDisabled = true
      this.isDisabled = true
      axios.post(this.$store.state.urls.resetPassword.getSecurityQuestions, {
        username: this.username
      }).then(response => {
        console.log(response.data)
        this.isValid = true
        this.isUsernameForm = false
        this.isSubmitDisabled = false
        this.isSecurityQuestionsForm = true
        this.securityQuestions = response.data.securityQuestionDtos
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.isValid = true
        this.isDisabled = false
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
    },
    // Confirm user's security answers
    confirmSecurityAnswers () {
      this.isSubmitDisabled = true
      this.isDisabled = true
      axios.post(this.$store.state.urls.resetPassword.confirmSecurityAnswers, {
        username: this.username,
        securityQuestionDtos: this.securityQuestions
      }).then(response => {
        this.isValid = true
        this.isSubmitDisabled = false
        this.securityQuestions = response.data.securityQuestionDtos
      }).catch(error => {
        this.isSubmitDisabled = false
        this.showSuccess = false
        this.showError = true
        this.isValid = true
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
    },
    // Update the user's password
    updatePassword () {
      this.isSubmitDisabled = true
      this.disable = true
      axios.post(this.$store.state.urls.resetPassword.updatePassword, {
        username: this.username,
        securityQuestionDtos: this.securityQuestions,
        password: this.password
      }).then(response => {
        this.isValid = true
        this.isSubmitDisabled = false
        this.showSuccess = true
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.validSecurityInput = true
        this.disable = false
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
    },
    validatePassword () {
      if (this.userAccount.password.length < 8) {
        this.passwordErrorMessages = []
        return
      }
      PasswordValidation.methods.validate(this.userAccount.password)
        .then(response => {
          this.isPasswordValid = response.isValid
          this.passwordErrorMessages = response.message
          if (response === []) {
            this.isPasswordValid = true
          }
        })
    }
  }
}
</script>

<style scoped>
#reset-password-div {
  margin: 6em 0 0 0;
}
</style>