<template>
  <div>
    <div id="restaurant-profile-div">
      <div>
        <!-- Portion with profile picture and display name -->
        <v-parallax src="/static/parallax.png" height="400">
          <!-- Display picture -->
          <div id="display-picture">
            <v-layout column align-center justify-center>
              <v-avatar
                :size="200"
                class="grey lighten-4"
              >
                <img :src="profile.displayPicture" alt="avatar">
              </v-avatar>
              <v-flex>
                <!-- <v-btn id="image-upload-btn" dark v-if="isEdit">
                  <span id="upload-image-text">Upload Image</span>
                </v-btn> -->
                <div v-if="isEdit">
                  <profile-image-upload id="image-upload"/>
                </div>
              </v-flex>
              <v-flex>
              <div id="display-name-div">
                <div v-if="!editDisplayName">
                  <span id="display-name-text">
                    {{ profile.displayName }}
                  </span>
                  <v-btn id="display-name-edit-btn" dark icon @click="toggleEditDisplayName()" v-if="isEdit">
                    <v-icon>edit</v-icon>
                  </v-btn>
                </div>
                <div id="display-name-text"  v-if="editDisplayName && isEdit">
                  <v-layout row>
                  <v-flex>
                  <v-text-field
                    label="Enter a display name"
                    v-model="profile.displayName"
                    :rules="$store.state.displayNameRules"
                    required
                    dark
                  ></v-text-field>
                  </v-flex>
                  <span v-if="showDisplayNameError"> {{ error }} </span>
                  <v-flex>
                  <v-btn id="display-name-edit-btn" dark icon @click="submitEditDisplayName()">
                    <v-icon>save</v-icon>
                  </v-btn>
                  </v-flex>
                  </v-layout>
                </div>
              </div>
            </v-flex>
            </v-layout>
            <v-btn
              v-if="!isEdit"
              fab
              color="cyan accent-2"
              bottom
              right
              absolute
              @click="toggleIsEdit()"
              id="edit-button"
              slot="activator"
              >
              <v-icon>edit</v-icon>
            </v-btn>
            <v-btn
              id="submit-btn"
              v-if="isEdit"
              fab
              color="cyan accent-2"
              bottom
              right
              absolute
              @click="editRestaurantProfile()"
              slot="activator"
              >
              <v-icon>save</v-icon>
            </v-btn>
            <v-btn
              id="cancel-btn"
              v-if="isEdit"
              fab
              color="pink"
              bottom
              right
              absolute
              @click="cancel()"
              slot="activator"
              >
              <v-icon>clear</v-icon>
            </v-btn>
          </div>
        </v-parallax>
      </div>
      <div>
      <!-- Tabs toolbar -->
      <v-tabs
          color="cyan"
          slot="extension"
          v-model="tab"
          grow
          show-arrows
        >
        <v-tabs-slider color="yellow"></v-tabs-slider>
        <v-tab v-for="itemTab in itemsTab" :key="itemTab" id="item-tab">
          {{ itemTab }}
        </v-tab>
      </v-tabs>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Contact Info'">
        <contact-info class="profile-component" :profile="profile" :address="profile.address" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Restaurant Details'">
        <restaurant-details class="profile-component" :details="profile.details" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Business Hours'">
        <business-hours class="profile-component" :businessHours="profile.businessHours" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Menus'">
        <menus class="profile-component" :restaurantMenusList="profile.restaurantMenusList" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Accommodations'">
        <food-preferences class="profile-component" :isEdit="isEdit"/>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import axios from 'axios'
import jwt from 'jsonwebtoken'
import ContactInfo from './ContactInfo'
import RestaurantDetails from './RestaurantDetails'
import BusinessHours from './BusinessHours'
import Menus from './Menus'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences'
import MenuItemImageUpload from '@/components/ImageUploadVues/MenuItemUpload'
import RestaurantImageUpload from '@/components/ImageUploadVues/RestaurantImageUpload'

export default {
  components: {
    ContactInfo,
    RestaurantDetails,
    BusinessHours,
    Menus,
    FoodPreferences,
    'profile-image-upload': RestaurantImageUpload,
    'menu-image-upload': MenuItemImageUpload
  },
  data () {
    return {
      showDisplayNameError: false,
      error: '',
      editDisplayName: false,
      isEdit: false,
      profile: {
        displayName: '',
        displayPicture: '',
        phoneNumber: '',
        address: {
          street1: '',
          street2: '',
          city: '',
          state: '',
          zip: null
        },
        details: {
          avgFoodPrice: 1,
          hasReservations: null,
          hasDelivery: null,
          hasTakeOut: null,
          acceptCreditCards: null,
          attire: null,
          servesAlcohol: null,
          hasOutdoorSeating: null,
          hasTv: null,
          hasDriveThru: null,
          caters: null,
          allowsPets: null,
          foodType: ''
        },
        restaurantMenusList: [],
        businessHours: []
      },
      tab: '',
      itemsTab: [
        'Contact Info',
        'Restaurant Details',
        'Business Hours',
        'Menus',
        'Accommodations'
      ]
    }
  },
  // Check if user has permission to view the page
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push('Unauthorized')
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True') {
      } else {
        this.$router.push('Forbidden')
      }
    } catch (ex) {
      this.$router.push('Forbidden')
    }
  },
  // Get the restaurant profile information
  created () {
    this.getRestaurantProfile()
  },
  methods: {
    getRestaurantProfile () {
      axios.get(this.$store.state.urls.profileManagement.restaurantProfile, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.profile = response.data
        // this.updateProfileUrl(this.profile.displayPicture)
      }).catch(error => {
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push({ path: '/Unauthorized' })
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push({ path: '/Forbidden' })
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push({ path: '/ResourceNotFound' })
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push({ path: '/InternalServerError' })
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response
          Promise.reject(error)
        }
      })
    },
    editRestaurantProfile: function () {
      axios.post(this.$store.state.urls.profileManagement.updateRestaurantProfile,
        this.profile,
        {
          headers: { Authorization: `Bearer ${this.$store.state.authenticationToken}` }
        }).then(response => {
        this.getRestaurantProfile()
        this.isEdit = false
      }).catch(error => {
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push({ path: '/Unauthorized' })
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push({ path: '/Forbidden' })
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push({ path: '/ResourceNotFound' })
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push({ path: '/InternalServerError' })
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
    },
    toggleIsEdit () {
      this.isEdit = !this.isEdit
    },
    toggleEditDisplayName () {
      this.editDisplayName = !this.editDisplayName
    },
    submitEditDisplayName () {
      if (this.checkDisplayNameDoesNotEqualUsername()) {
        this.toggleEditDisplayName()
        this.editRestaurantProfile()
      }
    },
    checkDisplayNameDoesNotEqualUsername () {
      if (this.$store.state.username === this.editDisplayName) {
        this.error = 'Username must not equal display name.'
        this.showDisplayNameError = true
        return false
      } else {
        this.showDisplayNameError = false
        return true
      }
    },
    cancel () {
      this.toggleIsEdit()
      this.getRestaurantProfile()
    }
  }
}
</script>

<style scoped>
#restaurant-profile-div {
  margin: 0 0 0 0;
  padding: 3.5em 0 0 0;
}
#image-upload-btn {
  margin: 1em 0 0 0;
}
.restaurant-profile-tab-contents {
  padding: 0 0 2em 0;
  margin: auto;
}
#item-tab {
  font-weight: bold;
  color: white;
}
.profile-component {
  padding: 2em 0 0 0;
}
#display-name-div {
  margin: 1em 0 0 0em;
}
#display-name-text {
  font-size: 2.5em;
}
#display-name-edit-btn {
  margin: 0 -2em 0.8em 0;
}
#edit-profile-btn-div {
  margin: -3em 0 0 0;
}
#edit-profile-btn {
  padding: 0 0 0 0;
}
#edit-profile-btn-txt {
  margin: 1.1em 0 0 0;
}
.btn--bottom.btn--absolute {
  bottom: 20px;
}
#submit-btn {
  right: 90px;
}
#cancel-btn {
  color: white;
}
/* #image-upload{
  width: 0px;
  height: 0px;
} */
</style>
