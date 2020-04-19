import { factory } from '@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'page-home'
export default factory.page({
  name: 'HomePage',
  options,
  moduleName,
  storeModule: store,
  computed: {
    isOnRoles () {
      return this.$store.getters['app/isOnRoles']
    },
    isDeveloper () {
      return this.isOnRoles(['Developer'])
    }
  }
})
