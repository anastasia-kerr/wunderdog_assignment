<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="display-2 text-center font-weight-bold mb-3">
          {{ header }}
        </h1>
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="red--text" v-text="subtitle" />
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col class="mb-4">
        <v-btn id="start_game_button" color="primary" to="/create-game" v-text="startLabel" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  name: 'LandingPage',

  data: () => ({
    header: "Welcome to Anastasiia Kerr's assignment for Wunderdog",
    subtitle: 'ROCK PAPER SCISSORS',
    startLabel: 'Start new game'
  })
})
</script>
