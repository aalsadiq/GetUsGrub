<template>
    <v-toolbar id="header-toolbar" dark fixed>
      <div>
        <router-link to="/">
        <v-btn flat class="home-btn">
          <v-icon>home</v-icon>
          <v-toolbar-title>
            <span id="toolbar-title">
              GetUsGrub
            </span>
          </v-toolbar-title>
        </v-btn>
      </router-link>
      </div>
      <v-spacer></v-spacer>
      <div>
        <router-link to="/Registration">
          <v-btn color="red lighten-3" class="nav-btn"><span class="btn-text">REGISTER</span></v-btn>
        </router-link>
        <router-link to="/Login">
          <v-btn color="red lighten-3" class="nav-btn"><span class="btn-text">LOGIN</span></v-btn>
        </router-link>
        <router-link to="/RestaurantBillSplitter">
          <v-btn color="red lighten-3" class="nav-btn"><span class="btn-text">SPLIT BILL</span></v-btn>
        </router-link>
      </div>
    </v-toolbar>
</template>

<style>
#nav {
  margin: auto;
}
#header-toolbar {
  background-color: #5caabc;
}
div.btn__content {
  text-transform: none;
}
.btn-text {
  font-weight: bold;
  font-size: normal;
  color: rgb(255, 255, 255);
}
#toolbar-title {
  font-weight: bold;
  font-size: x-large;
}
</style>
