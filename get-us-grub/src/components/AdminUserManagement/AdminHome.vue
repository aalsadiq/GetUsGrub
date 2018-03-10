<template>
    <div>
      <app-admin-header></app-admin-header>
        <app-footer></app-footer>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  }
}
</script>
