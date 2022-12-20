<template>
  <v-form id="create_game" ref="form" v-model="valid">
    <v-container>
      <v-card class="mb-4">
        <v-card-text>
          <v-row class="text-center">
            <v-col>
              <v-text-field
                id="create_game__nickname_input"
                :rules="nicknameRules"
                :label="nicknameLabel"
                outlined
                v-model="nickname"
              />
            </v-col>
            <v-col :cols="2">
              <v-text-field
                id="create_game__round_input"
                :min="0"
                :rules="roundsRules"
                :label="roundsLabel"
                outlined
                type="number"
                v-model="rounds"
              />
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
      <v-row class="text-center">
        <v-col class="mb-4">
          <v-btn
            id="create_game__button"
            :disabled="isBtnDisabled"
            @click="onBtnPress"
            color="primary"
            x-large
            fab
          >
            <v-icon> mdi-play </v-icon>
          </v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script lang="ts">
import Vue from 'vue'
import { createGame } from '../api'
import { nameIsRequired, roundIsRequired } from '../validators'

export default Vue.extend({
  name: 'CreateGame',

  data: () => ({
    valid: false,
    nickname: localStorage.getItem('nickname') || '',
    nicknameLabel: 'Nickname',
    nicknameRules: [nameIsRequired],
    roundsLabel: 'Rounds',
    rounds: 3,
    roundsRules: [roundIsRequired],
    btnLabel: 'Start game'
  }),
  created () {
    this.$store.commit('resetGameState')
  },
  computed: {
    isBtnDisabled (): boolean {
      return !this.nickname || !this.rounds
    }
  },
  methods: {
    async onBtnPress (): Promise<void> {
      this.validateForm()
      if (!this.valid) return
      const result = await createGame({
        nickname: this.nickname,
        numberOfRounds: this.rounds
      })
      const gameId = result.toString()
      await this.$store.dispatch('setUser', this.nickname)
      this.$store.commit('setGameId', gameId)

      this.$router.push({ name: 'game', params: { id: gameId } })
    },
    validateForm (): void {
      const form = this.$refs.form as Vue & { validate: any }
      form.validate()
    }
  }
})
</script>
