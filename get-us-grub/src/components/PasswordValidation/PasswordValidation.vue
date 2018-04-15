<script>
import axios from 'axios'
import sha1 from 'sha1'

export default {
  name: 'PasswordValidation',
  components: {},
  methods: {
    validate (password) {
      var hash = sha1(password)
      var anonHash = hash.substring(0, 5)
      var promise = axios.get('https://api.pwnedpasswords.com/range/' + anonHash
      ).then(response => {
        var data = response.data
        var lines = data.split('\n')

        for (var i = 0; i < lines.length; i++) {
          var line = lines[i].split(':')

          if ((anonHash + line[0]).toLowerCase() !== hash) {
            continue
          }

          var count = line[1]

          if (count >= 100) {
            return ({
              isValid: false,
              message: 'Your password has been found in multiple breaches. You may not use this password. For more information, visit HaveIBeenPwned.com'
            })
          } else if (count !== 0) {
            return ({
              isValid: true,
              message: 'Your password was previously found in a breach. We HIGHLY recommend you change your password. For more information, visit HaveIBeenPwned.com'
            })
          }
        }

        return ({
          isValid: true,
          message: []
        })
      })
      return promise
    }
  }
}
</script>
