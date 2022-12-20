import { ActionTree } from 'vuex'
import { RootState } from './types'
import { leaveCurrentRound, joinCurrentRound, getCurrentRoundPlayers, getRoundResults, getGameResult, placeGesture } from '@/api'
import { poll } from '@/api/helpers'

const actions: ActionTree<RootState, RootState> = {
  setUser (context, nickname: string) {
    context.commit('setNickname', nickname)
    localStorage.setItem('nickname', nickname ?? '')
    localStorage.setItem('playerIdentifier', nickname ? nickname + Date.now().toString() : '')
  },
  async leaveGame ({ state, dispatch }) {
    if (state.playerIdentifier) {
      await leaveCurrentRound({ gameId: state.gameId, playerIdentifier: state.playerIdentifier })
    }
    dispatch('setUser', '')
  },
  async joinCurrentRound ({ state, commit, dispatch }) {
    if (!state.playerIdentifier || !state.nickname) {
      throw new Error('Player must have some identifier or name')
    }

    try {
      const roundNumber = await joinCurrentRound({
        gameId: state.gameId,
        playerNickname: state.nickname,
        playerIdentifier: state.playerIdentifier
      })

      commit('resetGameState')
      commit('setRoundNumber', roundNumber)

      dispatch('setCurrentPlayers')
    } catch (error : any) {
      commit('setError', error?.response?.data?.Errors)
    }
  },
  async setCurrentPlayers ({ commit, state }) {
    return await poll(async () => {
      const result = await getCurrentRoundPlayers(state.gameId)
      if (result.length === 2) {
        commit('setCurrentPlayers', result)
        return true
      } else if (state.currentPlayers.length === 2) {
        return true
      }
      return false
    })
  },
  async setCurrentRoundResults ({ state, commit, dispatch }) {
    return await poll(async () => {
      const response = await getRoundResults(state.gameId, state.roundNumber)
      if (response.succeeded) {
        commit('setCurrentRoundResults', response.result)
        if (response.result.isLastRound) {
          dispatch('setGameResult')
        }
        return true
      }
      return false
    })
  },
  async setGameResult ({ commit, state }) {
    const response = await getGameResult(state.gameId)
    commit('setGameResult', response.result.winnerNickname)
  },
  async placeGesture (context, payload: { gesture: number }) {
    if (context.state.playerIdentifier && context.state.nickname) {
      await placeGesture({
        gameId: context.state.gameId,
        playerNickname: context.state.nickname,
        playerIdentifier: context.state.playerIdentifier,
        roundGesture: payload.gesture
      })
    }
  }
}
export default actions
