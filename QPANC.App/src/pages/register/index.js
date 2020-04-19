import validations from 'services/validations'
import { factory } from '@toby.mosque/utils'
import store, { options } from './store'
import zxcvbn from 'zxcvbn'

const moduleName = 'page-register'
export default factory.page({
  name: 'RegisterPage',
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
      confirmUserName: ['required', 'compare', 'email'],
      firstName: ['required'],
      lastName: ['required'],
      password: ['required', 'strength', 'server'],
      confirmPassword: ['required', 'compare']
    })
    return {
      validation,
      validationArgs: {
        userName: {
          server: true
        },
        confirmUserName: {
          compare: 'userName'
        },
        password: {
          server: true,
          strength: 'strength'
        },
        confirmPassword: {
          compare: 'password'
        }
      }
    }
  },
  computed: {
    strength () {
      if (!this.password) {
        return 0
      }
      const strength = zxcvbn(this.password).score + 1
      return strength / 5
    },
    strColor () {
      switch (this.strength) {
        case 1: return 'blue'
        case 0.8: return 'green'
        case 0.6: return 'yellow'
        case 0.4: return 'orange'
        case 0.2: return 'red'
        default: return 'grey'
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
    async register () {
      this.validation.resetServer()
      const isValid = await this.$refs.form.validate()
      if (isValid) {
        this.$store.dispatch(`${moduleName}/register`)
      }
    }
  }
})
