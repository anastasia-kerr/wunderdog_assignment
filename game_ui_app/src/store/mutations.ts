import { MutationTree } from 'vuex'
import { RootState } from './types'

const mutations: MutationTree<RootState> = {
  setGameId (state, id: string) {
    state.gameId = id
  },
  setError (state, error: string) {
    state.error = error
  },
  resetGameState (state) {
    state.currentPlayers = []
    state.roundResults = {}
    state.roundNumber = 0
    state.gameWinner = ''
    state.error = ''
  },
  setGameResult (state, gameWinner:string) {
    state.gameWinner = gameWinner
  },
  setNickname (state, nickname: string) {
    state.nickname = nickname
    state.playerIdentifier = nickname + Date.now().toString()
  },
  setRoundNumber (state, roundNumber: number) {
    state.roundNumber = roundNumber
  },
  setCurrentPlayers (state, currentPlayers: string[]) {
    state.currentPlayers = currentPlayers
  },
  setCurrentRoundResults (state, result:any) {
    state.roundResults = result
  }
}
export default mutations
