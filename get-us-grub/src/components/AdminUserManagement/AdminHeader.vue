<template>
  <div>
    <v-btn @click.stop="drawer = !drawer" bottom left dark color="pink">
      <v-icon>
        list
      </v-icon>
    </v-btn>
    <v-navigation-drawer id="nav-drawer" temporary absolute v-model="drawer" >
      <v-toolbar flat class="transparent">
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar id="admin-picture">
              <img :src="displayPicture + '?' + getRandomQuery()" id="display-picture"/>
              <h1 id="displayname-text">
                {{ displayName }}
              </h1>
            </v-list-tile-avatar>
          <v-list-tile-content>
          <v-list-tile-title/>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-toolbar>
    <v-list class="header-admin" dense>
      <v-list-tile v-for="item in items" :key="item.title" @click="item" :to="item.path">
        <v-list-tile-action>
        <v-icon>{{ item.icon }}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content ref="items">
          <v-list-tile-title>{{ item.title }}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
      <image-upload-app id="image-upload"/>
      <v-btn flat id="logout-btn" @click="logout()">
        <v-icon>power_settings_new</v-icon>
        <h5 id="logout-text"> Logout </h5>
      </v-btn>
    </v-navigation-drawer>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'
import axios from 'axios'
import moment from 'moment'
import profileImageUpload from '@/components/ImageUploadVues/ProfileImageUpload'

export default {
  name: 'admin-header',
  showImageUpload: false,
  components: {
    'image-upload-app': profileImageUpload
  },
  data () {
    return {
      drawer: true,
      items: [
        { title: 'Home', icon: 'home', path: '/User/Admin' },
        { title: 'Create User', icon: 'face', path: '/User/CreateUser' },
        { title: 'Edit User', icon: 'edit', path: '/User/EditUser' },
        { title: 'Deactivate User', icon: 'block', path: '/User/DeactivateUser' },
        { title: 'Reactivate User', icon: 'check', method: 'reactivateUser', path: '/User/ReactivateUser' },
        { title: 'Delete User', icon: 'delete', path: '/User/DeleteUser' }
      ],
      mini: true,
      right: null,
      output: '',
      displayName: '',
      displayPicture: ''
    }
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({path: '/Unauthorized'})
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
      } else {
        this.$router.push({path: '/Forbidden'})
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  },
  created () {
    this.getAdminProfile()
  },
  methods: {
    getRandomQuery () {
      return moment().format()
    },
    logout () {
      axios.post(this.$store.state.urls.logout.logoutUser, {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.commit('setAuthenticationToken', null)
        this.$store.commit('setIsAuthenticated', false)
        this.$store.commit('setUsername', '')
        // Force refresh of page
        location.reload()
        this.$router.push({path: '/'})
      }).catch(error => {
        this.$store.commit('setAuthenticationToken', null)
        this.$store.commit('setIsAuthenticated', false)
        this.$store.commit('setUsername', '')
        // Force refresh of page
        location.reload()
        this.$router.push({path: '/'})
        Promise.reject(error)
      })
    },
    getAdminProfile () {
      try {
        if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
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
    }
  }
}
</script>

<style scoped>
#logout-btn{
  width: 292px;
  height: 40px;
  padding-left: 55px;
  padding-right: 250px;
}
#logout-text{
  padding-left:35px;
}
#admin-picture {
  margin: 0 0 0 2.6em;
}
#displayname-text {
  padding: 0 0 0 1.1em;
}
</style>
