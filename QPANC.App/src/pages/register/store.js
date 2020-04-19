import { factory } from '@toby.mosque/utils'
import { Notify } from 'quasar'

class RegisterPageModel {
  constructor ({
    userName = '',
    confirmUserName = '',
    firstName = '',
    lastName = '',
    password = '',
    confirmPassword = ''
  } = {}) {
    this.userName = userName
    this.confirmUserName = confirmUserName
    this.firstName = firstName
    this.lastName = lastName
    this.password = password
    this.confirmPassword = confirmPassword
  }
}

const options = {
  model: RegisterPageModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    },
    async register ({ state }) {
      await this.$axios.post('/Auth/Register', state)
      Notify.create({
        color: 'positive',
        message: this.$t('register.success')
      })
      this.$router.push('/login')
    }
  }
})

export { options, RegisterPageModel }
