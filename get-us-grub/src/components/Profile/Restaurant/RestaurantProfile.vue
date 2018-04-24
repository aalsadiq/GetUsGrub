<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="restaurant-profile">
        <div id="profile-header">
          <v-flex xs12>
            <v-card>
              <div id="profile-image">
                <v-avatar :size="avatarSize">
                  <img src="../../../../../Images/DefaultImages/DefaultProfileImage.png">
                </v-avatar>
              </div>
              <div id="display-name">
                <v-flex xs12>
                  <h1>{{ profile.displayName }}</h1>
                </v-flex>
              </div>
              <template>
                <div>
                  <v-layout row justify-center>
                    <template>
                      <v-dialog v-model="dialog" persistent max-width="1000">
                        <v-btn color="primary" slot="activator">Edit Profile Image</v-btn>
                        <v-card>
                          <v-layout wrap>
                            <image-upload />
                          </v-layout>
                          <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="green darken-1" flat @click.native="dialog=false">Close</v-btn>
                          </v-card-actions>
                        </v-card>
                      </v-dialog>
                    </template>
                    <!--Get this out of a dialog-->
                    <v-dialog v-model="dialog2" persistent max-width="500px">
                      <v-btn color="primary" dark slot="activator">Edit Profile</v-btn>
                      <v-card>
                        <v-card-title>
                          <span>Edit Profile Name</span>
                          <v-spacer></v-spacer>
                        </v-card-title>
                        <v-card-text>
                          <v-container grid-list-md>
                            <v-layout wrap>
                              <v-flex xs12>
                                <v-text-field label="Display Name"
                                              v-model="newDisplayName"
                                              required></v-text-field>
                              </v-flex>
                            </v-layout>
                          </v-container>
                          <small>*indicates required field</small>
                        </v-card-text>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn color="primary" @click.native="editRestaurantProfile">Save</v-btn>
                          <v-btn color="primary" @click.native="dialog2=false">Close</v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                  </v-layout>
                </div>
              </template>
            </v-card>
          </v-flex>
        </div>
        <div id="profile-info">
          <v-flex xs12>
            <v-card>
              <v-layout row justify-space-between>
                <v-flex xs12>
                  <h3>Phone Number:</h3>
                  <p>
                    {{ profile.phoneNumber }}
                  </p>
                  <h3>Address:</h3>
                  <p v-if='profile.address.street2 === ""'>
                    {{ profile.address.street1 }},
                    {{ profile.address.city }}, {{ profile.address.state }} {{ profile.address.zip }}
                  </p>
                  <p v-if='profile.address.street2 !== ""'>
                    {{ profile.address.street1 }},
                    {{ profile.address.street2 }},
                    {{ profile.address.city }}, {{ profile.address.state }} {{ profile.address.zip }}
                  </p>
                  <h3>BusinessHours:</h3>
                  <p v-for="businessHour in profile.businessHours" :key="businessHour.day">
                    {{ businessHour.day }}:
                    {{ businessHour.OpenTime }} -
                    {{ businessHour.CloseTime }}
                  </p>
                </v-flex>
              </v-layout>
            </v-card>
          </v-flex>
        </div>
        <div id="profile-details">
          <v-flex xs12>
            <v-card>
              <v-layout row justify-space-between>
                <v-flex xs12>
                  <h3>Phone Number:</h3>
                  <p>
                    {{ profile.phoneNumber }}
                  </p>
                  <h3>Address:</h3>
                  <p v-if='profile.address.street2 === ""'>
                    {{ profile.address.street1 }},
                    {{ profile.address.city }}, {{ profile.address.state }} {{ profile.address.zip }}
                  </p>
                  <p v-if='profile.address.street2 !== ""'>
                    {{ profile.address.street1 }},
                    {{ profile.address.street2 }},
                    {{ profile.address.city }}, {{ profile.address.state }} {{ profile.address.zip }}
                  </p>
                  <h3>BusinessHours:</h3>
                  <p v-for="businessHour in profile.businessHours" :key="businessHour.day">
                    {{ businessHour.day }}:
                    {{ businessHour.OpenTime }} -
                    {{ businessHour.CloseTime }}
                  </p>
                </v-flex>
              </v-layout>
            </v-card>
          </v-flex>
        </div>
        <div id="food-preferences">
          <v-flex xs12>
            <v-card>
              <food-preferences />
            </v-card>
          </v-flex>
        </div>
      </v-container>
    </v-container>
  </div>
</template>

<script>
import axios from 'axios'
import jwt from 'jsonwebtoken'
import ImageUpload from '@/components/ImageUploadVues/ImageUpload'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences'
export default {
  name: 'RestaurantProfile',
  components: {
    FoodPreferences,
    ImageUpload
  },
  data () {
    return {
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
          avgFoodPrice: null,
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
        restaurantMenu: {
          id: null,
          menuName: '',
          isActive: null,
          flag: null
        },
        menuItem: {
          id: null,
          itemName: '',
          itemPicture: '',
          tag: '',
          description: '',
          isActive: null,
          flag: null
        },
        businessHours: [],
        businessHour: {
          id: null,
          day: '',
          openTime: '',
          closeTime: '',
          flag: null
        }
      },
      errors: '',
      dialog: false,
      dialog2: false
    }
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({ path: '/Unauthorized' })
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True') {
        axios.get(this.$store.state.urls.profileManagement.restaurantProfile, {
          headers: {
            Authorization: `Bearer ${this.$store.state.authenticationToken}`
          }
        }).then(response => {
          console.log(response.data.restaurantMenusList)
          this.profile = response.data
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
      } else {
        console.log('forbidden else')
        this.$router.push({ path: '/Forbidden' })
      }
    } catch (ex) {
      console.log('forbidden exception')
      this.$router.push({ path: '/Forbidden' })
    }
  },
  created () {
  },
  methods: {
    editRestaurantProfile: function () {
      axios.post(this.$store.state.urls.profileManagement.updateRestaurantProfile,
        {
          displayName: this.newDisplayName
        },
        {
          headers: { Authorization: `Bearer ${this.$store.state.authenticationToken}` }
        }).then(response => {
        this.dialog2 = false
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
    }
  },
  computed: {
    avatarSize () {
      return '200px'
    }
  }
}
</script>

<style scoped>
#scroll-target {
  max-height: 56.5em;
  position: absolute;
  overflow-x: hidden;
}

#restaurant-profile {
  max-width: 1200px;
  margin: auto;
}

#profile-header {
  padding-top: 2em;
}

#display-name {
  padding-top: 1em;
}

#food-preferences {
  padding-top: 1em;
}
</style>
