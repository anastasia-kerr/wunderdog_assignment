<template>
  <v-container>
    <v-row no-gutters>
      <v-col v-for="gesture in availableGestures" :key="gesture.text">
        <v-card
          :color="isGestureSelected(gesture) ? 'primary' : 'grey'"
          :elevation="isGestureSelected(gesture) ? '1' : '6'"
          @click="() => selectGesture(gesture)"
          :max-width="maxCardSize"
          :min-height="maxCardSize"
          :max-height="maxCardSize"
        >
          <v-card-text>
            <h1>{{ gesture.text }}</h1>
            <img :src="gesture.img" class="pt-2" :alt="gesture.text" />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col class="mb-4">
        <v-btn
          id="play_game_button"
          :loading="submittingGesture"
          :disabled="isBtnDisabled"
          :color="isBtnDisabled ? 'secondary' : 'primary'"
          fab
          @click="playRound"
        >
          <v-icon> mdi-play </v-icon>
        </v-btn>
        <round-result-dialog v-if="showResult" @closed="showResult = false" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue'
import config from '../configs'
import RoundResultDialog from './dialogs/RoundResultDialog.vue'

export default Vue.extend({
  name: 'GamePlay',
  components: { RoundResultDialog },
  data: () => ({
    maxCardSize: 250,
    imgWidth: 200,
    btnLabel: 'Start game',
    submittingGesture: false,
    showResult: false,
    selectedGesture: {} as { value?: number },
    availableGestures: config.availableGestures
  }),
  computed: {
    isBtnDisabled (): boolean {
      return this.selectedGesture.value === undefined
    }
  },
  methods: {
    isGestureSelected (gesture: any): boolean {
      return this.selectedGesture === gesture
    },
    selectGesture (gesture: any): void {
      this.selectedGesture = gesture
    },
    async playRound (): Promise<void> {
      if (this.isBtnDisabled) return
      this.submittingGesture = true
      await this.$store.dispatch('placeGesture', { gesture: this.selectedGesture.value })
      this.submittingGesture = false
      this.showResult = true
      this.selectedGesture = {}
    }
  }
})
</script>
