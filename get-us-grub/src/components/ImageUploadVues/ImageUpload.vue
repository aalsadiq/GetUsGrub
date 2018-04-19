<template>
  <div id="image-upload">
      <v-flex xs5 sm5 offset-sm3>
        {{ responseData }}
        <h1> Image Upload </h1>
          <v-card dark>
            <br/>
              <input id="uploadImage" name="imaageInput" ref="imageData" type="file" @change="StoreSelectedFile" accept="image/*"/>
              <v-btn id="subtmitImage" name= "submitButton" color="pink" type="submit" value ="upload" v-on:click="SubmitImageUpload">Upload Image</v-btn> <!-- formData -->
              <!-- <img id="uploadPreview" :src="imageData" alt="ProfileImage"/> -->
            <br/>
          </v-card>
          <br/>
      </v-flex>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'ImageHome',
  components: {
  },
  data: () => ({
    username: '',
    selectedFile: null
  }),
  methods: {
    StoreSelectedFile: function (event) {
      this.selectedFile = event.target.files[0]
    },
    SubmitImageUpload: function () {
      var formData = new FormData()
      formData.append('myfile', this.selectedFile, this.selectedFile.name)
      axios.post('http://localhost:8081/Profile/User/Edit/ProfileImageUpload', formData, { // formData,
        // headers: {
        //   'content-type': 'multipart/form-data'
        // },
        // data: {
        //   profileImage: this.$refs.dataform
        //   profileImage: file
        // }
      }).then(response => {
        this.responseDataStatus = 'Success! Image has been uploaded.'
        this.responseData = response.data
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
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
