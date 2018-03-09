<template>
  <div>
    <app-header/>
      <v-toolbar dark tabs flat>
        <v-tabs v-model="tabs" icons-and-text centered dark color="deep-orange darken-3">
          <v-spacer/>
          <v-tab href="#user">User
            <v-icon>face</v-icon>
          </v-tab>
          <v-spacer/>
          <v-tab href="#restaurant">Restaurant
            <v-icon>store</v-icon>
          </v-tab>
          <v-spacer/>
          <v-tabs-slider color="pink"></v-tabs-slider>
        </v-tabs>
      </v-toolbar>
      <v-tabs-items v-model="tabs">
        <v-tab-item v-for="content in ['user', 'restaurant']" :key="content" :id="content">
          <div v-if="content === 'user'">
            <v-stepper v-model="userStep" vertical>
              <v-stepper-header>
                <v-divider></v-divider>
                <v-stepper-step step="1" :complete="userStep > 1" editable>Identification</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="2" :complete="userStep > 2">Security Questions</v-stepper-step>
                <v-divider></v-divider>
              </v-stepper-header>
              <v-stepper-items>
                <v-stepper-content step="1">
                    <v-form v-model="validIdentificationInput">
                      <v-text-field
                        label="Enter a username"
                        v-model="username"
                        :rules="usernameRules"
                        required
                      ></v-text-field>
                      <v-text-field
                        label="Enter a display name"
                        v-model="displayName"
                        :rules="displayNameRules"
                        required
                      ></v-text-field>
                      <v-text-field
                        label="Enter a password"
                        v-model="password"
                        :rules="passwordRules"
                        :min="8"
                        :counter="64"
                        :append-icon="visibile ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (visibile = !visibile)"
                        :type=" visibile ? 'text' : 'password'"
                        required
                      ></v-text-field>
                    </v-form>
                  <v-btn color="primary" @click="userStep = 2" :disabled="!validIdentificationInput">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="2">
                  <v-form v-model="validSecurityInput">
                  <v-layout row wrap>
                    <v-flex xs12>
                      <v-select
                        :items="firstSecurityQuestionsSet"
                        v-model="firstSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                        label="Enter an answer to the above security question"
                        v-model="firstSecurityAnswer"
                        :rules="securityAnswerRules"
                        required
                      ></v-text-field>
                      <v-select
                        :items="secondSecurityQuestionsSet"
                        v-model="secondSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="secondSecurityAnswer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    <v-select
                        :items="thirdSecurityQuestionsSet"
                        v-model="thirdSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="thirdSecurityAnswer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    </v-flex>
                  </v-layout>
                  </v-form>
                  <v-btn color="grey lighten-5" @click="userStep = 1">Previous</v-btn>
                  <v-btn color="primary" @submit.prevent="userSubmit" :disabled="!validSecurityInput">Submit</v-btn>
                </v-stepper-content>
              </v-stepper-items>
            </v-stepper>
          </div>
          <div v-if="content === 'restaurant'">
            <v-stepper v-model="restaurantStep" vertical>
              <v-stepper-header>
                <v-divider></v-divider>
                <v-stepper-step step="1" :complete="restaurantStep > 1">Identification</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="2" :complete="restaurantStep > 2">Security Questions</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="3" :complete="restaurantStep > 3" editable>Business Hours</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="4" :complete="restaurantStep > 4">Contact Information</v-stepper-step>
                <v-divider></v-divider>
              </v-stepper-header>
              <v-stepper-items>
                <v-stepper-content step="1">
                    <v-form v-model="validIdentificationInput">
                      <v-text-field
                        label="Enter a username"
                        v-model="username"
                        :rules="usernameRules"
                        required
                      ></v-text-field>
                      <v-text-field
                        label="Enter a display name"
                        v-model="displayName"
                        :rules="displayNameRules"
                        required
                      ></v-text-field>
                      <v-text-field
                        label="Enter a password"
                        v-model="password"
                        :rules="passwordRules"
                        :min="8"
                        :counter="64"
                        :append-icon="visibile ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (visibile = !visibile)"
                        :type=" visibile ? 'text' : 'password'"
                        required
                      ></v-text-field>
                    </v-form>
                  <v-btn color="primary" @click="restaurantStep = 2" :disabled="!validIdentificationInput">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="2">
                  <v-form v-model="validSecurityInput">
                  <v-layout row wrap>
                    <v-flex xs12>
                      <v-select
                        :items="firstSecurityQuestionsSet"
                        v-model="firstSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                        label="Enter an answer to the above security question"
                        v-model="firstSecurityAnswer"
                        :rules="securityAnswerRules"
                        required
                      ></v-text-field>
                      <v-select
                        :items="secondSecurityQuestionsSet"
                        v-model="secondSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="secondSecurityAnswer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    <v-select
                        :items="thirdSecurityQuestionsSet"
                        v-model="thirdSecurityQuestion"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="thirdSecurityAnswer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    </v-flex>
                  </v-layout>
                  </v-form>
                  <v-btn color="grey lighten-5" @click="restaurantStep = 1">Previous</v-btn>
                  <v-btn color="primary" :disabled="!validSecurityInput" @click="restaurantStep = 3">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="3">
                  <v-layout row wrap>
                    <v-flex xs11 sm5>
                      <v-menu
                        ref="menu"
                        lazy
                        :close-on-content-click="false"
                        v-model="menu2"
                        transition="scale-transition"
                        offset-y
                        full-width
                        :nudge-right="40"
                        max-width="290px"
                        min-width="290px"
                        :return-value.sync="time"
                      >
                        <v-text-field
                          slot="activator"
                          label="Picker in menu"
                          v-model="time"
                          prepend-icon="access_time"
                          readonly
                        ></v-text-field>
                        <v-time-picker v-model="time" @change="$refs.menu.save(time)"></v-time-picker>
                      </v-menu>
                    </v-flex>
                    <v-spacer></v-spacer>
                    <v-flex xs11 sm5>
                      <v-dialog
                        ref="dialog"
                        persistent
                        v-model="modal2"
                        lazy
                        full-width
                        width="290px"
                        :return-value.sync="time"
                      >
                        <v-text-field
                          slot="activator"
                          label="Picker in dialog"
                          v-model="time"
                          prepend-icon="access_time"
                          readonly
                        ></v-text-field>
                        <v-time-picker v-model="time" actions>
                          <v-spacer></v-spacer>
                          <v-btn flat color="primary" @click="modal2 = false">Cancel</v-btn>
                          <v-btn flat color="primary" @click="$refs.dialog.save(time)">OK</v-btn>
                        </v-time-picker>
                      </v-dialog>
                    </v-flex>
                  </v-layout>
                  <v-btn color="grey lighten-5" @click="restaurantStep = 2">Previous</v-btn>
                  <v-btn color="primary" :disabled="!validSecurityInput" @click="restaurantStep = 4">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="4">
                  <v-btn color="grey lighten-5" @click="restaurantStep = 3">Previous</v-btn>
                  <v-btn color="primary" @submit.prevent="userSubmit" :disabled="!validSecurityInput">Submit</v-btn>
                </v-stepper-content>
              </v-stepper-items>
            </v-stepper>
          </div>
        </v-tab-item>
      </v-tabs-items>
    <app-footer/>
  </div>
</template>

<script>
import axios from 'axios'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'

export default {
  name: 'Registration',
  components: {AppHeader, AppFooter},
  data: () => ({
    tabs: null,
    userStep: 0,
    restaurantStep: 0,
    validIdentificationInput: false,
    validSecurityInput: false,
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
    ],
    firstSecurityQuestion: null,
    firstSecurityQuestionsSet: [
      'Who was the company you first worked for?',
      'Where did you go to highschool or college?',
      'What was the name of the teacher who gave you your first failing grade?'
    ],
    secondSecurityQuestion: null,
    secondSecurityQuestionsSet: [
      'What is your favorite song?',
      'What is your mother\'s maiden name?',
      'What is your favorite sports team?'
    ],
    thirdSecurityQuestion: null,
    thirdSecurityQuestionsSet: [
      'What was the name of your first crush?',
      'What is the name of your hometown?',
      'What was the name of your first pet?'
    ],
    firstSecurityAnswer: '',
    secondSecurityAnswer: '',
    thirdSecurityAnswer: '',
    securityAnswerRules: [
      v => !!v || 'Security answer is required'
    ]
  }),

  methods: {
    userSubmit () {
      if (this.$refs.form.validate()) {
        // Native form submission is not yet supported
        axios.post('localhost:8081/Registration/User', {
          name: this.username,
          password: this.password,
          select: this.select,
          checkbox: this.checkbox
        })
      }
    }
  }
}
</script>
