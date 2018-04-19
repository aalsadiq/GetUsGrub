<template>
  <div id="image-upload">
      <v-flex xs5 sm5 offset-sm3>
        <h1> Image Upload </h1>
          <v-card dark>
            <br/>
              <input id="uploadImage" ref="imageData" type="file" @change="previewImage" accept="image/*"/>
              <v-btn id="subtmitImage" color="pink" type="submit" name="upload_btn" value ="upload" v-on:click="SubmitImageUpload">Upload Image</v-btn>
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
    profileImage: ''
  }),
  methods: {
    SubmitImageUpload: function () {
      axios.post('http://localhost:8081/Profile/User/Edit/ProfileImageUpload', { // formData,
        headers: {
          'content-type': 'multipart/form-data'
        },
        data: {
          profileImage: this.imageData
        }
      }).then(response => {
        this.responseDataStatus = 'Success! Image has been uploaded.'
        this.responseData = response.data
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
      console.log(this.$refs.imageData.value)
    }
  }
}
// var formData = new FormData()
// formData.append('file', imageData[0])

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
