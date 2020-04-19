import validations from 'services/validations'
import { factory } from '@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'page-login'
export default factory.page({
  name: 'LoginPage',
  options,
  moduleName,
  storeModule: store,
  created () {
    if (process.env.CLIENT) {
      this.$root.$on('unprocessable', this.unprocessable)
    }
  },
  destroy () {
    if (process.env.CLIENT) {
      this.$root.$off('unprocessable', this.unprocessable)
    }
  },
  data () {
    const self = this
    const validation = validations(self, {
      userName: ['required', 'email', 'server'],
      password: ['required', 'server']
    })
    return {
      validation,
      validationArgs: {
        userName: {
          server: true
        },
        password: {
          server: true
        }
      }
    }
  },
  methods: {
    unprocessable (errors) {
      switch (true) {
        case !!errors.UserName: this.validationArgs.userName.server = errors.UserName[0]; break
        case !!errors.Password: this.validationArgs.password.server = errors.Password[0]; break
      }
      this.$refs.form.validate()
    },
    forget () {
      this.$store.dispatch(`${moduleName}/forget`)
    },
    async login () {
      this.validation.resetServer()
      const isValid = await this.$refs.form.validate()
      if (isValid) {
        this.$store.dispatch(`${moduleName}/login`)
      }
    }
  }
})
