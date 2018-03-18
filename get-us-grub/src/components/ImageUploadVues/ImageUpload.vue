<template>
  <div id="image-upload">
      <v-flex xs5 sm5 offset-sm3>
        <h1> Image Upload </h1>
          <v-card dark>
              <br/>
            <img id ="uploadPreview" style="width: 300px; height: 300px;"/>
            <br/>
            <input multiple id= "uploadImage" type = "file" name ="uploadNewImage" onchange ="PreviewImage();" accept =".png"/>
            <v-btn id = "subtmitImage" color ="pink" type="submit" name="upload_btn" value ="upload" v-onclick ="SubmitImageUpload">Upload Image </v-btn>
          </v-card>
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
    image: '',
    username: ''
  }),
  methods: {
    PreviewImage: function () {
    },
    SubmitImageUpload () {
      axios.put('http://localhost:8081/User/Admin/EditUser', {
        username: this.username
      }).then(response => {
        this.responseDataStatus = 'Success! User has been created: '
        this.responseData = response.data
        console.log(response)
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
    }
  }
}
</script>

<style>
#image-upload{
  height: 30em;
  width: 80em;
}
</style>
