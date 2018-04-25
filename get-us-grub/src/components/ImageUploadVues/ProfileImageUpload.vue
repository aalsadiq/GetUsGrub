<template>
  <div id="image-upload">
        <v-dialog  v-model="dialog">
          <v-btn color="dark" dark slot="activator">Upload Image</v-btn>
          <v-flex >
          <v-card dark>
            {{ responseDataStatus }}
            {{ responseData }}
            <br/>
            <v-flex  xs4 sm2 md1>
              <v-flex sm2>
              <input id="uploadImage" name="imageInput" ref="imageData" type="file" @change="StoreSelectedFile" accept="image/*"/>
              <v-btn id="submitImage" name= "submitButton" color="pink" type="submit" value ="upload" v-on:click="SubmitImageUpload">Upload Image</v-btn>
              </v-flex>
              </v-flex>
              <v-flex xs15 sm15 offset-sm2>
                  <img id="previewImage" class="preview" :src="imageData" height="100" width="100"/>
              </v-flex>
            <br/>
          </v-card>
          </v-flex>
        </v-dialog>
      <br/>
  </div>
</template>

<script>
import axios from 'axios'
// import jwt from 'jsonwebtoken'
export default {
  name: 'ImageHome',
  dialog: false,
  components: {
  },
  data: () => ({
    selectedFile: null,
    responseDataStatus: '',
    responseData: '',
    test: null,
    username: 'username16',
    imageData: '' // Stores in base 64 format of image
  }),
  methods: {
    // beforeCreate () {
    //   if (this.$store.state.authenticationToken === null) {
    //     this.$router.push({path: '/Unauthorized'})
    //   }
    //   try {
    //     if (jwt.decode(this.$store.state.authenticationToken).ReadUser === 'True') {
    //     } else {
    //       this.$router.push({path: '/Forbidden'})
    //     }
    //   } catch (ex) {
    //     this.$router.push({path: '/Forbidden'})
    //   }
    // },
    StoreSelectedFile: function (event) {
      this.selectedFile = event.target.files[0]
      this.previewImage(event)
    },
    previewImage: function (event) {
      var input = event.target // References the DOM input element
      if (input.files[0]) {
        var reader = new FileReader() // Read image and convert to base64
        reader.onload = (image) => {
          this.imageData = image.target.result // Read image as base64
        }
        reader.readAsDataURL(input.files[0]) // Read as data url (base64 format)
      }
    },
    SubmitImageUpload: function () {
      // ReadRestaurantProfile
      var formData = new FormData()
      formData.append('username', this.username) // this.$store.state.username
      formData.append('filename', this.selectedFile, this.selectedFile.name)
      axios.post('http://localhost:8081/Profile/User/Edit/ProfileImageUpload', formData, {
      }).then(response => {
        this.responseDataStatus = 'Success! Image has been uploaded.'
        this.responseData = response.data
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
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
