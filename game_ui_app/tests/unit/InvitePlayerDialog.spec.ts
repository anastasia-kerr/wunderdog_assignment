import Vue from 'vue'
import InvitePlayerDialog from '@/components/dialogs/InvitePlayerDialog.vue'
import { shallowMount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'
import router from '../../src/router'

const localVue = createLocalVue()
Vue.use(Vuetify)

describe('InvitePlayerDialog', () => {
  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  beforeEach(() => {
    vuetify = new Vuetify()
    wrapper = shallowMount(InvitePlayerDialog, {
      router,
      localVue,
      vuetify
    })
  })
  it('renders correctly', () => {
    expect(wrapper.element).toMatchSnapshot()
  })
})
