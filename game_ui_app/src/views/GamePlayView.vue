<template>
  <div class="text-center">
    <div v-if="error">
      <v-alert border="top" color="red lighten-2" dark>Ooups Something went wrong</v-alert>
      <v-btn color="primary" to="/create-game" v-text="startLabel" />
    </div>
    <span v-else>
      <app-bar />
      <v-spacer />
      <v-row class="ma-3"  v-if="showCardLoaded">
          <v-col><v-skeleton-loader type="image"/></v-col>
          <v-col><v-skeleton-loader type="image"/></v-col>
          <v-col><v-skeleton-loader type="image"/></v-col>
     </v-row>
      <game-play v-else/>
    </span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapState } from 'vuex'
import AppBar from '../components/AppBar.vue'
import GamePlay from '../components/GamePlay.vue'

export default Vue.extend({
  name: 'GamePlayView',
  components: { AppBar, GamePlay },
  data: () => ({
    startLabel: 'Start new game'
  }),
  computed: {
    ...mapState(['nickname', 'roundNumber', 'error']),
    showCardLoaded ():boolean {
      return !this.roundNumber
    }
  },
  watch: {
    nickname: {
      async handler (newValue) {
        if (!newValue) return
        await this.joinCurrentRound()
      },
      immediate: true
    }
  },
  methods: {
    async joinCurrentRound () {
      await this.$store.dispatch('joinCurrentRound')
    }
  }
})
</script>
