import moxios from 'moxios'

it('testing deactivate user', () => {
  moxios.stubRequest('/User/DeactivateUser', {
    status: 200,
    response: {
      username: 'Billy'
    }
  })
})

it('testing deactivate user - post', () => {
  moxios.stubRequest('/User/DeactivateUser', {
    status: 200,
    response: {
      username: 'NotBilly'
    }
  })
})
