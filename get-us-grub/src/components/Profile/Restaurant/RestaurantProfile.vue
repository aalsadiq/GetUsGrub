<template>
<div>
  <v-container class="scroll-y" id="scroll-target">
    <v-container id="restaurant-profile">
      <div id="profile-header">
        <v-flex xs12>
          <v-card>
            <div id="profile-image">
              <v-avatar :tile="tile"
                        :size="avatarSize">
                <img src="@/assets/ProfileImages/nets.jpg">
              </v-avatar>
            </div>
            <div id="display-name">
              <v-flex xs12>
                <h1>{{ displayName }}</h1>
              </v-flex>
            </div>
            <v-btn flat color="blue"
                   class="nav-btn"
                   :to="{name: profile-restaurant-edit}">
              Edit Profile
            </v-btn>
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
                  {{ restaurant.phoneNumber }}
                </p>
                <h3>Address:</h3>
                <p v-if='restaurant.address.street2 === ""'>
                  {{ restaurant.address.street1 }},
                  {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
                </p>
                <p v-if='restaurant.address.street2 !== ""'>
                  {{ restaurant.address.street1 }},
                  {{ restaurant.address.street2 }},
                  {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
                </p>
                <h3>BusinessHours:</h3>
                <p v-for="businessHour in restaurant.businessHours" :key="businessHour.day">
                  {{ businessHour.day }}:
                  {{ businessHour.twelveHourFormatOpenTime }} -
                  {{ businessHour.twelveHourFormatCloseTime }}
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
import MenuItemImageUpload from '@/components/ImageUploadVues/MenuItemUpload'
import ProfileImageUpload from '@/components/ImageUploadVues/ProfileImageUpload'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences'
export default {
  name: 'RestaurantProfile',
  components: {
    FoodPreferences,
    ProfileImageUpload,
    MenuItemImageUpload
  },
  data () {
    return {
      username: '',
      displayName: 'Restaurant Display Name',
      restaurant: null
    }
  },
  /*
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({ path: '/Unauthorized' })
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadIndividual === 'True') {
      } else {
        this.$router.push({ path: '/Forbidden' })
      }
    } catch (ex) {
      this.$router.push({ path: '/Forbidden' })
    }
  },
  */
  created () {
    // retrieve claim to check if they can view a user profile or a restaurant profile
    // if the claim is view user profile
    // make the username the store's username
    axios
      .get('http://localhost:8081/Profile/User', {
        headers: {
          'Access-Control-Allow-Origin': '*'
        },
        params: {
          username: jwt.decode(this.$store.state.authenticationToken).Username
        }
      })
      .then(response => {
        this.restaurant = response.data
      })
      .catch(error => {
        Promise.reject(error)
      })
  },
  methods: {
    GetProfile: function () {
      // retrieve claim to check if they can view a user profile or a restaurant profile
      // if the claim is view user profile
      // make the username the store's username
      this.username = this.$store.state.username
      axios
        .get('http://localhost:8081/Profile/Restaurant', {
          headers: {
            'Access-Control-Allow-Origin': '*'
          },
          params: {
            username: jwt.decode(this.$store.state.authenticationToken).Username
          }
        })
        .then(response => {
          this.restaurant = response.data
        })
        .catch(error => {
          Promise.reject(error)
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
