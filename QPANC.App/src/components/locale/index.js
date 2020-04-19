export default {
  name: 'LocaleComponent',
  data () {
    return {
      fab: false
    }
  },
  computed: {
    locale () {
      return this.$store.getters['app/locale']
    },
    icon () {
      return `img:statics/flags/${this.locale}.svg`
    }
  },
  methods: {
    async set (locale) {
      this.$store.commit('app/localeUser', locale)
      this.$i18n.locale = locale
      const lang = await import('quasar/lang/' + locale)
      console.log(lang)
      this.$q.lang.set(lang)
    }
  }
}
