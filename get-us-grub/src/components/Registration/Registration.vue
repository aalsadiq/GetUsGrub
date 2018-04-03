<template>
  <div>
    <app-header/>
    <app-create-user/>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import AppCreateUser from '@/components/CreateUser'
export default {
  name: 'AdminHome',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'app-create-user': AppCreateUser
  }
}
</script>
