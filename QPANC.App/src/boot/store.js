// import something here

// "async" is optional
export default async ({ app, store, router }) => {
  store.$router = router
  Object.defineProperty(store, '$t', {
    get () {
      return app.i18n.t.bind(app.i18n)
    }
  })
  Object.defineProperty(store, '$route', {
    get () {
      return router.app.$route
    }
  })
  Object.defineProperty(store, '$root', {
    get () {
      return router.app.$root
    }
  })
}
