<template>
  <v-dialog :width="dialogSize" persistent v-model="dialog">
    <v-card v-if="showLoader" :loading="true">
      <v-card-text>
        <div class="mt-2">{{ waitingLbl }}</div>
      </v-card-text>
    </v-card>
    <v-card v-else>
      <v-card-title v-text="headerRoundLabel" />
      <v-card-text>
        <v-row>
          <v-col> {{ roundResultMessage }} </v-col>
        </v-row>
      </v-card-text>
        <v-card-title v-if="isLastRound" v-text="headerGameLabel" />
        <v-card-text v-if="isLastRound">
          <v-row>
            <v-col> {{ gameResultMessage }} </v-col>
          </v-row>
        </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn color="primary" v-text="btnLabel" @click="onBtnPress" />
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapState } from 'vuex'
import config from '../../configs'
const { smallDialogSize } = config

export default Vue.extend({
  name: 'RoundResult',
  data: () => ({
    dialog: true,
    dialogSize: smallDialogSize,
    headerRoundLabel: 'Round results',
    headerGameLabel: 'Game results'
  }),
  computed: {
    ...mapGetters(['opponent', 'roundResultMessage', 'gameResultMessage']),
    ...mapState(['roundResults']),
    showLoader (): boolean {
      return !this.roundResults.id
    },
    waitingLbl (): string {
      return this.opponent
        ? `Waiting for ${this.opponent} to make their move`
        : 'Waiting for an opponent to join the game'
    },
    isLastRound (): boolean {
      return this.roundResults.isLastRound === true
    },
    btnLabel (): string {
      return this.isLastRound ? 'Start new game' : 'Play another round'
    }
  },
  watch: {
    opponent: {
      async handler (newValue) {
        if (!newValue) {
          await this.$store.dispatch('setCurrentPlayers')
        } else {
          await this.$store.dispatch('setCurrentRoundResults')
        }
      },
      immediate: true
    }
  },
  methods: {
    async onBtnPress (): Promise<void> {
      if (this.isLastRound) {
        this.$router.push({ name: 'create-game' })
      } else {
        await this.$store.dispatch('joinCurrentRound')
        this.dialog = false
        this.$emit('closed')
      }
    }
  }
})
</script>
