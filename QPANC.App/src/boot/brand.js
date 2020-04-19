import { factory } from '@toby.mosque/utils'
import inject from './inject'
import { QInput, Dark } from 'quasar'

// "async" is optional
export default inject(({ Vue, store }) => {
  const brand = {}
  brand.input = Vue.observable({
    /*
    style: {
      'font-size': '12px'
    },
    class: {
      'custom-input': true
    },
    */
    props: {
      filled: Dark.isActive,
      outlined: !Dark.isActive
    }
  })

  factory.reBrand('q-input', QInput, brand.input)
  return {
    brand
  }
})
