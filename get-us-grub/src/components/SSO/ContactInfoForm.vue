<template>
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
</template>

<script>
export default {
  name: 'ContactInfoForm',
  components: {},
  props: ['restaurantProfile']
}
</script>
