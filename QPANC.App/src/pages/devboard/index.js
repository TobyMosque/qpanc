import { factory } from '@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'page-devboard'
export default factory.page({
  name: 'DevboardPage',
  options,
  moduleName,
  storeModule: store
})
