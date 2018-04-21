<template>
  <div id="image-upload">
        <v-dialog  v-model="dialog">
          <v-btn color="primary" dark slot="activator">Open Dialog</v-btn>
          <v-card dark>
            {{ responseDataStatus }}
            {{ responseData }}
            <br/>
            <v-flex xs1 sm4 offset-sm1>
              <v-flex sm2>
              <input id="uploadImage" name="imaageInput" ref="imageData" type="file" @change="StoreSelectedFile" accept="image/*"/>
              </v-flex>
              <div v-if="show===true">
                 <v-text-field label="Menu Name" v-model="menuItem" required />
                 <v-text-field label="Menu Item" v-model="itemName" required />
            </div>
              <v-btn id="subtmitImage" name= "submitButton" color="pink" type="submit" value ="upload" v-on:click="SubmitImageUpload">Upload Image</v-btn>
              </v-flex>
              <v-flex xs20 sm35 offset-sm5>
                <img id="uploadPreview" :src="imageData" alt="ProfileImage"/>
              </v-flex>
            <br/>
          </v-card>
        </v-dialog>
          <br/>
  </div>
</template>

<script>
import axios from 'axios'
import jwt from 'jsonwebtoken'
export default {
  name: 'ImageHome',
  dialog: false,
  components: {
  },
  data: () => ({
    username: '26user', // Grab from store
    menuItem: '', // User input // menuName1
    itemName: '', // User input // itemName1
    selectedFile: null,
    responseDataStatus: '',
    responseData: '',
    test: null,
    show: false // true for Restaurant and false for Profile
  }),
  methods: {
    beforeCreate () {
      if (this.$store.state.authenticationToken === null) {
        this.$router.push({path: '/Unauthorized'})
      }
      try {
        if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
          this.show = false
        } else if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantProfile === 'True') {
          this.show = true
        } else {
          this.$router.push({path: '/Forbidden'})
        }
      } catch (ex) {
        this.$router.push({path: '/Forbidden'})
      }
    },
    StoreSelectedFile: function (event) {
      this.selectedFile = event.target.files[0]
    },
    SubmitImageUpload: function () {
      // ReadRestaurantProfile
      var formData = new FormData()
      if (this.show === false) {
        formData.append('username', this.username)
        formData.append('filename', this.selectedFile, this.selectedFile.name)
        axios.post('http://localhost:8081/Profile/User/Edit/ProfileImageUpload', formData, {
        }).then(response => {
          this.responseDataStatus = 'Success! Image has been uploaded.'
          this.responseData = response.data
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
      if (this.show === true) {
        formData.append('username', this.username)
        formData.append('menuItem', this.menuItem)
        formData.append('itemName', this.itemName)
        formData.append('filename', this.selectedFile, this.selectedFile.name)
        axios.post('http://localhost:8081/Profile/Restaurant/Edit/MenuItemImageUpload', formData, {
        }).then(response => {
          this.responseDataStatus = 'Success! Image has been uploaded.'
          this.responseData = response.data
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
    },
    ValidateImageType: function () {
    },
    ValidateImageSize: function () {
    }
  }
}
</script>

<style>
#image-upload{
  height: 40em;
  width: 80em;
}
#uploadPreview{
  height: 100px;
  width: 100px;
}
</style>
