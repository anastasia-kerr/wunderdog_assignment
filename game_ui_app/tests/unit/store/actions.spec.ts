import { RootState } from '../../../src/store/types'
import { state } from '../../../src/store/index'
import mutations from '../../../src/store/mutations'
import actions from '../../../src/store/actions'
import * as api from '../../../src/api'
import { createLocalVue } from '@vue/test-utils'
import Vuex, { Store } from 'vuex'
import { GetGameResultResponse } from '@/api/types'
import { Gestures } from '@/types'
jest.mock('../../../src/api')

describe('Actions', () => {
  const nickname = 'test'
  const mockApi = api as jest.Mocked<typeof api>
  let store: Store<RootState>

  beforeEach(() => {
    jest.clearAllMocks()
    const localVue = createLocalVue()
    localVue.use(Vuex)
    store = new Vuex.Store({
      state,
      mutations,
      actions
    })
  })
  describe('setUser', () => {
    beforeEach(() => {
      jest.spyOn(Storage.prototype, 'setItem')
    })

    it('When setting user must set store to persist aftr app is terminated', async () => {
      await store.dispatch('setUser', nickname)
      expect(store.state.nickname).toBe(nickname)
      expect(localStorage.setItem).toHaveBeenCalled()
    })
  })
  describe('leaveGame', () => {
    it('When leaveGame must clear the store, and reset the localStorage', async () => {
      jest.spyOn(Storage.prototype, 'setItem')

      await store.dispatch('leaveGame')
      expect(api.leaveCurrentRound).toHaveBeenCalled()
      expect(store.state.nickname).toBe('')
      expect(localStorage.setItem).toHaveBeenCalled()
    })
  })
  describe('joinCurrentRound', () => {
    beforeAll(() => {
      mockApi.joinCurrentRound.mockImplementationOnce(
        (): Promise<number> => Promise.resolve(1)
      )
    })
    it('When joinCurrentRound must set round number', async () => {
      await store.dispatch('setUser', nickname)

      await store.dispatch('joinCurrentRound')
      expect(api.joinCurrentRound).toHaveBeenCalled()
      expect(store.state.roundNumber).toBe(1)
    })
    it('When joinCurrentRound must make sure user is set, otherwise do not call the api', async () => {
      await store.dispatch('setUser', '')

      try {
        return await store.dispatch('joinCurrentRound')
      } catch (err:any) {
        expect(err.message).toBe('Player must have some identifier or name')

        expect(api.joinCurrentRound).not.toHaveBeenCalled()
      }
    })
    it('When joinCurrentRound must make sure any previous results are cleared', async () => {
      const errorText = 'some error text'
      await store.dispatch('setUser', nickname)
      store.commit('setError', errorText)
      store.commit('setGameResult', nickname)
      expect(store.state.error).toBe(errorText)
      expect(store.state.gameWinner).toBe(nickname)

      await store.dispatch('joinCurrentRound')
      expect(store.state.error).not.toBe(errorText)
      expect(store.state.gameWinner).not.toBe(nickname)
    })
  })
  describe('setGameResult', () => {
    beforeAll(() => {
      mockApi.getGameResult.mockImplementationOnce(
        (): Promise<GetGameResultResponse> => Promise.resolve(
          {
            succeeded: true,
            errors: [],
            result: { winnerNickname: nickname }
          })
      )
    })
    it('When setGameResult must set game winner nickname number', async () => {
      await store.dispatch('setGameResult')

      expect(api.getGameResult).toHaveBeenCalled()
      expect(store.state.gameWinner).toBe(nickname)
    })
  })
  describe('placeGesture', () => {
    it('When user is not set up then no call is made to placeGesture', async () => {
      await store.dispatch('setUser', '')

      await store.dispatch('placeGesture', { gesture: Gestures.Paper })

      expect(api.placeGesture).not.toHaveBeenCalled()
    })

    it('When placeGesture must call the api placeGesture', async () => {
      await store.dispatch('setUser', nickname)

      await store.dispatch('placeGesture', { gesture: Gestures.Paper })

      expect(api.placeGesture).toHaveBeenCalledWith(
        {
          gameId: store.state.gameId,
          playerIdentifier: store.state.playerIdentifier,
          playerNickname: nickname,
          roundGesture: Gestures.Paper
        })
    })
  })
})
