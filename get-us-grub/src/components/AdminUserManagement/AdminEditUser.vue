<template>
  <div>
    <app-admin-header/>
    <!-- <v-container id="choose-your-user" fluid>
    </v-container> -->
    <v-layout>
      <!-- <v-flex xs5 sm3 offset-sm3> -->
      <v-flex xs15>
        <h1>Choose your user</h1>
        <v-btn v-on:click="viewUserRequirements" id ="user-button" color="info">User Account</v-btn>
        <v-btn v-on:click="viewRestaurantRequirements" id ="restaurant-button" color="info">Restaurant Profile</v-btn>
        <v-btn v-on:click="viewAdminRequirements" id ="admin-button" color="info">User Profile</v-btn>
      </v-flex>
    </v-layout>
      <v-flex sm2 offset-sm5>
        <h2>Input User To Edit</h2>
          <v-form v-model="validIdentificationInput">
            <v-text-field label="username" v-model="username" :rules="usernameRules" required ></v-text-field>
            <v-btn v-on:click="imageuploadvue" id ="imageupload-button" color="warning">Image Upload</v-btn>
          </v-form>
      </v-flex>
      <v-container fluid grid-list-md>
      <v-layout row wrap>
        <v-flex sm2 offset-sm5>
          <v-card-text>
            <h1> New User Information</h1>
            <!-- <app-user-validations/> -->
            <v-form v-model="validIdentificationInput">
              <v-text-field label="Enter new username" v-model="username" :rules="usernameRules" required></v-text-field>
              <v-text-field label="Enter new display name" v-model="displayName" :rules="displayNameRules" required></v-text-field>
              <v-text-field label="Enter new password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
            </v-form>
            <div v-if="check">
              <h1>Additional Restaurant requireinformation</h1>
              <!-- <app-restaurant-validations/> -->
              <v-form v-model="validAddBusinessHour">
                <v-select
                  :items="dayOfWeek"
                  v-model="businessHour.day"
                  label="Select a day"
                  single-line
                  auto
                  hide-details
                  :rules="businessDayRules"
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
              :rules="openTimeRules"
              readonly
              ></v-text-field>
              <v-time-picker format="24hr" v-model="businessHour.openTime" @change="$refs.openMenu.save(time)"></v-time-picker>
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
                :rules="closeTimeRules"
                readonly
              ></v-text-field>
              <v-time-picker format="24hr" v-model="businessHour.closeTime" @change="$refs.closeMenu.save(time)"></v-time-picker>
              </v-menu>
            </v-form>
            <v-form v-model="validContactInput">
              <v-flex xs4>
                <v-subheader>Enter the address of your restaurant</v-subheader>
              </v-flex>
              <v-text-field
                label="Street 1"
                placeholder="1111 Snowy Rock Pl"
                v-model="restaurantProfile.address.street1"
                :rules="addressStreet1Rules"
              ></v-text-field>
              <v-text-field
                label="Street 2"
                placeholder="Unit 2"
                v-model="restaurantProfile.address.street2"
              ></v-text-field>
              <v-text-field
                label="City"
                placeholder="Long Beach"
                v-model="restaurantProfile.address.city"
              ></v-text-field>
              <v-select
                :items="states"
                v-model="restaurantProfile.address.state"
                label="Select a state"
                single-line
                auto
                append-icon="map"
                hide-details
              ></v-select>
              <v-text-field
                label="Zip"
                placeholder="92812"
                :rules="addressZipRules"
                type="number"
                v-model.number="restaurantProfile.address.zip"
              ></v-text-field>
              <v-flex xs4>
                <v-subheader>Enter a phone number</v-subheader>
              </v-flex>
              <v-flex xs12 sm5>
                <v-text-field
                  v-model="restaurantProfile.phoneNumber"
                  placeholder="(562)111-5555"
                  prepend-icon="phone"
                  :rules="phoneNumberRules"
                  single-line
                ></v-text-field>
                </v-flex>
              </v-form>
              <v-spacer/>
              <v-form v-model="validRestaurantDetailsInput">
                <v-flex xs12>
                  <v-select
                    :items="foodCategories"
                    item-text="category"
                    item-value="category"
                    v-model="restaurantProfile.details.category"
                    label="Select a food category"
                    single-line
                    auto
                    prepend-icon="restaurant"
                    hide-details
                    :rules="foodCategoryRules"
                  ></v-select>
                </v-flex>
                <v-flex xs12>
                  <v-select
                    :items="avgFoodPrices"
                    item-text="price"
                    item-value="id"
                    v-model="restaurantProfile.details.avgFoodPrice"
                    label="Select average food price"
                    single-line
                    auto
                    prepend-icon="money"
                    hide-details
                    :rules="avgFoodPriceRules"
                  ></v-select>
                </v-flex>
              </v-form>
              <v-spacer/>
            </div>
            <v-btn id ="submit-account-button" color="warning" v-on:click="submitUserAccount">Submit UserAccount</v-btn>
            <v-btn id ="submit-restaurant-button" color="warning" v-on:click="submitRestaurantProfile">Submit RestaurantProfile</v-btn>
            <v-btn id ="submit-profile-button" color="warning" v-on:click="submitUserProfile">Submit UserProfile</v-btn>
          </v-card-text>
        </v-flex>
      </v-layout>
    </v-container>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
import AppUserValidations from '@/components/AdminUserManagement/UserValidationNoRequiredFields'
import AppRestaurantValidations from '@/components/AdminUserManagement/RestaurantValidationsNoRequiredFields'
import axios from 'axios'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-user-text-box': AppUserTextBox,
    'app-user-validations': AppUserValidations,
    'app-restaurant-validations': AppRestaurantValidations
  },
  data: () => ({
    check: false,
    validIdentificationInput: false,
    validAddBusinessHour: false,
    validRestaurantDetailsInput: false,
    userAccount: {
      username: '',
      displayname: '',
      password: '',
      street1: '',
      street2: '',
      city: '',
      zip: '',
      phone: '',
      businesshours: ''
    },
    usernameRules: [
      username => !!username || 'Username is required',
      username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
    ],
    restaurantProfile: {
      address: {
        street1: '',
        street2: '',
        city: '',
        state: '',
        zip: null
      },
      phoneNumber: '',
      businessHours: []
    },
    businessHour: {
      day: '',
      openTime: null,
      closeTime: null
    },
    addressStreet1Rules: [
      street1 => !!street1 || 'Street 1 is required'
    ],
    addressZipRules: [
      zip => !!zip || 'Zip code is required',
      zip => /^\d{5}$/.test(zip) || 'Zip code must contain 5 numbers'
    ],
    phoneNumberRules: [
      phone => !!phone || 'Phone number is required',
      phone => /^\([2-9]\d{2}\)\d{3}-\d{4}$/.test(phone) || 'Phone number must be in (XXX)XXX-XXXX format and not start with 0 or 1'
    ],
    dayOfWeek: [
      'Sunday',
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday',
      'Saturday'
    ],
    states: [
      'CA'
    ],
    foodCategoryRules: [
      category => !!category || 'Food category is required'
    ],
    avgFoodPriceRules: [
      price => !!price || 'Food price is required'
    ],
    responseDataStatus: '',
    responseData: ''
  }),
  methods: {
    viewRestaurantRequirements: function () {
      this.check = true
    },
    viewUserRequirements: function () {
      this.check = false
    },
    viewAdminRequirements: function () {
      this.check = false
    },
    checkbox: function () {
      this.checkbox = !this.checkbox
    },
    submitUserAccount () {
      axios.put('http://localhost:8081/User/Admin/EditUser', {
        editUser: this.userAccount
      }).then(response => {
        this.responseDataStatus = 'Success! User has been edited: '
        this.responseData = response.data
        console.log(response)
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
    }
  }
}
</script>
