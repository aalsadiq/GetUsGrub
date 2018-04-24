<template>
  <div>
    <app-header/>
    <div id="user-profile-div">
      <div>
        <v-parallax src="/static/parallax.png" height="400">
          <div id="display-picture">
            <v-layout column align-center justify-center>
              <v-avatar
                :size="225"
                class="grey lighten-4"
              >
                <img v-bind:src="require($store.state.constants.defaultProfilePicturePath)" alt="avatar">
              </v-avatar>
              <v-flex>
                <v-btn id="image-upload-btn" dark v-if="isEdit">
                  <span id="upload-image-text">Upload Image</span>
                </v-btn>
              </v-flex>
              <v-flex id="display-name-flex">
                <span id="display-name-text">
                  Display Name
                </span>
                <v-btn id="display-name-edit-btn" dark icon v-if="isEdit">
                  <v-icon>edit</v-icon>
                </v-btn>
              </v-flex>
            </v-layout>
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
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Menus'">
        <menus :restaurantMenusList="restaurantMenusList" :isEdit="isEdit"/>
      </div>
    </div>
    <app-footer/>
  </div>
</div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import Menus from '@/components/Profile/Menus'

export default {
  components: {
    AppHeader,
    AppFooter,
    Menus
  },
  data () {
    return {
      isEdit: true,
      restaurantMenusList: [],
      tab: '',
      itemsTab: [
        'Contact Info',
        'Restaurant Details',
        'Business Hours',
        'Menus',
        'Accommodations'
      ]
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
      this.restaurantMenusList = [{
        restaurantMenu: {
          id: 51,
          restaurantId: null,
          menuName: 'menuName51',
          isActive: true,
          flag: 0,
          restaurantProfile: null,
          restaurantMenuItems: null
        },
        menuItem: [{
          id: 251,
          menuId: null,
          itemName: 'itemName251',
          itemPrice: 256.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\51.png',
          tag: 'tag250',
          description: 'description251',
          isActive: true,
          flag: 0,
          restaurantMenu: null
        },
        {
          id: 250,
          menuId: null,
          itemName: 'itemName250',
          itemPrice: 257.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\52.png',
          tag: 'tag251',
          description: 'description250',
          isActive: false,
          flag: 0,
          restaurantMenu: null
        },
        {
          id: 252,
          menuId: null,
          itemName: 'itemName252',
          itemPrice: 257.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\52.png',
          tag: 'tag251',
          description: 'description252',
          isActive: true,
          flag: 0,
          restaurantMenu: null
        }]
      },
      {
        restaurantMenu: {
          id: 53,
          restaurantId: null,
          menuName: 'edited',
          isActive: false,
          flag: 0,
          restaurantProfile: null,
          restaurantMenuItems: null
        },
        menuItem: [{
          id: 257,
          menuId: null,
          itemName: 'itemName123123',
          itemPrice: 256.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\51.png',
          tag: 'tag250',
          description: 'description251',
          isActive: true,
          flag: 0,
          restaurantMenu: null
        },
        {
          id: 258,
          menuId: null,
          itemName: '???',
          itemPrice: 257.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\52.png',
          tag: 'tag251',
          description: 'description252',
          isActive: false,
          flag: 0,
          restaurantMenu: null
        }]
      },
      {
        restaurantMenu: {
          id: 52,
          restaurantId: null,
          menuName: 'menuName52',
          isActive: true,
          flag: 0,
          restaurantProfile: null,
          restaurantMenuItems: null
        },
        menuItem: [{
          id: 253,
          menuId: null,
          itemName: 'itemName253',
          itemPrice: 257.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\52.png',
          tag: 'tag251',
          description: 'description252',
          isActive: true,
          flag: 0,
          restaurantMenu: null
        },
        {
          id: 254,
          menuId: null,
          itemName: 'itemName254',
          itemPrice: 257.99,
          itemPicture: 'C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\52.png',
          tag: 'tag251',
          description: 'description252',
          isActive: true,
          flag: 0,
          restaurantMenu: null
        }]
      }]
    }
  }
}

</script>

<style scoped>
#user-profile-div {
  margin: 4em 0 0 0;
}
#image-upload-btn {
  margin: 1em 0 0 0;
}
#display-name-flex {
  margin: 1em 0 0 0em;
}
#display-name-text {
  font-size: 2.5em;
}
#display-name-edit-btn {
  margin: 0 -2em 0.8em 0;
}
.restaurant-profile-tab-contents {
  padding: 0 0 4em 0;
  margin: auto;
}
#item-tab {
  font-weight: bold;
  color: white;
}
</style>
