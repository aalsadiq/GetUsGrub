<template>
  <v-form v-model="value.isValidContactInfo" v-on:input="$emit('input', value)">
    <v-subheader>Enter the address of your restaurant</v-subheader>
      <v-text-field
        label="Street 1"
        placeholder="1111 Snowy Rock Pl"
        v-model="value.restaurantProfile.address.street1"
        :rules="$store.state.addressStreet1Rules"
        required
        :disabled="disabled"
        ></v-text-field>
      <v-text-field
        label="Street 2"
        placeholder="Unit 2"
        v-model="value.restaurantProfile.address.street2"
        :disabled="disabled"
        ></v-text-field>
      <v-text-field
        label="City"
        placeholder="Long Beach"
        v-model="value.restaurantProfile.address.city"
        :rules="$store.state.rules.addressCityRules"
        required
        :disabled="disabled"
        ></v-text-field>
      <v-select
        :items="$store.state.constants.states"
        v-model="value.restaurantProfile.address.state"
        item-text="name"
        item-value="abbreviation"
        label="Select a state"
        :rules="$store.state.rules.addressStateRules"
        single-line
        auto
        append-icon="map"
        hide-details
        required
        :disabled="disabled"
        ></v-select>
      <v-text-field
        label="Zip"
        placeholder="92812"
        :rules="$store.state.rules.addressZipRules"
        type="number"
        v-model.number="value.restaurantProfile.address.zip"
        required
        :disabled="disabled"
        ></v-text-field>
    <v-subheader>Enter a phone number</v-subheader>
    <v-text-field
      v-model="value.restaurantProfile.phoneNumber"
      placeholder="(562)111-5555"
      prepend-icon="phone"
      :rules="$store.state.rules.phoneNumberRules"
      single-line
      :disabled="disabled"
      ></v-text-field>
  </v-form>
</template>

<script>
export default {
  name: 'ContactInfoForm',
  components: {},
  props: [
    'value',
    'disabled'
  ]
}
</script>
