import { mount } from 'vue-test-utils'
import expect from 'expect'
import CreateUser from '@/components/CreateUser'
import moxios from 'moxios'

describe('CreateUser.vue', () => {
  let wrapper

  beforeEach (() => {
    wrapper = mount(CreateUser, {
      propsData: {
        username: 'username'
      }
    })
  })

  it.only('updates request after submitting', () => {
    type('typedusername', 'input[name=username]')

    click('#userSubmit')

    see('typedusername')
  })
})
