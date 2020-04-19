import { factory } from '@toby.mosque/utils'
import store, { options } from './store'

const moduleName = 'page-_template'
export default factory.page({
  name: '_TemplatePage',
  options,
  moduleName,
  storeModule: store
})
