<template>
  <div>
    <app-create-user/>
  </div>
</template>

<script>
import AppCreateUser from '@/components/CreateUser'
export default {
  name: 'AdminCreate',
  components: {
    'app-create-user': AppCreateUser
  },
}
</script>
