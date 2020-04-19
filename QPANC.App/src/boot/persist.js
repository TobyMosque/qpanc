// import something here
import { Cookies } from 'quasar'
import createPersistedState from 'vuex-persistedstate'
import { detectLocale } from 'services/locales'

const persistState = function ({ name, store, storage }) {
  createPersistedState({
    key: name,
    paths: [name],
    filter ({ type }) {
      return type.startsWith(name)
    },
    storage
  })(store)
}

export default function ({ store, ssrContext }) {
  const cookies = process.env.SERVER
    ? Cookies.parseSSR(ssrContext)
    : Cookies

  const cookieStorage = {
    getItem (key) {
      return JSON.stringify(cookies.get(key))
    },
    setItem (key, value) {
      cookies.set(key, value, { path: '/' })
    },
    removeItem (key) {
      cookies.remove(key, { path: '/' })
    }
  }

  persistState({ name: 'app', store, storage: cookieStorage })
  if (process.env.CLIENT) {
    // persistState({ name: 'local', store, storage: window.localStorage })
    store.commit('app/localeOs', detectLocale())
  }
}
