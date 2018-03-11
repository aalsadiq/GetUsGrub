<template>
  <div>
    <app-admin-header/>
    <app-create-user/>
    <app-footer/>
  </div>
</template>

<script>
import AppCreateUser from '@/components/CreateUser'
import AdminHeader from '@/components/AdminHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'CreateUser',
  components: {
    'app-create-user': AppCreateUser,
    'app-admin-header':AdminHeader,
    'app-footer' : AppFooter
  }
}
</script>
