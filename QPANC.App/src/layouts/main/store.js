import { factory } from '@toby.mosque/utils'

class MainLayoutModel {
  constructor ({
    leftDrawerOpen = false
  } = {}) {
    this.leftDrawerOpen = leftDrawerOpen
  }
}

const options = {
  model: MainLayoutModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    },
    async logout ({ commit }) {
      await this.$axios.delete('/Auth/Logout')
      commit('app/token', undefined, { root: true })
      this.$router.push('/login')
    }
  }
})

export { options, MainLayoutModel }
