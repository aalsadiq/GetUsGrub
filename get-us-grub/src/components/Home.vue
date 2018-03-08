<template>
    <div>
        <app-header></app-header>
        <app-footer></app-footer>
    </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'Home',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  }
}
</script>
