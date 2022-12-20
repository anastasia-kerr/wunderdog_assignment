import { RoundResult } from '@/api/types'
import Vue from 'vue'
import RoundResultDialog from '@/components/dialogs/RoundResultDialog.vue'
import { RootState } from '@/store/types'
import { mount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'
import Vuex, { Store } from 'vuex'

const localVue = createLocalVue()
localVue.use(Vuex)

describe('RoundResultDialog', () => {
  let store: Store<RootState>
  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  const player1 = 'player1'
  const player2 = 'player2'
  const roundNumber = 1
  const state = {
    gameId: '1',
    error: '',
    roundNumber: roundNumber,
    currentPlayers: [player1, player2],
    gameWinner: '',
    nickname: player1,
    playerIdentifier: player1 + 'playerIdentifier',
    roundResults: { id: 1, isLastRound: true } as RoundResult
  }
  let actions
  let getters
  const roundResultMessage = 'roundResultMessage'
  const gameResultMessage = 'gameResultMessage'
  beforeEach(() => {
    vuetify = new Vuetify()
    actions = {
      setCurrentRoundResults: jest.fn(),
      setCurrentPlayers: jest.fn()
    }
    getters = {
      opponent: () => player2,
      roundResultMessage: () => roundResultMessage,
      gameResultMessage: () => gameResultMessage
    }
    store = new Vuex.Store({
      state: { ...state },
      actions,
      getters
    })

    wrapper = mount(RoundResultDialog, { store, localVue, vuetify })
  })
  it('renders correctly', () => {
    expect(wrapper.element).toMatchSnapshot()
  })
  it('Round is finsihed Must Display roundResultMessage', () => {
    expect(wrapper.text()).toContain(roundResultMessage)
  })
  it('Game is finsihed Must Display gameResultMessage', () => {
    expect(wrapper.text()).toContain(gameResultMessage)
  })
})
