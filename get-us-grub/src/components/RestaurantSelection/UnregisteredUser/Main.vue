<template>
  <div>
    <!-- Main page of the restaurant selector -->
    <select-restaurant/>
  </div>
</template>

<script>
import SelectRestaurant from '@/components/RestaurantSelection/SelectRestaurant'

export default {
  name: 'RestaurantSelectionMain',
  // Adding other Vue components dependencies
  components: {
    SelectRestaurant
  }
}
</script>
