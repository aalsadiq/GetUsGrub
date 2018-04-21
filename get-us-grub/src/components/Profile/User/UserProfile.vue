<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="user-profile">
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
              <template>
                <div>
                  <v-layout row justify-center>
                    <template>
                      <v-dialog v-model="dialog" persistent max-width="1000">
                        <v-btn color="primary" dark slot="activator">Edit Profile Image</v-btn>
                        <v-card>
                          <v-layout wrap>
                            <image-upload />
                          </v-layout>
                          <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="green darken-1" flat @click.native="dialog = false">Close</v-btn>
                          </v-card-actions>
                        </v-card>
                      </v-dialog>
                    </template>
                    <v-dialog v-model="dialog2" persistent max-width="500px">
                      <v-btn color="primary" dark slot="activator">Edit Display Name</v-btn>
                      <v-card>
                        <v-card-title>
                          <span>Edit Display Name</span>
                          <v-spacer></v-spacer>
                        </v-card-title>
                        <v-card-text>
                          <v-container grid-list-md>
                            <v-layout wrap>
                              <v-flex xs12>
                                <v-text-field label="Display Name" required></v-text-field>
                              </v-flex>
                            </v-layout>
                          </v-container>
                          <small>*indicates required field</small>
                        </v-card-text>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn color="primary" @click="EditUserProfile" :loading="loading">Save</v-btn>
                          <v-btn color="primary" @click="dialog2=false">Close</v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                  </v-layout>
                </div>
              </template>
            </v-card>
          </v-flex>
        </div>
        <div id="food-preferences">
          <v-flex xs12>
            <v-card>
              <food-preferences/>
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
  name: 'UserProfile',
  components: {
    FoodPreferences,
    ImageUpload
  },
  data () {
    return {
      username: '',
      displayName: null,
      profile: null,
      errors: null,
      dialog: false,
      dialog2: false
    }
  },
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
        this.profile = response.data
      })
      .catch(error => {
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
  methods: {
    GetProfile: function () {
      // retrieve claim to check if they can view a user profile or a restaurant profile
      // if the claim is view user profile
      // make the username the store's username
      this.username = this.$store.state.username
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
          this.profile = response.data
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

#user-profile {
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
