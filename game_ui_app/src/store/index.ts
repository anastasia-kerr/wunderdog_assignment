import { RoundResult } from '@/api/types'
import Vue from 'vue'
import Vuex from 'vuex'
import { RootState } from './types'
import getters from './getters'
import mutations from './mutations'
import actions from './actions'

Vue.use(Vuex)
export const state: RootState = {
  gameId: '',
  error: '',
  roundNumber: 0,
  currentPlayers: [] as string[],
  gameWinner: '',
  nickname: localStorage.getItem('nickname'),
  playerIdentifier: localStorage.getItem('playerIdentifier'),
  roundResults: {} as RoundResult
}

export default new Vuex.Store({
  state,
  getters,
  mutations,
  actions
})
