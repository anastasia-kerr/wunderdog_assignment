import Vue from 'vue'
import NicknameDialog from '@/components/dialogs/NicknameDialog.vue'
import { shallowMount, createLocalVue, Wrapper } from '@vue/test-utils'
import Vuetify from 'vuetify'

const localVue = createLocalVue()
Vue.use(Vuetify)

describe('NicknameDialog', () => {
  let vuetify: Vuetify
  let wrapper:Wrapper<Vue>
  beforeEach(() => {
    vuetify = new Vuetify()
    wrapper = shallowMount(NicknameDialog, { localVue, vuetify })
  })
  it('renders correctly', () => {
    expect(wrapper.element).toMatchSnapshot()
  })
})
