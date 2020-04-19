import clean from './areas/clean'
import main from './areas/main'

export default function (context) {
  const routes = [{
    path: '/',
    component: {
      render: h => h('router-view')
    },
    children: [
      {
        path: '',
        beforeEnter (to, from, next) {
          const { store } = context
          const logged = store.getters['app/isLogged']()
          if (logged) {
            next('/home')
          } else {
            next('/login')
          }
        }
      },
      clean(context),
      main(context)
    ]
  }]

  // Always leave this as last one
  if (process.env.MODE !== 'ssr') {
    routes.push({
      path: '*',
      component: () => import('pages/Error404.vue')
    })
  }

  return routes
}
