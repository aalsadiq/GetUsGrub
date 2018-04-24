<template>
<div>
  <!-- <v-app>
    <v-navigation-drawer id="nav-drawer" stateless hide-overlay :mini-variant.sync="mini" v-model="drawer">
    <v-toolbar flat class="transparent">
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              <img src="../../../../Images/DefaultImages/DefaultProfileImage.png">
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-title>Admin admin</v-list-tile-title>
            </v-list-tile-content>
            <v-list-tile-action>
              <v-btn icon @click.native.stop="mini = !mini">
              <v-icon>chevron_left</v-icon>
            </v-btn>
          </v-list-tile-action>
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
  </v-navigation-drawer>
  </v-app> -->
    <v-navigation-drawer id="nav-drawer" permanent absolute v-model="drawer" >
      <v-toolbar flat class="transparent">
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              <img :src="'ImagePath'"/>
              <!-- <img src="../../../../Images/DefaultImages/DefaultProfileImage.png" /> -->
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-title> </v-list-tile-title>
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
            <!-- <v-list-tile-title>{{ ImagePath }}</v-list-tile-title> -->
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
      <!-- <v-btn id="setImagePath" name= "setImagePath" color="pink" type="submit" value ="setImagePath" v-on:click="setImagePath">check Image Path</v-btn> -->
    </v-navigation-drawer>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'
import axios from 'axios'
export default {
  name: 'admin-header',
  showImageUpload: false,
  data () {
    return {
      drawer: true,
      items: [
        { title: 'Home', icon: 'home', path: '/User/Admin' },
        { title: 'Create User', icon: 'face', path: '/User/CreateUser' },
        { title: 'Edit User', icon: 'edit', path: '/User/EditUser' },
        { title: 'Deactivate User', icon: 'block', path: '/User/DeactivateUser' },
        { title: 'Reactivate User', icon: 'check', method: 'reactivateUser', path: '/User/ReactivateUser' },
        { title: 'Delete User', icon: 'delete', path: '/User/DeleteUser' },
        { title: 'Log Out', icon: 'power_settings_new', path: '/', click: 'logout' }
      ],
      mini: true,
      right: null,
      ImagePath: '', // For Admin
      output: ''
    }
  },
  logout () {
    axios.post('http://localhost:8081/Logout', {}, {
      headers: {
        Authorization: `Bearer ${this.$store.state.authenticationToken}`
      }
    }).then(response => {
      this.$store.dispatch('setAuthenticationToken', null)
      // Force reload to clear cache
      location.reload()
      this.$router.push({path: '/'})
    }).catch(error => {
      console.log(error.response)
    })
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({path: '/Unauthorized'})
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True' &&
        jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True' &&
        jwt.decode(this.$store.state.authenticationToken).ReadPreferences === 'True') {
      } else {
        this.$router.push({path: '/Forbidden'})
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  },
  created () {
    this.ImagePath = '../../../../Images/DefaultImages/DefaultProfileImage.png'
  },
  computed: {
    imageURL () {
      return this.ImagePath
    }
  }
  // ,
  // created () {
  //   axios.get('http://localhost:8081/', { // Get User Profile...
  //     headers: {
  //       'Access-Control-Allow-Origin': 'http://localhost:8080',
  //       'Authorization': `Bearer ${this.$store.state.authenticationToken}`
  //     },
  //     params: {
  //       username: jwt.decode(this.$store.state.authenticationToken).Username
  //     }
  //   }).then(response => {
  //     this.foodPreferences = response.data
  //   }).catch(error => {
  //     Promise.reject(error)
  //   })
  // }
}
</script>

<style>
/* .application--wrap {
  height: 300px;
  width:  943px;
} */
/* .theme--light{
  width: 300px;
  height:  943px;
}
#nav-drawer{
  padding: 0em;
}
.body{
  width: 300px;
  height:  943px;
} */
</style>
