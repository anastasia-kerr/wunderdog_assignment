import Vue from 'vue'
import LandingPage from '@/components/LandingPage.vue'
import { mount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'

const localVue = createLocalVue()

describe('LandingPage', () => {
  let wrapper:Wrapper<Vue>
  beforeEach(() => {
    const vuetify = new Vuetify()

    wrapper = mount(LandingPage, { localVue, vuetify })
  })
  it('Must have a button to create a game', () => {
    const btn = wrapper.find('#start_game_button')

    expect(btn.exists()).toBe(true)
    expect(btn.text()).toContain('Start new game')
  })
  it('renders correctly', () => {
    expect(wrapper.element).toMatchSnapshot()
  })
})
