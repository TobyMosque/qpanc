import { factory } from '@toby.mosque/utils'
import jwtDecode from 'jwt-decode'
class AppStoreModel {
  constructor ({
    token = '',
    localeOs = '',
    localeUser = ''
  } = {}) {
    this.token = token
    this.localeOs = localeOs
    this.localeUser = localeUser
  }
}

const options = {
  model: AppStoreModel
}

const roleClaimName = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'

export default factory.store({
  options,
  getters: {
    decoded (state) {
      if (!state.token) {
        return undefined
      }
      return jwtDecode(state.token)
    },
    expireAt (state, getters) {
      if (!getters.decoded || !getters.decoded.exp) {
        return undefined
      }
      const expiration = getters.decoded.exp * 1000
      return new Date(expiration)
    },
    roles (state, getters) {
      if (!getters.decoded || !getters.decoded.exp) {
        return []
      }
      const roles = getters.decoded[roleClaimName]
      if (Array.isArray(roles)) {
        return roles
      } else {
        return [roles]
      }
    },
    isOnRoles (state, getters) {
      return (roles) => {
        return getters.roles.some(role => roles.includes(role))
      }
    },
    isLogged (state, getters) {
      return function () {
        const now = new Date()
        return getters.expireAt && getters.expireAt > now
      }
    },
    detectLocale () {

    },
    locale (state) {
      return state.localeUser || state.localeOs
    }
  }
})

export { options, AppStoreModel }
