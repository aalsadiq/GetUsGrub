<template>
  <div>
    <v-form v-model="validContactInput">
      <v-subheader>Enter the address of your restaurant</v-subheader>
            <v-text-field
              label="Street 1"
              placeholder="1111 Snowy Rock Pl"
              v-model="restaurantProfile.address.street1"
              :rules="addressStreet1Rules"
              required
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
              required
            ></v-text-field>
            <v-select
              :items="states"
              v-model="restaurantProfile.address.state"
              label="Select a state"
              single-line
              auto
              append-icon="map"
              hide-details
              required
            ></v-select>
            <v-text-field
              label="Zip"
              placeholder="92812"
              :rules="addressZipRules"
              type="number"
              v-model.number="restaurantProfile.address.zip"
              required
            ></v-text-field>
            <v-subheader>Enter a phone number</v-subheader>
            <v-text-field
              v-model="restaurantProfile.phoneNumber"
              placeholder="(562)111-5555"
              prepend-icon="phone"
              :rules="phoneNumberRules"
              single-line
            ></v-text-field>
      </v-form>
      <v-form v-model="validAddBusinessHour">
         <v-subheader>Enter BusinessHours</v-subheader>
      </v-form>
    <br/>
  </div>
</template>

<script>
export default {
  name: 'RestaurantValidation',
  components: {},
  data: () => ({
    validBusinessHourInput: false,
    validAddBusinessHour: false,
    validContactInput: false,
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
    addressStreet1Rules: [
      v => !!v || 'Street 1 is required'
    ],
    addressZipRules: [
      v => !!v || 'Zip code is required',
      v => /^\d{5}$/.test(v) || 'Zip code must contain 5 numbers'
    ],
    phoneNumberRules: [
      v => !!v || 'Phone number is required',
      v => /^\([2-9]\d{2}\)\d{3}-\d{4}$/.test(v) || 'Phone number must be in (XXX)XXX-XXXX format and not start with 0 or 1'
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
    responseDataStatus: '',
    responseData: ''
  }),

  methods: {
    add () {
      this.restaurantProfile.businessHours.push({day: this.businessHour.day, openTime: this.businessHour.openTime, closeTime: this.businessHour.closeTime})
      this.businessHour.day = ''
      this.businessHour.openTime = null
      this.businessHour.closeTime = null
      this.counter = this.counter + 1
      if (this.counter > 0) {
        this.validBusinessHourInput = true
      }
    }
  }
}
</script>
