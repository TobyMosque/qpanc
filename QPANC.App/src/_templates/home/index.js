import { factory } from './node_modules/@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'page-home'
export default factory.page({
  name: 'HomePage',
  options,
  moduleName,
  storeModule: store
})
