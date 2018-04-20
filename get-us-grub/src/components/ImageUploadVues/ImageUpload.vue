<template>
  <div id="image-upload">
      <!-- <v-flex xs5 sm5 offset-sm3> -->
        {{ responseDataStatus }}
        {{ responseData }}
        <v-dialog  v-model="dialog">
          <v-btn color="primary" dark slot="activator">Open Dialog</v-btn>
          <v-card dark>
            <br/>
              <input id="uploadImage" name="imaageInput" ref="imageData" type="file" @change="StoreSelectedFile" accept="image/*"/>
              <v-btn id="subtmitImage" name= "submitButton" color="pink" type="submit" value ="upload" v-on:click="SubmitImageUpload(viewType)">Upload Image</v-btn> <!-- formData -->
              <!-- <img id="uploadPreview" :src="imageData" alt="ProfileImage"/> -->
            <br/>
          </v-card>
        </v-dialog>
          <br/>
      <!-- </v-flex> -->
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'ImageHome',
  props: ['viewType'],
  dialog: false,
  components: {
  },
  data: () => ({
    username: 'username26',
    menuItem: 'menuName1',
    itemName: 'itemName1',
    selectedFile: null,
    responseDataStatus: '',
    test: null
  }),
  methods: {
    StoreSelectedFile: function (event) {
      this.selectedFile = event.target.files[0]
    },
    SubmitImageUpload: function (viewType) {
      var formData = new FormData()
      if (viewType === 'profileImageupload') {
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
      if (viewType === 'menuItemImageUpload') {
        formData.append('username', this.username)
        formData.append('menuItem', this.menuItem)
        formData.append('itemName', this.itemName)
        formData.append('filename', this.selectedFile, this.selectedFile.name)
        axios.post('http://localhost:8081/Profile/User/Edit/MenuItemImageUpload', formData, {
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
