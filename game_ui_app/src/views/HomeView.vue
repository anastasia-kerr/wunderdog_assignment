<template>
  <landing-page />
</template>

<script lang="ts">
import Vue from 'vue'
import LandingPage from '../components/LandingPage.vue'

export default Vue.extend({
  name: 'HomePage',

  components: {
    LandingPage
  }
})
</script>
