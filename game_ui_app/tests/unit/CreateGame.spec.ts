import { RoundResult } from '@/api/types'
import Vue from 'vue'
import CreateGame from '@/components/CreateGame.vue'
import { RootState } from '@/store/types'
import { mount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'
import Vuex, { Store } from 'vuex'

const localVue = createLocalVue()
localVue.use(Vuex)
Vue.use(Vuetify)

describe('CreateGame', () => {
  let store: Store<RootState>
  let mutations: { resetGameState: any }

  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  beforeEach(() => {
    vuetify = new Vuetify()
    mutations = {
      resetGameState: jest.fn()
    }
    store = new Vuex.Store({
      state: {
        gameId: '1',
        error: '',
        roundNumber: 0,
        currentPlayers: [],
        gameWinner: '',
        roundResults: {} as RoundResult
      },
      mutations
    })

    wrapper = mount(CreateGame, { store, localVue, vuetify })
  })
  it('Must reset the state', () => {
    expect(mutations.resetGameState).toHaveBeenCalled()
  })
  it('Create must be disabled until all fields are complete', () => {
    const btn = wrapper.find('.v-btn')
    expect(btn.exists()).toBe(true)
    expect(btn.attributes().disabled).toBe('disabled')
  })
  it('Create must be disabled until all fields are complete', async () => {
    const btn = wrapper.find('.v-btn')

    const input = wrapper.find('#create_game__nickname_input')
    expect(input.exists()).toBe(true)
    await input.setValue('player 1')
    expect(btn.attributes().disabled).toBeFalsy()
  })
})
