export default function (context) {
  return {
    path: '',
    component: () => import('layouts/main/index.vue'),
    children: [
      { name: 'home', path: 'home', component: () => import('pages/home/index.vue') },
      {
        name: 'devboard',
        path: 'devboard',
        component: () => import('src/pages/devboard/index.vue'),
        meta: {
          authorize: {
            roles: ['Developer']
          }
        }
      }
    ],
    meta: {
      authorize: true
    }
  }
}
