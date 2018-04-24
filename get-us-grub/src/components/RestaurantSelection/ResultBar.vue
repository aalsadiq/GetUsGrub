<template>
  <div>
    <!-- Title bar for the restaurant selection -->
    <v-alert class="result-bar" :value=true>
      <span class="result-text">
        Search Result
      </span>
    </v-alert>
  </div>
</template>

<style>
.result-bar {
  background-color: #e08a8a !important;
  margin: 2em 0em 0em 0em;
}
.result-text {
  color: rgb(255, 255, 255);
  font-weight: bold;
  font-size: 1.5em;
}
</style>
