import Vue from 'vue'
import VueI18n from 'vue-i18n'
import messages from 'src/i18n'
import { Quasar } from 'quasar'

Vue.use(VueI18n)

export default ({ Vue, app, store }) => {
  const fallback = Quasar.lang.isoName
  const locale = store.getters['app/locale'] || fallback
  if (locale !== fallback) {
    import('quasar/lang/' + locale).then(Quasar.lang.set)
  }
  const i18n = new VueI18n({
    locale: locale,
    fallbackLocale: fallback,
    messages
  })

  app.i18n = i18n
}
