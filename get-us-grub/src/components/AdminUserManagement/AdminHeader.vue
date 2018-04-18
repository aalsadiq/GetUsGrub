<template>
<div>
    <!-- <v-card height="350px"> -->
    <!-- <v-navigation-drawer id="menu" stateless hide-overlay :mini-variant.sync="mini" v-model="drawer"> -->
    <v-navigation-drawer id="nav-drawer" permanent absolute v-model="drawer" >
      <!-- used to navigate through application to work with or without vue-router outside the box-->
      <!--permanent: remains visible regardless of screensize absolute: Position the element absolutely is false v-model= drawer-->
      <!--v-toolbar:flat: removes the toolbar box-shadow-->
      <v-toolbar flat class="transparent"> <!-- the square to the left -->
      <!--v-list: where our items are held..-->
        <v-list class="pa-0">
          <v-list-tile avatar> <!--avatar: used to set minimum tile height on a single-line list item -->
            <v-list-tile-avatar>
              <img src="@/assets/ProfileImages/DefaultProfileImage.png"><!--Grab image from the store... -->
              <!-- <v-btn absolute dark fab top right color="pink"> -->
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-title>Admin admin</v-list-tile-title><!-- Grab username when logged in?-->
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
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  name: 'admin-header',
  showImageUpload: false,
  data () {
    return {
      drawer: true,
      // mini:true,
      // right:null,
      items: [
        { title: 'Home', icon: 'home', path: '/User/Admin' },
        { title: 'Create User', icon: 'face', path: '/User/CreateUser' },
        { title: 'Edit User', icon: 'edit', path: '/User/EditUser' },
        { title: 'Deactivate User', icon: 'block', path: '/User/DeactivateUser' },
        { title: 'Reactivate User', icon: 'check', method: 'reactivateUser', path: '/User/ReactivateUser' },
        { title: 'Delete User', icon: 'delete', path: '/User/DeleteUser' }
      ],
      right: null
    }
  }
}
</script>

<style>
.navigation{
  position: relative;
}
.menu{
margin-left:0px;
margin-right:0px;
margin-top:0px;
width:0px;
height:0px;
padding:0px;
}
</style>
