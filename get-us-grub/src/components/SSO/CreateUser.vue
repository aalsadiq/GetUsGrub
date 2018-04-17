<template>
  <div id="create-user">
    <v-layout id="response">
    <v-flex xs12>
      {{ responseDataStatus }} {{ responseData }}
    </v-flex>
    </v-layout>
    <v-layout>
    <v-flex xs12>
        <v-toolbar dark tabs flat>
          <v-tabs v-model="tabs" icons-and-text centered dark color="deep-orange accent-2">
            <v-spacer/>
            <v-tab href="#user">User
              <v-icon>face</v-icon>
            </v-tab>
            <v-spacer/>
            <v-tab href="#restaurant">Restaurant
              <v-icon>store</v-icon>
            </v-tab>
            <v-spacer/>
          </v-tabs>
        </v-toolbar>
        <v-tabs-items v-model="tabs">
          <v-tab-item v-for="content in ['user', 'restaurant']" :key="content" :id="content">
            <div v-if="content === 'user'">
              <v-stepper v-model="userStep" vertical>
                <v-stepper-header>
                  <v-divider></v-divider>
                  <v-stepper-step step="1" :complete="userStep > 1">Identification</v-stepper-step>
                  <v-divider></v-divider>
                  <v-stepper-step step="2" :complete="userStep > 2">Security Questions</v-stepper-step>
                  <v-divider></v-divider>
                </v-stepper-header>
                <v-stepper-items>
                    <auth-form />
                    <security-questions />
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
                  <v-stepper-step step="3" :complete="restaurantStep > 3">Restaurant Details</v-stepper-step>
                  <v-divider></v-divider>
                  <v-stepper-step step="4" :complete="restaurantStep > 4">Business Hours</v-stepper-step>
                  <v-divider></v-divider>
                  <v-stepper-step step="5" :complete="restaurantStep > 5">Contact Information</v-stepper-step>
                  <v-divider></v-divider>
                </v-stepper-header>
                <v-stepper-items>
                  <v-stepper-content step="1">
                      <v-form v-model="validIdentificationInput">
                        <v-text-field
                          label="Enter a username"
                          v-model="userAccount.username"
                          :rules="$store.state.rules.usernameRules"
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-text-field
                          label="Enter a display name"
                          v-model="userProfile.displayName"
                          :rules="$store.state.rules.displayNameRules"
                          required
                          :disabled=disable
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
                            :disabled=disable
                          ></v-text-field>
                      </v-form>
                    <v-btn color="primary" @click="restaurantStep = 2" :disabled="!isPasswordValid || !validIdentificationInput || disable">Next</v-btn>
                  </v-stepper-content>
                  <v-stepper-content step="2">
                    <v-form v-model="validSecurityInput">
                    <v-layout row wrap>
                      <v-flex xs12>
                        <v-select
                          :items="$store.state.constants.securityQuestionsSet1"
                          item-text="question"
                          item-value="id"
                          v-model="securityQuestions[0].question"
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
                          v-model="securityQuestions[0].answer"
                          :rules="$store.state.rules.securityAnswerRules"
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-select
                          :items="$store.state.constants.securityQuestionsSet2"
                          item-text="question"
                          item-value="id"
                          v-model="securityQuestions[1].question"
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
                          v-model="securityQuestions[1].answer"
                          :rules="$store.state.rules.securityAnswerRules"
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-select
                          :items="$store.state.constants.securityQuestionsSet3"
                          item-text="question"
                          item-value="id"
                          v-model="securityQuestions[2].question"
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
                        v-model="securityQuestions[2].answer"
                        :rules="$store.state.rules.securityAnswerRules"
                        required
                        :disabled=disable
                      ></v-text-field>
                      </v-flex>
                    </v-layout>
                    </v-form>
                    <v-btn color="grey lighten-5" @click="restaurantStep = 1" :disabled=disable>Previous</v-btn>
                    <v-btn color="primary" :disabled="!validSecurityInput || disable" @click="restaurantStep = 3">Next</v-btn>
                  </v-stepper-content>
                  <v-stepper-content step="3">
                    <v-form v-model="validRestaurantDetailsInput">
                      <v-flex xs12>
                        <v-select
                          :items="$store.state.constants.foodTypes"
                          item-text="type"
                          item-value="type"
                          v-model="restaurantProfile.details.foodType"
                          label="Select a food type associated with your restaurant"
                          single-line
                          auto
                          prepend-icon="restaurant"
                          hide-details
                          :rules="$store.state.rules.foodTypeRules"
                          required
                          :disabled=disable
                        ></v-select>
                      </v-flex>
                      <v-flex xs12>
                        <v-select
                          :items="$store.state.constants.avgFoodPrices"
                          item-text="price"
                          item-value="id"
                          v-model="restaurantProfile.details.avgFoodPrice"
                          label="Select average food price"
                          single-line
                          auto
                          prepend-icon="money"
                          hide-details
                          :rules="$store.state.rules.avgFoodPriceRules"
                          required
                          :disabled=disable
                        ></v-select>
                      </v-flex>
                      <v-flex xs12>
                        <v-select
                          label="Select a food preference"
                          :items="$store.state.constants.foodPreferences"
                          v-model="foodPreferences"
                          multiple
                          chips
                          prepend-icon="done"
                          persistent-hint
                          :rules="$store.state.rules.foodPreferenceRules"
                          required
                          :disabled=disable
                        ></v-select>
                      </v-flex>
                    </v-form>
                    <v-btn color="grey lighten-5" @click="restaurantStep = 2" :disabled=disable>Previous</v-btn>
                    <v-btn color="primary" :disabled="!validRestaurantDetailsInput || disable" @click="restaurantStep = 4">Next</v-btn>
                  </v-stepper-content>
                  <v-stepper-content step="4">
                    <v-form v-model="validAddBusinessHour">
                      <v-select
                        :items="$store.state.constants.timeZones"
                        item-text="displayString"
                        item-value="timeZoneName"
                        v-model="timeZone"
                        label="Select your time zone"
                        single-line
                        auto
                        hide-details
                        :rules="$store.state.rules.timeZoneRules"
                        required
                        :disabled=disable
                      ></v-select>
                      <v-select
                        :items="$store.state.constants.dayOfWeek"
                        v-model="businessHour.day"
                        label="Select a day"
                        single-line
                        auto
                        hide-details
                        :rules="$store.state.rules.businessDayRules"
                        required
                        :disabled=disable
                      ></v-select>
                      <v-menu
                        ref="openMenu"
                        lazy
                        :close-on-content-click="false"
                        v-model="openTimeSync"
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
                          label="Select opening time (24hr format)"
                          v-model="businessHour.openTime"
                          prepend-icon="access_time"
                          :rules="$store.state.rules.openTimeRules"
                          readonly
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-time-picker
                          format="24hr"
                          v-model="businessHour.openTime"
                          @change="$refs.openMenu.save(time)"
                          :max="businessHour.closeTime"
                        >
                        </v-time-picker>
                      </v-menu>
                      <v-menu
                        ref="closeMenu"
                        lazy
                        :close-on-content-click="false"
                        v-model="closeTimeSync"
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
                          label="Select closing time (24hr format)"
                          v-model="businessHour.closeTime"
                          prepend-icon="access_time"
                          :rules="$store.state.rules.closeTimeRules"
                          readonly
                          required
                          :disabled=disable
                        ></v-text-field>
                        <v-time-picker
                          format="24hr"
                          scrollable
                          v-model="businessHour.closeTime"
                          @change="$refs.closeMenu.save(time)"
                          :min="businessHour.openTime"
                        >
                        </v-time-picker>
                      </v-menu>
                    </v-form>
                    <v-btn @click.prevent="addBusinessHour" :disabled="!validAddBusinessHour || disable">Add</v-btn>
                    <ul class="list-group">
                        <li v-for="storeHour in businessHours" :key="storeHour" class="list-group-item">
                            {{ storeHour.day }}: {{ storeHour.openTime }} - {{ storeHour.closeTime }}
                        </li>
                    </ul>
                    <v-btn color="grey lighten-5" @click="restaurantStep = 3" :disabled=disable>Previous</v-btn>
                    <v-btn color="primary" :disabled="!validBusinessHourInput || disable" @click="restaurantStep = 5">Next</v-btn>
                  </v-stepper-content>
                  <v-stepper-content step="5">
                    <v-form v-model="validContactInput">
                      <v-flex xs12>
                        <v-subheader>Enter the address of your restaurant</v-subheader>
                      </v-flex>
                      <v-layout id="contact-layout" row justify-space-between>
                        <v-flex xs12>
                          <v-text-field
                            label="Street 1"
                            placeholder="1111 Snowy Rock Pl"
                            v-model="restaurantProfile.address.street1"
                            :rules="$store.state.rules.addressStreet1Rules"
                            required
                            :disabled=disable
                          ></v-text-field>
                          <v-text-field
                            label="Street 2"
                            placeholder="Unit 2"
                            v-model="restaurantProfile.address.street2"
                            :disabled=disable
                          ></v-text-field>
                          <v-text-field
                            label="City"
                            placeholder="Long Beach"
                            v-model="restaurantProfile.address.city"
                            :rules="$store.state.constants.addressCityRules"
                            required
                            :disabled=disable
                          ></v-text-field>
                          <v-select
                            :items="$store.state.constants.states"
                            v-model="restaurantProfile.address.state"
                            item-text="name"
                            item-value="abbreviation"
                            label="Select a state"
                            :rules="$store.state.rules.addressStateRules"
                            single-line
                            auto
                            append-icon="map"
                            hide-details
                            required
                            :disabled=disable
                          ></v-select>
                            <v-text-field
                            label="Zip"
                            placeholder="92812"
                            :rules="$store.state.rules.addressZipRules"
                            type="number"
                            v-model.number="restaurantProfile.address.zip"
                            required
                            :disabled=disable
                          ></v-text-field>
                        </v-flex>
                      </v-layout>
                      <v-flex xs4>
                        <v-subheader>Enter a phone number</v-subheader>
                      </v-flex>
                      <v-flex xs12 sm2>
                        <v-text-field
                          v-model="restaurantProfile.phoneNumber"
                          placeholder="(562)111-5555"
                          prepend-icon="phone"
                          :rules="$store.state.rules.phoneNumberRules"
                          single-line
                          :disabled=disable
                        ></v-text-field>
                      </v-flex>
                    </v-form>
                    <v-btn color="grey lighten-5" @click="restaurantStep = 4" :disabled=disable>Previous</v-btn>
                    <v-btn color="primary" @click="restaurantSubmit" :disabled="!validContactInput || disable" :loading="loading">Submit</v-btn>
                  </v-stepper-content>
                </v-stepper-items>
              </v-stepper>
            </div>
          </v-tab-item>
        </v-tabs-items>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import AuthorizationForm from './AuthorizationForm'
import SecurityQuestionsForm from './SecurityQuestions'
export default {
  name: 'SSOCreateUser',
  components: {
    'auth-form': AuthorizationForm,
    'security-questions': SecurityQuestionsForm
  }
}
</script>
