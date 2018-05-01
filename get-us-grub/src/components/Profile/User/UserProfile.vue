<template>
  <div>
    <div id="user-profile-div">
      <div>
        <v-parallax src="/static/parallax.png" height="425">
          <div id="display-picture">
            <v-layout column align-center justify-center>
              <v-avatar
                :size="255"
                class="grey lighten-4"
              >
              <img :src="displayPicture + '?' + appendRandomQueryToImageUrl()" alt="avatar">
              </v-avatar>
              <v-flex>
               <div v-if="isEdit">
                  <profile-image-upload id="image-upload"/>
                </div>
              </v-flex>
              <v-flex>
              <div id="display-name-div">
                  <div v-if="!editDisplayName">
                    <span id="display-name-text">
                      {{ displayName }}
                    </span>
                    <v-btn id="display-name-edit-btn" dark icon @click="toggleEditDisplayName()" v-if="isEdit">
                      <v-icon>edit</v-icon>
                    </v-btn>
                  </div>
                  <div id="display-name-text" v-if="editDisplayName && isEdit">
                    <v-layout row>
                    <v-flex>
                    <v-text-field
                      label="Enter a display name"
                      v-model="displayName"
                      :rules="$store.state.rules.displayNameRules"
                      required
                      dark
                    ></v-text-field>
                    </v-flex>
                    <v-flex>
                    <v-btn id="display-name-edit-btn" dark icon @click="toggleEditDisplayName()">
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
              @click="editUserProfile()"
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
            <div class="user-profile-tab-contents" v-if="itemsTab[tab] === 'Food Preferences'">
        <food-preferences ref="preferences" class="profile-component" :isEdit="isEdit"/>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import axios from 'axios'
import jwt from 'jsonwebtoken'
import moment from 'moment'
import ProfileImageUpload from '@/components/ImageUploadVues/ProfileImageUpload'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences'

export default {
  name: 'UserProfile',
  components: {
    FoodPreferences,
    'profile-image-upload': ProfileImageUpload
  },
  data () {
    return {
      editDisplayName: false,
      isEdit: false,
      displayName: '',
      displayPicture: '',
      tab: '',
      itemsTab: [
        'Food Preferences'
      ],
      errors: ''
    }
  },
  // Check permission if user can access this page
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push('Unauthorized')
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUserProfile === 'True') {
      } else {
        this.$router.push('Forbidden')
      }
    } catch (ex) {
      this.$router.push('Forbidden')
    }
  },
  created () {
    this.getUserProfile()
  },
  methods: {
    appendRandomQueryToImageUrl () {
      return moment().format()
    },
    getUserProfile () {
      axios.get(this.$store.state.urls.profileManagement.userProfile, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.displayName = response.data.displayName
        this.displayPicture = response.data.displayPicture
        this.appendRandomQueryToImageUrl()
        try {
          this.$refs.preferences.getFoodPreferences()
        } catch (ex) {}
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
    editUserProfile: function () {
      try {
        this.$refs.preferences.update()
      } catch (ex) {}
      axios.post(this.$store.state.urls.profileManagement.updateUserProfile,
        {
          displayName: this.displayName,
          displayPicture: this.displayPicture
        },
        {
          headers: { Authorization: `Bearer ${this.$store.state.authenticationToken}` }
        }).then(response => {
        this.isEdit = false
        this.getUserProfile()
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
      }).then(this.getUserProfile())
    },
    toggleIsEdit () {
      this.isEdit = !this.isEdit
    },
    toggleEditDisplayName () {
      this.editDisplayName = !this.editDisplayName
    },
    cancel () {
      this.toggleIsEdit()
      this.getUserProfile()
    }
  }
}
</script>

<style scoped>
#user-profile-div {
  margin: 0 0 0 0;
  padding: 3.5em 0 0 0;
}
#image-upload-btn {
  margin: 1em 0 0 0;
}
.user-profile-tab-contents {
  padding: 0 0 4em 0;
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
#main-edit-btns-div {
  align-self: right;
}
#edit-btns-div {
  margin: 0 0 3em 0;
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
</style>
