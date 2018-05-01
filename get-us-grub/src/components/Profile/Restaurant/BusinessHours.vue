<template>
  <div id="business-hours-div">
  <div>
  <v-layout row justify-center>
    <!-- Dialog popup for business hours form -->
  <v-dialog v-model="dialog" persistent max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">{{ formTitle }}</span>
      </v-card-title>
      <v-card-text>
          <v-layout wrap>
            <v-flex>
            <v-form v-model="isValid">
              <v-select
                :items="$store.state.constants.timeZones"
                item-text="displayString"
                item-value="timeZoneName"
                v-model="editedBusinessHour.timeZone"
                label="Select your time zone"
                single-line
                auto
                hide-details
                :rules="$store.state.timeZoneRules"
                required
                :disabled=disable
              ></v-select>
              <v-select
                :items="$store.state.constants.dayOfWeek"
                v-model="editedBusinessHour.day"
                label="Select a day"
                single-line
                auto
                hide-details
                :rules="$store.state.businessDayRules"
                required
                :disabled=disable
              ></v-select>
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
                  :rules="$store.state.businessHourRules"
                  prepend-icon="access_time"
                  readonly
                  required
                  :disabled=disable
                ></v-text-field>
                <v-time-picker
                  format="24hr"
                  v-model="editedBusinessHour.openTime"
                  :max="editedBusinessHour.closeTime"
                >
                </v-time-picker>
              </v-menu>
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
                  :rules="$store.state.businessHourRules"
                  prepend-icon="access_time"
                  readonly
                  required
                  :disabled=disable
                ></v-text-field>
                <v-time-picker
                  format="24hr"
                  scrollable
                  v-model="editedBusinessHour.closeTime"
                  :min="editedBusinessHour.openTime"
                >
                </v-time-picker>
              </v-menu>
            </v-form>
            </v-flex>
          </v-layout>
      </v-card-text>
      <!-- Buttons to cancel or save form -->
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
        <v-btn color="blue darken-1" flat @click.native="save" :disabled="!isValid">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  </v-layout>
  </div>
   <div v-show="showError" id="error-div">
    <v-layout>
    <v-flex xs12 sm6 offset-sm3>
      <!-- Error messages for registration -->
      <v-alert id="error-message" :value=true icon='warning'>
        <span id="error-title">
          {{ error }}
        </span>
      </v-alert>
    </v-flex>
    </v-layout>
  </div>
    <v-layout row>
    <v-flex xs12 sm6 offset-sm3>
      <v-card>
        <v-toolbar color="teal" dark>
          <v-spacer/>
          <v-toolbar-title>Business Hours</v-toolbar-title>
          <v-spacer/>
          <!-- Button to add business hours -->
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
                <!-- Business hour day of week -->
                <v-list-tile-title>{{ businessHour.day }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">
                  {{ businessHour.openTime }} - {{ businessHour.closeTime }}
                </v-list-tile-sub-title>
              </v-list-tile-content>
              <v-list-tile-action>
                <div v-if="isEdit">
                  <!-- Edit a business hour -->
                  <v-btn icon class="mx-0" @click="editBusinessHour(businessHour)">
                    <v-icon color="teal">edit</v-icon>
                  </v-btn>
                  <!-- Delete a particular business hour -->
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
export default {
  // Passed down variables from parent component
  props: [
    'businessHours',
    'isEdit'
  ],
  data () {
    return {
      disable: false,
      showError: false,
      error: '',
      isValid: false,
      editedIndex: -1,
      time: '',
      openTimeSync: false,
      closeTimeSync: false,
      dialog: false,
      editedBusinessHour: {
        timeZone: '',
        day: '',
        openTime: null,
        closeTime: null,
        flag: 0
      },
      defaultBusinessHour: {
        timeZone: '',
        day: '',
        openTime: null,
        closeTime: null,
        flag: 0
      }
    }
  },
  computed: {
    // Compute form title for add and edit business hour
    formTitle () {
      return this.editedIndex === -1 ? 'New Business Hour' : 'Edit Business Hour'
    }
  },
  methods: {
    // Set up to add business hour
    addBusinessHour () {
      this.showError = false
      this.editedIndex = -1
      this.dialog = true
    },
    // Set up to edit business hour
    editBusinessHour (businessHour) {
      this.showError = false
      this.editedIndex = this.businessHours.indexOf(businessHour)
      this.editedBusinessHour = Object.assign({}, businessHour)
      this.dialog = true
    },
    // Set up to delete business hour
    deleteBusinessHour (businessHour) {
      const index = this.businessHours.indexOf(businessHour)
      if (this.businessHours.length <= 1) {
        this.error = 'It is required that you have one business hour.'
        this.showError = true
      } else {
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
      }
    },
    // Close the dialog popup
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedBusinessHour = Object.assign({}, this.defaultBusinessHour)
        this.editedIndex = -1
      }, 300)
    },
    // Save the edit/add business hour inputs
    save () {
      // Save edited business hour
      if (this.editedIndex > -1) {
        if (this.businessHours[this.editedIndex].flag !== 1) {
          this.businessHours[this.editedIndex].flag = 2
        }
        this.businessHours[this.editedIndex].day = this.editedBusinessHour.day
        this.businessHours[this.editedIndex].openTime = this.editedBusinessHour.openTime
        this.businessHours[this.editedIndex].closeTime = this.editedBusinessHour.closeTime
      // Save added business hour
      } else {
        this.editedBusinessHour.flag = 1
        this.editedBusinessHour.openTime = this.editedBusinessHour.openTime
        this.editedBusinessHour.closeTime = this.editedBusinessHour.closeTime
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
