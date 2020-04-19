import axios from 'axios'
import inject from './inject'
import { Notify } from 'quasar'

export default inject(({ store, router }) => {
  const instance = axios.create({
    baseURL: process.env.API_URL
  })

  instance.interceptors.request.use((config) => {
    const token = store.state.app.token
    const locale = store.getters['app/locale']
    config.headers['Accept-Language'] = locale
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  }, (error) => {
    return Promise.reject(error)
  })

  instance.interceptors.response.use((response) => {
    return response
  }, (error) => {
    const { status } = error.response || {}
    let message = store.$t('http.generic')
    switch (status) {
      case 400: message = store.$t('http.badRequest'); break
      case 401: message = store.$t('http.unauthorized'); break
      case 403: message = store.$t('http.forbidden'); break
      case 422:
        if (error.response.data) {
          const { title, errors } = error.response.data
          message = title
          message += '<ul>'
          for (const key in errors) {
            const error = errors[key]
            for (const msg of error) {
              message += '<li>' + msg + '</li>'
            }
          }
          message += '</ul>'
          store.$root.$emit('unprocessable', errors)
        } else {
          message = store.$t('http.unprocessable')
        }
        break
      case 500: message = store.$t('http.serverError'); break
      case 503: message = store.$t('http.serviceUnavailable'); break
    }
    Notify.create({
      type: 'negative',
      html: true,
      message: message
    })
    if (status === 401) {
      store.commit('app/token', undefined)
      router.push('/login')
    }
    return Promise.reject(error)
  })

  return {
    axios: instance
  }
})
