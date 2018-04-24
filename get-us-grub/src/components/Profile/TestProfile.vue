<template>
  <div>
    <app-header/>
    <div id="user-profile-div">
      <div>
        <v-parallax src="/static/parallax.png" height="400">
          <v-btn dark icon @click="toggleIsEdit()" fixed>
            <v-icon>edit</v-icon>
          </v-btn>
          <div id="display-picture">
            <v-layout column align-center justify-center>
              <v-avatar
                :size="225"
                class="grey lighten-4"
              >
                <img v-bind:src="require('C:/Users/Jenn/Desktop/GitHub/GetUsGrub/get-us-grub/src/assets/DefaultProfileImage.png')" alt="avatar">
              </v-avatar>
              <v-flex>
                <v-btn id="image-upload-btn" dark v-if="isEdit">
                  <span id="upload-image-text">Upload Image</span>
                </v-btn>
              </v-flex>
              <v-flex>
              <div id="display-name-div">
                  <div v-if="!editDisplayName">
                    <span id="display-name-text">
                      {{ restaurantProfile.displayName }}
                    </span>
                    <v-btn id="display-name-edit-btn" dark icon @click="toggleEditDisplayName()" v-if="isEdit">
                      <v-icon>edit</v-icon>
                    </v-btn>
                  </div>
                  <span id="display-name-text"  v-if="editDisplayName">
                    <v-layout row>
                    <v-flex>
                    <v-text-field
                      label="Enter a display name"
                      v-model="restaurantProfile.displayName"
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
                  </span>
              </div>
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
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Contact Info'">
        <contact-info class="profile-component" :phoneNumber="restaurantProfile.phoneNumber" :address="restaurantProfile.address" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Restaurant Details'">
        <restaurant-details class="profile-component" :details="restaurantProfile.details" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Business Hours'">
        <business-hours class="profile-component" :businessHours="restaurantProfile.businessHours" :isEdit="isEdit"/>
      </div>
      <div class="restaurant-profile-tab-contents" v-if="itemsTab[tab] === 'Menus'">
        <menus class="profile-component" :restaurantMenusList="restaurantProfile.restaurantMenusList" :isEdit="isEdit"/>
      </div>
    </div>
    <app-footer/>
  </div>
</div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import ContactInfo from '@/components/Profile/ContactInfo'
import RestaurantDetails from '@/components/Profile/RestaurantDetails'
import BusinessHours from '@/components/Profile/BusinessHours'
import Menus from '@/components/Profile/Menus'

export default {
  components: {
    AppHeader,
    AppFooter,
    ContactInfo,
    RestaurantDetails,
    BusinessHours,
    Menus
  },
  data () {
    return {
      editDisplayName: false,
      isEdit: false,
      restaurantProfile: null,
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
      this.restaurantProfile = {
        displayName: 'Test Restaurant',
        displayPicture: 'C:\\Users\\Andrew\\Documents\\GetUsGrub\\Images\\DefaultImages\\DefaultProfileImage.png',
        phoneNumber: '(714)291-2222',
        address: {
          street1: '116 E 2nd St',
          street2: '',
          city: 'Los Angeles',
          state: 'CA',
          zip: 90012
        },
        details: {
          avgFoodPrice: 1,
          hasReservations: null,
          hasDelivery: null,
          hasTakeOut: null,
          acceptCreditCards: null,
          attire: null,
          servesAlcohol: null,
          hasOutdoorSeating: null,
          hasTv: null,
          hasDriveThru: null,
          caters: null,
          allowsPets: null,
          foodType: 'American Food'
        },
        geoCoordinates: null,
        restaurantMenusList: [{
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
        }],
        businessHours: [{
          id: 76,
          restaurantId: null,
          day: 'Sunday',
          openTime: '2018-05-17T07:00:00',
          closeTime: '2018-05-18T06:59:00',
          flag: 0,
          restaurantProfile: null
        },
        {
          id: 77,
          restaurantId: null,
          day: 'Thursday',
          openTime: '2018-05-17T19:00:00',
          closeTime: '2018-05-18T06:59:00',
          flag: 0,
          restaurantProfile: null
        },
        {
          id: 78,
          restaurantId: null,
          day: 'Thursday',
          openTime: '2018-05-17T07:00:00',
          closeTime: '2018-05-17T18:59:00',
          flag: 0,
          restaurantProfile: null
        },
        {
          id: 79,
          restaurantId: null,
          day: 'Saturday',
          openTime: '2018-05-17T07:00:00',
          closeTime: '2018-05-18T06:59:00',
          flag: 0,
          restaurantProfile: null
        }]
      }
    },
    toggleIsEdit () {
      this.isEdit = !this.isEdit
    },
    toggleEditDisplayName () {
      this.editDisplayName = !this.editDisplayName
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
.restaurant-profile-tab-contents {
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
</style>
