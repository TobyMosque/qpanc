export default function (context) {
  return {
    path: '',
    component: () => import('layouts/clean/index.vue'),
    children: [
      { name: 'login', path: 'login', component: () => import('pages/login/index.vue') },
      { name: 'register', path: 'register', component: () => import('pages/register/index.vue') }
    ]
  }
}
