import Vue from 'vue'
import GamePlay from '@/components/GamePlay.vue'
import { mount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'

const localVue = createLocalVue()

describe('GamePlay', () => {
  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  beforeEach(() => {
    vuetify = new Vuetify()
    wrapper = mount(GamePlay, { localVue, vuetify })
  })
  it('Must offer three gesture options', () => {
    expect(wrapper.text()).toContain('Rock')
    expect(wrapper.text()).toContain('Paper')
    expect(wrapper.text()).toContain('Scissors')
  })
  it('Must have a play button', () => {
    const btn = wrapper.find('#play_game_button')
    expect(btn.exists()).toBeTruthy()
  })
})
