import { factory } from '@toby.mosque/utils'

class LoginPageModel {
  constructor ({
    userName = '',
    password = ''
  } = {}) {
    this.userName = userName
    this.password = password
  }
}

const options = {
  model: LoginPageModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    },
    forget ({ state }) {
      console.log('forget: not implemented yet')
    },
    async login ({ commit, state, rootGetters }) {
      const { data: token } = await this.$axios.post('/Auth/Login', state)
      commit('app/token', token, { root: true })
      this.$router.push('/home')
      console.log(rootGetters['app/decoded'])
    }
  }
})

export { options, LoginPageModel }
