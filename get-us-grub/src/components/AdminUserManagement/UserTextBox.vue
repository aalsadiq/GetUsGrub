<template>
    <div>
      <v-app id="inputUser">
          <v-container fluid>
            <v-spacer/>
              <v-flex  xs5 sm2 offset-sm5>
                <h2>Input User Name </h2>
                <v-form v-model="validIdentificationInput">
                  <v-text-field label="username" v-model="username" :rules="usernameRules" required />
                </v-form>
              </v-flex>
              <v-btn id ="submit-button" color="warning" v-onclick="userSubmit">Submit</v-btn>
          </v-container>
        </v-app>
    </div>
</template>

<script>
export default {
  name: 'UserTextBox',
  data: () => ({
    validIdentificationInput: false,
    username: '',
    usernameRules: [
      username => !!username || 'Username is required',
      username => /^[A-Za-z\d]+$/.test(v) || 'Username must contain only letters and numbers'
    ]
  })
}
</script>
