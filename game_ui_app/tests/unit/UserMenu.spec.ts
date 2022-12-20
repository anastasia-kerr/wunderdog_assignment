import Vue from 'vue'
import UserMenu from '@/components/UserMenu.vue'
import { shallowMount, mount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'
import { RootState } from '@/store/types'
import { RoundResult } from '@/api/types'
import Vuex, { Store } from 'vuex'

const localVue = createLocalVue()
localVue.use(Vuex)
Vue.use(Vuetify)

describe('UserMenu', () => {
  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  let store: Store<RootState>
  const player1 = 'player1'
  const state = {
    gameId: '1',
    error: '',
    nickname: player1,
    roundNumber: 0,
    currentPlayers: [],
    gameWinner: '',
    roundResults: {} as RoundResult
  }
  beforeEach(() => {
    vuetify = new Vuetify()
    store = new Vuex.Store({
      state
    })
    wrapper = shallowMount(UserMenu, { localVue, vuetify, store })
  })
  it('renders correctly', () => {
    expect(wrapper.element).toMatchSnapshot()
  })
  it('Must display player name', () => {
    expect(wrapper.text()).not.toContain(player1)
    const dialog = wrapper.find('#nickname_dialog')
    expect(dialog.exists()).toBeFalsy()
  })

  it('Must have a way to leave the game', () => {
    const btn = wrapper.find('#leave_button')
    expect(btn.exists()).toBeTruthy()
  })

  it('If nickname is not provided must ask nickname before continuing', () => {
    store = new Vuex.Store({
      state: { ...state, nickname: '' }
    })
    wrapper = mount(UserMenu, { localVue, vuetify, store })
    const dialog = wrapper.find('#nickname_dialog')
    expect(dialog.exists()).toBe(true)
  })
})
