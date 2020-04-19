// This is just an example,
// so you can safely delete all default props below

export default {
  validations: {
    compare: '{field} and {other} do not match',
    email: 'The {field} field is not a valid e-mail address',
    required: 'The {field} field is required',
    strength: 'Password is too weak, please improve your strength'
  },
  fields: {
    confirmPassword: 'Confirm your Password',
    confirmUserName: 'Confirm your Email',
    firstName: 'First Name',
    lastName: 'Last Name',
    password: 'Password',
    userName: 'Email'
  },
  actions: {
    forget: 'Recovery password',
    createAccount: 'Create Account',
    backLogin: 'Back to Login',
    login: 'Login',
    logout: 'Logout',
    register: 'Register',
    devboard: 'Developer Board'
  },
  login: {
    title: 'Login into the System'
  },
  http: {
    generic: 'Something not right happened',
    badRequest: 'We aren\'t able of to do your request, please review all the fields',
    unauthorized: 'You aren\'t authorized, please login',
    forbidden: 'You aren\'t allowed to do that action',
    unprocessable: '@:http.badRequest',
    serverError: 'An unexpected error occurred at the API',
    serviceUnavailable: 'An error occurred at the API, mostly because a third party service'
  },
  locale: {
    title: 'Idiom',
    ptbr: 'Portuguese',
    enus: 'English'
  },
  register: {
    title: 'Register a new account',
    success: 'Account created, you can login now!'
  }
}
