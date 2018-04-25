<template>
  <div id="contact-info-div">
    <div>
    <!-- Conact info dialog popup form -->
    <v-dialog v-model="dialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-layout>
              <v-form v-model="valid">
                <v-layout>
                  <!-- User input for phone number -->
                  <v-flex xs12>
                  <v-subheader>Enter a phone number</v-subheader>
                  </v-flex>
                  <v-flex xs12>
                    <v-text-field
                      v-model="editedContact.phoneNumber"
                      placeholder="(562)111-5555"
                      prepend-icon="phone"
                      :rules="$store.state.rules.phoneNumberRules"
                      single-line
                    ></v-text-field>
                  </v-flex>
                  </v-layout>
                  <v-layout>
                  <!-- User input for address -->
                  <v-flex xs12>
                    <v-subheader>Enter the address of your restaurant</v-subheader>
                  </v-flex>
                  </v-layout>
                  <v-layout>
                  <v-flex xs12>
                    <v-text-field
                      label="Street 1"
                      placeholder="1111 Snowy Rock Pl"
                      v-model="editedContact.address.street1"
                      :rules="$store.state.rules.addressStreet1Rules"
                      required
                    ></v-text-field>
                    <v-text-field
                      label="Street 2"
                      placeholder="Unit 2"
                      v-model="editedContact.address.street2"
                    ></v-text-field>
                    <v-text-field
                      label="City"
                      placeholder="Long Beach"
                      v-model="editedContact.address.city"
                      :rules="$store.state.constants.addressCityRules"
                      required
                    ></v-text-field>
                    <v-select
                      :items="$store.state.constants.states"
                      v-model="editedContact.address.state"
                      item-text="name"
                      item-value="abbreviation"
                      label="Select a state"
                      :rules="$store.state.rules.addressStateRules"
                      single-line
                      auto
                      append-icon="map"
                      hide-details
                      required
                    ></v-select>
                      <v-text-field
                      label="Zip"
                      placeholder="92812"
                      :rules="$store.state.rules.addressZipRules"
                      type="number"
                      v-model.number="editedContact.address.zip"
                      required
                    ></v-text-field>
                  </v-flex>
                  </v-layout>
              </v-form>
            </v-layout>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="save" :disabled="!valid">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
    <v-layout row wrap>
      <v-flex d-flex>
        <v-card class="card--flex-toolbar">
          <!-- Card with toolbar that holds the contact information of a user -->
          <v-toolbar dark card prominent color="teal">
            <v-spacer/>
            <v-toolbar-title>Contact</v-toolbar-title>
            <v-spacer/>
            <!-- Edit button on the card toolbar-->
              <div v-if="isEdit">
                <v-btn icon class="mx-0" @click="editContactInfo()">
                  <v-icon color="black">edit</v-icon>
                </v-btn>
              </div>
            </v-toolbar>
          <v-divider></v-divider>
          <v-card-text style="height: 175px;">
            <h3>Phone Number:</h3>
          <p class="paragrah">
            {{ phoneNumber }}
          </p>
          <h3>Address:</h3>
          <p class="paragraph" v-if='address.street2 === ""'>
            {{ address.street1 }} <br>
            {{ address.city }}, {{ address.state }} {{ address.zip }}
          </p>
          <p class="paragraph" v-if='address.street2 !== ""'>
            {{ address.street1 }} <br>
            {{ address.street2 }} <br>
            {{ address.city }}, {{ address.state }} {{ address.zip }}
          </p>
          </v-card-text>
        </v-card>
      </v-flex>
      <!-- Google embedded map displayed here -->
      <v-flex>
        <google-embed-map/>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import GoogleEmbedMap from '@/components/EmbedMap/GoogleEmbedMap'

export default {
  // Vue component dependencies
  components: {
    GoogleEmbedMap
  },
  // Passed down variables from parent component
  props: [
    'phoneNumber',
    'address',
    'isEdit'
  ],
  data () {
    return {
      dialog: false,
      formTitle: 'Edit Contact Info',
      valid: false,
      editedContact: {
        phoneNumber: '',
        address: {
          street1: '',
          street2: '',
          city: '',
          state: '',
          zip: 0
        }
      }
    }
  },
  created () {
    this.editedContact.phoneNumber = this.phoneNumber
    this.editedContact.address = this.address
  },
  methods: {
    editContactInfo () {
      this.editedContact.phoneNumber = this.phoneNumber
      this.editedContact.address = this.address
      this.dialog = true
    },
    // Close dialog popup
    close () {
      this.dialog = false
      setTimeout(() => {
      }, 300)
    },
    // Save user input contact information
    save () {
      this.phoneNumber = this.editedContact.phoneNumber
      this.address = this.editedContact.address
      this.close()
    }
  }
}
</script>
