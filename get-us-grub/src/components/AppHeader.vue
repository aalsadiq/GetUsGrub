<template>
    <v-toolbar id="header-toolbar" dark fixed>
      <v-toolbar-side-icon></v-toolbar-side-icon>
        <img src="@/assets/GetUsGrub-icon.png">
      <v-toolbar-title>
        GetUsGrub
      </v-toolbar-title>
      <v-spacer></v-spacer>
          <router-link to="/">
            <v-btn icon><v-icon>home</v-icon></v-btn>
          </router-link>
          <router-link to="/Registration">
            <v-btn color="teal">Register</v-btn>
          </router-link>
          <v-btn color="teal">Login</v-btn>
          <router-link to="/RestaurantBillSplitter">
            <v-btn color="teal">Split Bill</v-btn>
          </router-link>

          <v-btn icon><v-icon>email</v-icon></v-btn>
    </v-toolbar>
</template>

<style>
#nav {
  margin: auto;
}
#header-toolbar {
  background-color: #660000;
}
</style>
