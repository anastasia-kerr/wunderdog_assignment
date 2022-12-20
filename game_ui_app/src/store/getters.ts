import config from '../configs'
import { GetterTree } from 'vuex'
import { RootState } from './types'

const getters: GetterTree<RootState, RootState> = {
  opponent (state) {
    return state.currentPlayers.find(player => player !== state.nickname)
  },
  roundResultMessage (state) {
    if (state.roundResults.isDraw) {
      return 'No Winners, it\'s a draw'
    }
    const winningGesture = config.availableGestures.find(g => g.value === state.roundResults.winnerGesture)
    const losingGesture = config.availableGestures.find(g => g.value === state.roundResults.loserGesture)
    return `${winningGesture?.text} beats ${losingGesture?.text}! ${state.roundResults.winnerNickname} is the winner of the round`
  },
  gameResultMessage (state) {
    if (!state.roundResults.isLastRound) {
      return ''
    }
    return state.gameWinner ? `${state.gameWinner} is the winner of the game!` : 'It\'s a draw, there is no winner'
  }
}
export default getters
