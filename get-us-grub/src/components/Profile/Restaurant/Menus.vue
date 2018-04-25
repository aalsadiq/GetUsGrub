<template>
  <div id="menus-div">
    {{ restaurantMenusList }}
    <div>
    <v-dialog v-model="menuDialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formMenuTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Menu Name" v-model="editedMenu.menuName" required></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                 <v-switch
                    :label="`Active: ${editedMenu.isActive.toString()}`"
                    v-model="editedMenu.isActive"
                    required
                  ></v-switch>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="closeMenuDialog">Cancel</v-btn>
          <v-btn color="blue darken-1" flat @click.native="saveMenu">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
    <div>
    <v-dialog v-model="menuItemDialog" persistent max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ formMenuItemTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Menu Name" v-model="formMenuName" disabled></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Item Name" v-model="editedMenuItem.itemName" required></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Item Price" v-model="editedMenuItem.itemPrice" required></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Tag" v-model="editedMenuItem.tag"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Description" v-model="editedMenuItem.description"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                 <v-switch
                    :label="`Active: ${editedMenuItem.isActive.toString()}`"
                    v-model="editedMenuItem.isActive"
                    required
                  ></v-switch>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click.native="closeMenuItemDialog">Cancel</v-btn>
          <v-btn color="blue darken-1" flat @click.native="saveMenuItem">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
    <div id="active-menus">
    <v-layout row justify-center>
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
            <div v-if="isEdit">
              <v-btn icon class="mx-0" @click="editMenu(menu)">
                <v-icon color="yellow">edit</v-icon>
              </v-btn>
              <v-btn icon class="mx-0" @click="deleteMenu(menu)">
                <v-icon color="grey">delete</v-icon>
              </v-btn>
              <v-btn dark slot="activator" class="mb-2" icon @click="addMenuItem(menuIndex)">
                <v-icon>add_circle</v-icon>
              </v-btn>
            </div>
          </v-tab>
        </v-tabs>
      </v-toolbar>
      <v-tabs-items v-model="activeTab">
        <v-tab-item v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
          <div
            style="max-height: 600px;"
            class="scroll-y"
            id="scrolling-techniques"
          >
          <v-card flat>
          <v-list three-line>
            <template v-for="(item, itemIndex) in restaurantMenusList[menuIndex].menuItem">
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="item.isActive && item.flag !== 3"
              >
              <v-list-tile-avatar size="50">
                <img :src="require('@/assets/GetUsGrub.png')">
              </v-list-tile-avatar>
              <v-list-tile-content class="active-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <div v-if="isEdit">
                <!-- Button to call Image Upload's function -->
                <v-btn icon class="mx-0">
                  <v-icon color="blue">photo_camera</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </div>
            </v-list-tile>
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="!item.isActive && isEdit && item.flag !== 3"
              >
              <v-list-tile-avatar size="50" class="inactive-avatar">
                <img :src="require('@/assets/GetUsGrub.png')">
              </v-list-tile-avatar>
              <v-list-tile-content class="inactive-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <div v-if="isEdit">
                <!-- Button to call Image Upload's function -->
                <v-btn icon class="mx-0">
                  <v-icon color="blue">photo_camera</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </div>
            </v-list-tile>
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
  <div id="inactive-menus" v-if="isEdit">
    <v-layout row justify-center>
      <v-flex xs8>
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
            <div v-if="isEdit">
              <v-btn icon class="mx-0" @click="editMenu(menu)">
                <v-icon color="yellow">edit</v-icon>
              </v-btn>
              <v-btn icon class="mx-0" @click="deleteMenu(menu)">
                <v-icon color="grey">delete</v-icon>
              </v-btn>
              <v-btn dark slot="activator" class="mb-2" icon @click="addMenuItem(menuIndex)">
                <v-icon>add_circle</v-icon>
              </v-btn>
            </div>
          </v-tab>
        </v-tabs>
      </v-toolbar>
      <v-tabs-items v-model="inactiveTab">
        <v-tab-item v-for="(menu, menuIndex) in restaurantMenusList" :key="menuIndex" v-if="!menu.restaurantMenu.isActive && menu.restaurantMenu.flag !== 3">
          <div
            style="max-height: 600px;"
            class="scroll-y"
            id="scrolling-techniques"
          >
          <v-card flat>
          <v-list three-line>
            <template v-for="(item, itemIndex) in restaurantMenusList[menuIndex].menuItem">
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="item.isActive && item.flag !== 3"
              >
              <v-list-tile-avatar size="50">
                <img :src="require('@/assets/GetUsGrub.png')">
              </v-list-tile-avatar>
              <v-list-tile-content class="active-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <div v-if="isEdit">
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </div>
            </v-list-tile>
            <v-list-tile
              @click="toggle()"
              avatar
              ripple
              :key="item.id"
              v-if="!item.isActive && isEdit && item.flag !== 3"
              >
              <v-list-tile-avatar size="50" class="inactive-avatar">
                <img :src="require('@/assets/GetUsGrub.png')">
              </v-list-tile-avatar>
              <v-list-tile-content class="inactive-menu-item">
                <v-list-tile-title>{{ item.itemName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">${{ item.itemPrice }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.description }}</v-list-tile-sub-title>
                <v-list-tile-action-text>#{{ item.tag }}</v-list-tile-action-text>
              </v-list-tile-content>
              <div v-if="isEdit">
                <v-btn icon class="mx-0" @click="editMenuItem(menuIndex, item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteMenuItem(menuIndex, item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </div>
            </v-list-tile>
            <v-divider
              v-if="itemIndex !== restaurantMenusList[menuIndex].menuItem.length"
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
export default {
  props: [
    'restaurantMenusList',
    'isEdit'
  ],
  data () {
    return {
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
        itemName: '',
        itemPrice: 0,
        itemPicture: '',
        tag: '',
        description: '',
        isActive: true,
        flag: 0
      },
      defaultMenuItem: {
        itemName: '',
        itemPrice: 0,
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
    editMenu (menu) {
      this.editedIndex = this.restaurantMenusList.indexOf(menu)
      this.editedMenu = Object.assign({}, menu.restaurantMenu)
      this.menuDialog = true
    },
    deleteMenu (menu) {
      const index = this.restaurantMenusList.indexOf(menu)
      if (confirm('Are you sure you want to delete this menu?')) {
        try {
          if (this.restaurantMenusList[index].restaurantMenu.flag === 1) {
            this.restaurantMenusList.splice(index, 1)
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
      if (this.editedIndex > -1) {
        if (this.restaurantMenusList[this.editedIndex].restaurantMenu.flag !== 1) {
          this.restaurantMenusList[this.editedIndex].restaurantMenu.flag = 2
        }
        this.restaurantMenusList[this.editedIndex].restaurantMenu.menuName = this.editedMenu.menuName
        this.restaurantMenusList[this.editedIndex].restaurantMenu.isActive = this.editedMenu.isActive
      } else {
        this.editedMenu.flag = 1
        this.newMenu = Object.assign({}, this.defaultNewMenu)
        this.newMenu.restaurantMenu = this.editedMenu
        this.restaurantMenusList.push(this.newMenu)
      }
      this.closeMenuDialog()
    },
    addMenuItem (menuIndex) {
      this.formMenuIndex = menuIndex
      this.formMenuName = this.restaurantMenusList[menuIndex].restaurantMenu.menuName
      this.editedIndex = -1
      this.menuItemDialog = true
    },
    editMenuItem (menuIndex, item) {
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
          if (this.restaurantMenusList[menuIndex].menuItem[index].flag === 1) {
            this.restaurantMenusList[menuIndex].menuItem.splice(index, 1)
          } else {
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
      if (this.editedIndex > -1) {
        if (this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].flag !== 1) {
          this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].flag = 2
        }
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemName = this.editedMenuItem.itemName
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemPrice = this.editedMenuItem.itemPrice
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].itemPicture = this.editedMenuItem.itemPicture
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].tag = this.editedMenuItem.tag
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].description = this.editedMenuItem.description
        this.restaurantMenusList[this.formMenuIndex].menuItem[this.editedIndex].isActive = this.editedMenuItem.isActive
      } else {
        this.editedMenuItem.flag = 1
        this.restaurantMenusList[this.formMenuIndex].menuItem.push(this.editedMenuItem)
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
</style>
