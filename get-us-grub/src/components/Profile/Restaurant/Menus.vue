<template>
  <div id="menus-div">
    <div>
    <!-- Dialog popup for adding/editing menus -->
    <v-dialog v-model="menuDialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formMenuTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form v-model="valid">
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                    label="Menu Name"
                    v-model="editedMenu.menuName"
                    :rules="$store.state.rules.menuNameRules"
                    required>
                  </v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-switch
                    :label="`Active: ${editedMenu.isActive.toString()}`"
                    v-model="editedMenu.isActive"
                    required>
                  </v-switch>
                </v-flex>
              </v-layout>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="closeMenuDialog">Cancel</v-btn>
          <v-btn color="blue darken-1" flat @click.native="saveMenu" :disabled="!valid">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
    <div>
    <!-- Dialog popup for adding/editing menu items -->
    <v-dialog v-model="menuItemDialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formMenuItemTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form v-model="valid">
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field label="Menu Name" v-model="formMenuName" disabled></v-text-field>
                </v-flex>
                <v-flex xs>
                  <v-text-field
                    label="Item Name"
                    v-model="editedMenuItem.itemName"
                    :rules="$store.state.rules.itemNameRules"
                    required>
                  </v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field
                    label="Item Price"
                    v-model="editedMenuItem.itemPrice"
                    :rules="$store.state.rules.itemPriceRules"
                    required>
                  </v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field
                    label="Tag"
                    v-model="editedMenuItem.tag"
                    :rules="$store.state.rules.tagRules"
                    required>
                  </v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field
                    label="Description"
                    v-model="editedMenuItem.description">
                  </v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-switch
                    :label="`Active: ${editedMenuItem.isActive.toString()}`"
                    v-model="editedMenuItem.isActive"
                    required>
                  </v-switch>
                </v-flex>
              </v-layout>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="closeMenuItemDialog">Cancel</v-btn>
          <v-btn color="blue darken-1" flat @click.native="saveMenuItem" :disabled="!valid">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
    <!-- Portion dealing with active menus -->
    <div id="active-menus">
    <v-layout row justify-center>
      <!-- Tabs to click on an active menu -->
      <v-flex xs8>
      <v-toolbar color="teal" dark tabs>
        <v-spacer/>
        <v-toolbar-title class="menus-title"><span v-if="isEdit">Active</span> Menus</v-toolbar-title>
        <v-spacer/>
        <div v-if="isEdit">
          <v-btn dark slot="activator" class="mb-2" icon @click="addMenu()">
            <v-icon>add_circle</v-icon>
          </v-btn>
        </div>
        <v-tabs
          color="teal"
          slot="extension"
          v-model="activeTab"
          fixed-tabs
          show-arrows
          prev-icon="chevron_left"
          next-icon="chevron_right"
        >
          <v-tabs-slider color="yellow"></v-tabs-slider>
          <v-tab v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
            {{ menu.restaurantMenu.menuName }}
            <!-- Buttons on the active menu tab -->
            <div v-if="isEdit">
              <v-layout>
                <v-btn icon class="mx-0" @click="editMenu(menu)">
                  <v-icon color="yellow">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteMenu(menu)">
                  <v-icon color="grey">delete</v-icon>
                </v-btn>
                <v-btn dark slot="activator" class="mb-2" icon @click="addMenuItem(menuIndex)">
                  <v-icon>add_circle</v-icon>
                </v-btn>
              </v-layout>
            </div>
          </v-tab>
        </v-tabs>
      </v-toolbar>
      <!-- Binding the which active menu tab user clicks on to activeTab variable -->
      <v-tabs-items v-model="activeTab">
        <v-tab-item v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
          <div
            style="max-height: 600px;"
            class="scroll-y"
            id="scrolling-techniques"
          >
          <v-card flat>
          <v-list three-line>
            <!-- Active Menu's menu items -->
            <template v-for="(item, itemIndex) in restaurantMenusList[menuIndex].menuItem">
            <!-- An active menu item of an active menu -->
            <v-list-tile
              id="active-menu-active-menu-item"
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="item.isActive && item.flag !== 3"
              >
              <!-- Picture of active menu item for an active menu -->
              <v-list-tile-avatar size="50">
                <img :src="item.itemPicture">
              </v-list-tile-avatar>
              <!-- Content of active menu item for an active menu -->
              <v-list-tile-content class="active-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <!-- Buttons on the active menu item of an active menu -->
              <div v-if="isEdit">
                <v-layout>
                  <!-- Menu item image upload component button -->
                <v-flex>
                   <menu-image-upload v-if="item.flag !== 1" :menuItemId="item.id" id="menu-items-image-upload"/>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
                </v-flex>
                </v-layout>
              </div>
            </v-list-tile>
            <!-- An inactive menu item of an active menu -->
            <v-list-tile
              id="active-menu-inactive-menu-item"
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="!item.isActive && isEdit && item.flag !== 3"
              >
              <!-- Picture of inactive menu item for an active menu -->
              <v-list-tile-avatar size="50" class="inactive-avatar">
                <img :src="item.itemPicture">
              </v-list-tile-avatar>
              <!-- Content of inactive menu item for an active menu -->
              <v-list-tile-content class="inactive-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <!-- Buttons on the inactive menu item of an active menu -->
              <div v-if="isEdit">
                <v-layout>
                <!-- Menu item image upload component button -->
                <v-flex>
                <menu-image-upload v-if="item.flag !== 1" :menuItemId="item.id" id="menu-items-image-upload"/>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
                </v-flex>
                </v-layout>
              </div>
            </v-list-tile>
            <!-- The line underneath the menu item content -->
            <v-divider
              v-if="itemIndex !== restaurantMenusList[menuIndex].menuItem.length
                    && isEdit"
              :key="itemIndex"
            >
              </v-divider>
            <v-divider
              v-if="itemIndex !== restaurantMenusList[menuIndex].menuItem.length
                    && !isEdit
                    && restaurantMenusList[menuIndex].menuItem[itemIndex].isActive"
              :key="itemIndex"
            >
            </v-divider>
            </template>
          </v-list>
          </v-card>
          </div>
        </v-tab-item>
      </v-tabs-items>
    </v-flex>
  </v-layout>
  </div>
  <!-- Portion dealing with inactive menus -->
  <div id="inactive-menus" v-if="isEdit">
    <v-layout row justify-center>
      <v-flex xs8>
      <!-- Tabs to click on an inactive menu -->
      <v-toolbar color="teal" dark tabs>
        <v-spacer/>
        <v-toolbar-title class="menus-title">Inactive Menus</v-toolbar-title>
        <v-spacer/>
        <div v-if="isEdit">
          <v-btn dark slot="activator" class="mb-2" icon @click="addMenu()">
            <v-icon>add_circle</v-icon>
          </v-btn>
        </div>
        <v-tabs
          color="teal"
          slot="extension"
          v-model="inactiveTab"
          fixed-tabs
          show-arrows
          prev-icon="chevron_left"
          next-icon="chevron_right"
        >
          <v-tabs-slider color="yellow"></v-tabs-slider>
          <v-tab v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="!menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
            {{ menu.restaurantMenu.menuName }}
            <!-- Buttons on the inactive menu tab -->
            <div v-if="isEdit">
              <v-layout>
              <v-btn icon class="mx-0" @click="editMenu(menu, menuIndex)">
                <v-icon color="yellow">edit</v-icon>
              </v-btn>
              <v-btn icon class="mx-0" @click="deleteMenu(menu)">
                <v-icon color="grey">delete</v-icon>
              </v-btn>
              <v-btn dark slot="activator" class="mb-2" icon @click="addMenuItem(menuIndex)">
                <v-icon>add_circle</v-icon>
              </v-btn>
              </v-layout>
            </div>
          </v-tab>
        </v-tabs>
      </v-toolbar>
      <!-- Binding the which active menu tab user clicks on to activeTab variable -->
      <v-tabs-items v-model="inactiveTab">
        <v-tab-item v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="!menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
          <div
            style="max-height: 600px;"
            class="scroll-y"
            id="scrolling-techniques"
          >
          <v-card flat>
          <v-list three-line>
            <!-- Inactive Menu's menu items -->
            <template v-for="(item, itemIndex) in restaurantMenusList[menuIndex].menuItem">
            <!-- An active menu item of an inactive menu -->
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="item.isActive && item.flag !== 3"
              >
              <!-- Picture of active menu item for an inactive menu -->
              <v-list-tile-avatar size="50">
                <img :src="item.itemPicture">
              </v-list-tile-avatar>
              <!-- Content of active menu item for an inactive menu -->
              <v-list-tile-content class="active-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <!-- Buttons on the active menu item of an inactive menu -->
              <div v-if="isEdit">
                <v-layout>
                <!-- Menu item image upload component button -->
                  <v-flex>
                   <menu-image-upload v-if="item.flag !== 1" :menuItemId="item.id" id="menu-items-image-upload"/>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
                </v-flex>
                </v-layout>
              </div>
            </v-list-tile>
            <!-- An inactive menu item of an inactive menu -->
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="!item.isActive && isEdit && item.flag !== 3"
              >
              <!-- Picture of inactive menu item for an inactive menu -->
              <v-list-tile-avatar size="50" class="inactive-avatar">
                <img :src="item.itemPicture">
              </v-list-tile-avatar>
              <!-- Content of inactive menu item for an inactive menu -->
              <v-list-tile-content class="inactive-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <!-- Buttons on the active menu item of an inactive menu -->
              <div v-if="isEdit">
                <v-layout>
                <!-- Menu item image upload component button -->
                  <v-flex>
                   <menu-image-upload v-if="item.flag !== 1" :menuItemId="item.id" id="menu-items-image-upload"/>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                </v-flex>
                <v-flex>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
                </v-flex>
                </v-layout>
              </div>
            </v-list-tile>
            <!-- The line underneath the menu item content -->
            <v-divider
              v-if="itemIndex !== restaurantMenusList[menuIndex].menuItem.length
                    && isEdit"
              :key="itemIndex"
            >
            </v-divider>
            <v-divider
              v-if="itemIndex !== restaurantMenusList[menuIndex].menuItem.length
                    && !isEdit
                    && restaurantMenusList[menuIndex].menuItem[itemIndex].isActive"
              :key="itemIndex"
            >
            </v-divider>
            </template>
          </v-list>
          </v-card>
          </div>
        </v-tab-item>
      </v-tabs-items>
    </v-flex>
  </v-layout>
  </div>
  </div>
</template>

<script>
import MenuItemImageUpload from '@/components/ImageUploadVues/MenuItemUpload'
export default {
  components: {
    'menu-image-upload': MenuItemImageUpload
  },
  // Props are variables passed down by parent component
  props: [
    'restaurantMenusList',
    'isEdit'
  ],
  data () {
    return {
      menuItemId: 0,
      valid: false,
      newMenu: {
        restaurantMenu: null,
        menuItem: []
      },
      defaultNewMenu: {
        restaurantMenu: null,
        menuItem: []
      },
      activeTab: null,
      inactiveTab: null,
      menuDialog: false,
      menuItemDialog: false,
      formMenuName: 'hi',
      formMenuIndex: null,
      editedIndex: -1,
      editedMenu: {
        menuName: '',
        isActive: true,
        flag: 0
      },
      defaultMenu: {
        menuName: '',
        isActive: true,
        flag: 0
      },
      editedMenuItem: {
        id: 0,
        itemName: '',
        itemPrice: null,
        itemPicture: '',
        tag: '',
        description: '',
        isActive: true,
        flag: 0
      },
      defaultMenuItem: {
        id: 0,
        itemName: '',
        itemPrice: null,
        itemPicture: '',
        tag: '',
        description: '',
        isActive: true,
        flag: 0
      }
    }
  },
  computed: {
    formMenuTitle () {
      return this.editedIndex === -1 ? 'New Menu' : 'Edit Menu'
    },
    formMenuItemTitle () {
      return this.editedIndex === -1 ? 'New Menu Item' : 'Edit Menu Item'
    }
  },
  methods: {
    addMenu () {
      this.editedIndex = -1
      this.menuDialog = true
    },
    editMenu (menu, index) {
      this.editedIndex = index
      // Assign a new object of menu.restaurantMenu to editedMenu
      this.editedMenu = Object.assign({}, menu.restaurantMenu)
      this.menuDialog = true
    },
    deleteMenu (menu) {
      const index = this.restaurantMenusList.indexOf(menu)
      if (confirm('Are you sure you want to delete this menu?')) {
        try {
          // If the menu has been newly added during that session and user wants to delete it
          if (this.restaurantMenusList[index].restaurantMenu.flag === 1) {
            // Then delete the menu from the list entirely
            this.restaurantMenusList.splice(index, 1)
            // Else set the flag to delete in order to be deleted in the backend database
          } else {
            this.restaurantMenusList[index].restaurantMenu.flag = 3
          }
        } catch (ex) {
          this.restaurantMenusList[index].restaurantMenu.flag = 3
        }
      }
    },
    closeMenuDialog () {
      this.menuDialog = false
      setTimeout(() => {
        this.editedMenu = Object.assign({}, this.defaultMenu)
        this.editedIndex = -1
      }, 300)
    },
    saveMenu () {
      // If the item has been edited
      if (this.editedIndex > -1) {
        // If the flag is not a newly added menu during the user's session
        if (this.restaurantMenusList[this.editedIndex].restaurantMenu.flag !== 1) {
          // Set the flag to edit so that the backend can handle it as an edited item
          this.restaurantMenusList[this.editedIndex].restaurantMenu.flag = 2
        }
        this.restaurantMenusList[this.editedIndex].restaurantMenu.menuName = this.editedMenu.menuName
        this.restaurantMenusList[this.editedIndex].restaurantMenu.isActive = this.editedMenu.isActive
      // Else item is newly added
      } else {
        this.editedMenu.flag = 1
        this.newMenu.restaurantMenu = this.editedMenu
        this.restaurantMenusList.push(this.newMenu)
        this.newMenu = Object.assign({}, this.defaultNewMenu)
      }
      this.closeMenuDialog()
    },
    addMenuItem (menuIndex) {
      this.newMenu = Object.assign({}, this.defaultNewMenu)
      this.editedMenuItem = Object.assign({}, this.defaultMenuItem)
      this.formMenuIndex = menuIndex
      this.formMenuName = this.restaurantMenusList[menuIndex].restaurantMenu.menuName
      this.editedIndex = -1
      this.menuItemDialog = true
    },
    editMenuItem (menuIndex, item) {
      this.newMenu = Object.assign({}, this.defaultNewMenu)
      this.formMenuIndex = menuIndex
      this.formMenuName = this.restaurantMenusList[menuIndex].restaurantMenu.menuName
      this.editedIndex = this.restaurantMenusList[menuIndex].menuItem.indexOf(item)
      this.editedMenuItem = Object.assign({}, item)
      this.menuItemDialog = true
    },
    deleteMenuItem (menuIndex, item) {
      const index = this.restaurantMenusList[menuIndex].menuItem.indexOf(item)
      if (confirm('Are you sure you want to delete this menu item?')) {
        try {
          // If the menu item has been newly added during that session and user wants to delete it
          if (this.restaurantMenusList[menuIndex].menuItem[index].flag === 1) {
            // Then delete the menu from the list entirely
            this.restaurantMenusList[menuIndex].menuItem.splice(index, 1)
          } else {
            // Else set the flag to delete in order to be deleted in the backend database
            this.restaurantMenusList[menuIndex].menuItem[index].flag = 3
          }
        } catch (ex) {
          this.restaurantMenusList[menuIndex].menuItem[index].flag = 3
        }
      }
    },
    closeMenuItemDialog () {
      this.menuItemDialog = false
      setTimeout(() => {
        this.editedMenuItem = Object.assign({}, this.defaultMenuItem)
        this.editedIndex = -1
      }, 300)
    },
    saveMenuItem () {
      this.newMenu = Object.assign({}, this.defaultNewMenu)
      // If the item has been edited
      if (this.editedIndex > -1) {
        // If the flag is not a newly added menu item during the user's session
        if (this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].flag !== 1) {
          // Set the flag to edit so that the backend can handle it as an edited item
          this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].flag = 2
        }
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemName = this.editedMenuItem.itemName
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemPrice = this.editedMenuItem.itemPrice
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemPicture = this.editedMenuItem.itemPicture
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].tag = this.editedMenuItem.tag
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].description = this.editedMenuItem.description
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].isActive = this.editedMenuItem.isActive
        this.editedMenuItem = Object.assign({}, this.defaultMenuItem)
      // Else item is newly added
      } else {
        this.editedMenuItem.flag = 1
        this.editedMenuItem.itemPicture = this.$store.state.menuItemImagePath
        this.restaurantMenusList[this.formMenuIndex].menuItem.push(this.editedMenuItem)
        this.editedMenuItem = Object.assign({}, this.defaultMenuItem)
      }
      this.closeMenuItemDialog()
    },
    toggle () {}
  }
}
</script>

<style scoped>
.menus-title {
  margin: auto;
}
.active-menu-item {
  margin: 2em 1em 2em 1em;
}
.inactive-menu-item {
  margin: 2em 1em 2em 1em;
  opacity: 0.2;
}
.inactive-avatar {
  opacity: 0.2;
}
#inactive-menus {
  margin: 2em 0 0 0;
}
#image-upload[data-v-dd1103d4] {
  height: 0;
  width: 0;
}
#menu-items-image-upload {
  padding: 0.7em 0.7em 0 0;
}
</style>
