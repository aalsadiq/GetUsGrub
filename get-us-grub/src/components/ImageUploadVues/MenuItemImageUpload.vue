<template>
    <div id = 'menu-item-upload'>
      <h1> Menu Item Upload </h1>
      <app-image-upload :viewType="submitType"/>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppImageUpload from '@/components/ImageUploadVues/ImageUpload'
export default {
  name: 'ProfileImageUpload',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-image-upload': AppImageUpload
  },
  data: () => ({
    submitType: 'menuItemImageUpload'
  })
}
</script>
