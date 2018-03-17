<template>
  <div>
      <h1> Image Upload </h1>
      <v-flex xs5 sm5 offset-sm3>
          <v-card dark>
              <br/>
            <img id ="uploadPreview" style="width: 300px; height: 300px;"/>
            <br/>
            <input multiple id = "uploadImage" type = "file" name ="uploadNewImage" onchange ="PreviewImage();"/>
            <input id = "subtmitImage" type="submit" name="upload_btn" value ="upload" SubmitImageUpload>
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
  }),
  methods: {
    PreviewImage : function () {
        var oFReader = new FileReader();
        oFReader.readAsDataURL(document.getElementById("uploadImage").files[0]);

        oFReader.onload = function (oFREvent) {
            document.getElementById("uploadPreview").src = oFREvent.target.result;
        };
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