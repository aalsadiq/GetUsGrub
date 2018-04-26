<template>
  <div>
    <v-form v-model="value.isValid">
      <!-- TIMEZONE FIELD -->
      <v-select
        :items="$store.state.constants.timeZones"
        item-text="displayString"
        item-value="timeZoneName"
        v-model="value.timeZone"
        label="Select your time zone"
        single-line
        auto
        hide-details
        :rules="$store.state.rules.timeZoneRules"
        required
        :disabled="disabled"
        ></v-select>
      <!-- DAY FIELD -->
      <v-select
        :items="$store.state.constants.dayOfWeek"
        v-model="businessHour.day"
        label="Select a day"
        single-line
        auto
        hide-details
        :rules="$store.state.rules.businessDayRules"
        required
        :disabled="disabled"
        ></v-select>
      <!-- TIME PICKER OPENNING TIME MENU -->
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
        :disabled="disabled"
        >
        <!-- TIME PICKER OPENING TIME -->
        <v-text-field
          slot="activator"
          label="Select opening time (12hr format)"
          v-model="businessHour.openTime"
          prepend-icon="access_time"
          :rules="$store.state.rules.openTimeRules"
          readonly
          required
          :disabled="disabled"
          ></v-text-field>
        <v-time-picker
          format="12hr"
          v-model="businessHour.openTime"
          @change="$refs.openMenu.save(time)"
          :max="businessHour.closeTime"
          :disabled="disabled"
          >
        </v-time-picker>
      </v-menu>
      <!-- TIME PICKER CLOSING TIME MENU -->
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
        :disabled="disabled"
        >
        <!-- TIME PICKER CLOSING TIME -->
        <v-text-field
          slot="activator"
          label="Select closing time (24hr format)"
          v-model="businessHour.closeTime"
          prepend-icon="access_time"
          :rules="$store.state.rules.closeTimeRules"
          readonly
          required
          :disabled="disabled"
          ></v-text-field>
        <v-time-picker
          format="24hr"
          scrollable
          v-model="businessHour.closeTime"
          @change="$refs.closeMenu.save(time)"
          :min="businessHour.openTime"
          :disabled="disabled"
          >
        </v-time-picker>
      </v-menu>
    </v-form>
    <!-- ADD ENTRY BUTTON -->
    <v-btn @click.prevent="addBusinessHour" :disabled="!value.isValid || disabled">Add</v-btn>
    <ul class="list-group">
      <li v-for="storeHour in value.businessHours" :key="storeHour" class="list-group-item">
        {{ storeHour.day }}: {{ storeHour.openTime }} - {{ storeHour.closeTime }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'BusinessHoursForm',
  components: {},
  data: () => ({
    openMenu: false,
    closeMenu: false,
    openTimeSync: false,
    closeTimeSync: false,
    businessHour: {
      day: '',
      openTime: null,
      closeTime: null
    },
    time: '',
    counter: 0
  }),
  props: [
    'value',
    'disabled'
  ],
  methods: {
    addBusinessHour () {
      this.value.businessHours.push({day: this.businessHour.day, openTime: this.businessHour.openTime, closeTime: this.businessHour.closeTime})
      this.$emit('input', this.value)
      this.businessHour.day = ''
      this.businessHour.openTime = null
      this.businessHour.closeTime = null
      this.counter = this.counter + 1
      if (this.counter > 0) {
        this.value.isValid = true
      }
    }
  }
}
</script>
