import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routes'

Vue.use(VueRouter)

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default function (context) {
  const router = new VueRouter({
    scrollBehavior: () => ({ x: 0, y: 0 }),
    routes: routes(context),

    // Leave these as they are and change in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    mode: process.env.VUE_ROUTER_MODE,
    base: process.env.VUE_ROUTER_BASE
  })

  router.beforeEach((to, from, next) => {
    const { store } = context
    let protectedRoutes = to.matched.filter(route => route.meta.authorize)
    if (protectedRoutes.length > 0) {
      const logged = store.getters['app/isLogged']()
      if (!logged) {
        return next('/login')
      }

      protectedRoutes = protectedRoutes.filter(route => route.meta.authorize.roles)
      if (protectedRoutes.length > 0) {
        for (const protectedRoute of protectedRoutes) {
          const { roles } = protectedRoute.meta.authorize
          const isOnRoles = store.getters['app/isOnRoles'](roles)
          if (!isOnRoles) {
            return next('/home')
          }
        }
      }
    }
    next()
  })

  context.router = router
  return router
}
