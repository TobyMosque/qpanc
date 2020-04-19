import { factory } from '@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'layout-clean'
export default factory.page({
  name: 'CleanLayout',
  options,
  moduleName,
  storeModule: store
})
