<template>
  <div id="business-hours-div">
    <div>
    <v-layout row justify-center>
    <v-dialog v-model="dialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formTitle }}</span>
        </v-card-title>
        <v-card-text>
            <v-layout wrap>
              <v-form v-model="valid">
              <v-flex xs12>
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
              ></v-select>
              </v-flex>
              <v-flex xs12>
              <v-select
                :items="$store.state.constants.dayOfWeek"
                v-model="editedBusinessHour.day"
                label="Select a day"
                single-line
                auto
                hide-details
                :rules="$store.state.rules.businessDayRules"
                required
              ></v-select>
              </v-flex>
              <v-flex xs12>
              <v-menu
                ref="menu"
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
                  v-model="editedBusinessHour.openTime"
                  prepend-icon="access_time"
                  readonly
                  required
                ></v-text-field>
                <v-time-picker
                  format="24hr"
                  v-model="editedBusinessHour.openTime"
                  :max="editedBusinessHour.closeTime"
                >
                </v-time-picker>
              </v-menu>
              </v-flex>
              <v-flex xs12>
              <v-menu
                ref="menu"
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
                  v-model="editedBusinessHour.closeTime"
                  prepend-icon="access_time"
                  readonly
                  required
                ></v-text-field>
                <v-time-picker
                  format="24hr"
                  scrollable
                  v-model="editedBusinessHour.closeTime"
                  :min="editedBusinessHour.openTime"
                >
              </v-time-picker>
              </v-menu>
              </v-flex>
              </v-form>
            </v-layout>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
          <v-btn color="blue darken-1" flat @click.native="save" :disabled="!valid">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </v-layout>
    </div>
      <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-toolbar color="pink" dark>
            <v-spacer/>
            <v-toolbar-title>Business Hours</v-toolbar-title>
            <v-spacer/>
            <v-btn dark class="mb-2" icon @click="addBusinessHour()" v-if="isEdit">
            <v-icon>add_circle</v-icon>
          </v-btn>
          </v-toolbar>
          <v-list two-line>
            <template v-for="(businessHour, index) in businessHours" v-if="businessHour.flag !== 3">
              <v-list-tile
                avatar
                ripple
                @click="toggle()"
                :key="index"
              >
                <v-list-tile-content>
                  <v-list-tile-title>{{ businessHour.day }}</v-list-tile-title>
                  <v-list-tile-sub-title class="text--primary">
                    {{ convertUtcToLocalTime(businessHour.openTime, 'h:mm a') }} - {{ convertUtcToLocalTime(businessHour.closeTime, 'h:mm a') }}
                    </v-list-tile-sub-title>
                </v-list-tile-content>
                <v-list-tile-action>
                  <div v-if="isEdit">
                <v-btn icon class="mx-0" @click="editBusinessHour(businessHour)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteBusinessHour(businessHour)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </div>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider
              v-if="index !== businessHours.length"
              :key="businessHour.id"
              >
              </v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import moment from 'moment'

export default {
  props: [
    'businessHours',
    'isEdit'
  ],
  data () {
    return {
      valid: false,
      editedIndex: -1,
      time: '',
      openTimeSync: false,
      closeTimeSync: false,
      timeZone: 'Pacific Standard Time',
      dialog: false,
      editedBusinessHour: {
        day: '',
        openTime: null,
        closeTime: null,
        flag: 0
      },
      defaultBusinessHour: {
        day: '',
        openTime: null,
        closeTime: null,
        flag: 0
      }
    }
  },
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New Business Hour' : 'Edit Business Hour'
    }
  },
  methods: {
    convertUtcToLocalTime (utcTime, format) {
      return moment(utcTime).format(format)
    },
    concatenateDateAndTime (dateTime, time) {
      var newDateTime = moment(moment(dateTime).format('YYYY-MM-DD') + ' ' + time)
      return newDateTime.format('YYYY-MM-DDTHH:mm:ss')
    },
    createDateTime (time) {
      var dateTime = moment('2018-05-17' + ' ' + time)
      return dateTime.format('YYYY-MM-DDTHH:mm:ss')
    },
    addBusinessHour () {
      this.editedIndex = -1
      this.dialog = true
    },
    editBusinessHour (businessHour) {
      this.editedIndex = this.businessHours.indexOf(businessHour)
      this.editedBusinessHour = Object.assign({}, businessHour)
      this.editedBusinessHour.openTime = this.convertUtcToLocalTime(businessHour.openTime, 'h:mm')
      this.editedBusinessHour.closeTime = this.convertUtcToLocalTime(businessHour.closeTime, 'h:mm')
      this.dialog = true
    },
    deleteBusinessHour (businessHour) {
      const index = this.businessHours.indexOf(businessHour)
      if (confirm('Are you sure you want to delete this business hour?')) {
        try {
          if (this.businessHours[index].flag === 1) {
            this.businessHours.splice(index, 1)
          } else {
            this.businessHours[index].flag = 3
          }
        } catch (ex) {
          this.businessHours[index].flag = 3
        }
      }
    },
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedBusinessHour = Object.assign({}, this.defaultBusinessHour)
        this.editedIndex = -1
      }, 300)
    },
    save () {
      if (this.editedIndex > -1) {
        if (this.businessHours[this.editedIndex].flag !== 1) {
          this.businessHours[this.editedIndex].flag = 2
        }
        this.businessHours[this.editedIndex].day = this.editedBusinessHour.day
        this.businessHours[this.editedIndex].openTime = this.concatenateDateAndTime(this.businessHours[this.editedIndex].openTime, this.editedBusinessHour.openTime)
        this.businessHours[this.editedIndex].closeTime = this.concatenateDateAndTime(this.businessHours[this.editedIndex].closeTime, this.editedBusinessHour.closeTime)
      } else {
        this.editedBusinessHour.flag = 1
        this.editedBusinessHour.openTime = this.createDateTime(this.editedBusinessHour.openTime)
        this.editedBusinessHour.closeTime = this.createDateTime(this.editedBusinessHour.closeTime)
        this.businessHours.push(this.editedBusinessHour)
      }
      this.close()
    },
    toggle () {}
  }
}
</script>

<style scoped>
</style>
