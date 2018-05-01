import moxios from 'moxios'
import axios from 'axios'

describe('Mocking axios requests for Image Upload', function () {
  describe('Image Upload Tests', function () {
    beforeEach(function () {
      moxios.install()
    })

    afterEach(function () {
      moxios.uninstall()
    })

    it.only('ImageUpload-Post', function (done) {
      moxios.withMock(function () {
        let onFulfilled = sinon.spy()
        axios.put('/User/Profile/ImageUpload').then(onFulfilled)

        moxios.wait(function () {
          let request = moxios.requests.mostRecent()
          request.respondWith({
            status: 200,
            response: {
              username: 'Admin1',
              imgPath: 'C:\Users\Angelica\Documents\GetUsGrub\get-us-grub\src\assets\logo.png'
            }
          }).then(function () {
            done()
          })
        })
      })
    }),
    it('ImageUpload-Put', function (done) {
      moxios.withMock(function () {
        let onFulfilled = sinon.spy()
        axios.put('/User/ImageUpload').then(onFulfilled)

        moxios.wait(function () {
          let request = moxios.requests.mostRecent()
          request.respondWith({
            status: 200,
            response: {
              username: 'NotBilly'
            }
          }).then(function () {
            done()
          })
        })
      })
    })
  })
})
