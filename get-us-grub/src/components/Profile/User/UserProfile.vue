<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="user-profile">
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
                          <span>Edit Profile</span>
                          <v-spacer></v-spacer>
                        </v-card-title>
                        <v-card-text>
                          <v-container grid-list-md>
                            <v-layout wrap>
                              <v-flex xs12>
                                <v-text-field
                                  label="Display Name"
                                  v-model="displayName"
                                  required
                                  ></v-text-field>
                              </v-flex>
                            </v-layout>
                          </v-container>
                          <small>*indicates required field</small>
                        </v-card-text>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn color="primary" @click.native="editUserProfile">Save</v-btn>
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
      displayName: '',
      displayPicture: '',
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
      if (jwt.decode(this.$store.state.authenticationToken).ReadUserProfile === 'True') {
        axios.get(this.$store.state.urls.profileManagement.userProfile, {
          headers: {
            Authorization: `Bearer ${this.$store.state.authenticationToken}`
          }
        }).then(response => {
          this.displayName = response.data.displayName
          this.displayPicture = response.data.displayPicture
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
        this.$router.push({ path: '/Forbidden' })
      }
    } catch (ex) {
      this.$router.push({ path: '/Forbidden' })
    }
  },
  created () {
  },
  methods: {
    editUserProfile: function () {
      axios.post(this.$store.state.urls.profileManagement.updateUserProfile,
        {
          displayName: this.displayName
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
