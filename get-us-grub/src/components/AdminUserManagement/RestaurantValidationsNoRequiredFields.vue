<template>
  <div>
      <v-form v-model="validAddBusinessHour">
        <v-select
          :items="dayOfWeek"
          v-model="businessHour.day"
          label="Select a day"
          single-line
          auto
          hide-details
          :rules="businessDayRules"
          required
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
      required
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
        required
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
    <br/>
  </div>
</template>

<script>
export default {
  name: 'RestaurantValidation',
  components: {},
  data: () => ({
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
