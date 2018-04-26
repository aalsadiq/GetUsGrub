<template>
  <div>
    <v-navigation-drawer id="nav-drawer" permanent absolute v-model="drawer" >
      <v-toolbar flat class="transparent">
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              <img :src="displayPicture" id="display-picture"/>
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
    logout () {
      axios.post('http://localhost:8081/Logout', {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.commit('setAuthenticationToken', null)
        // Force refresh of page
        location.reload()
        this.$router.push({path: '/'})
      }).catch(error => {
        this.$store.commit('setAuthenticationToken', null)
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

<style>
#logout-btn{
  width: 292px;
  height: 40px;
  padding-left: 55px;
  padding-right: 250px;
}
#logout-text{
  padding-left:35px;
}
/* div.list__tile.list__tile--avatar{
    padding-left: 50px;
} */
div#image-upload{
    width: 0px;
    height: 550px;
    padding-left: 128px;
}
</style>
