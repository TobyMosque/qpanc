// This is just an example,
// so you can safely delete all default props below

export default {
  validations: {
    compare: '{field} e {other} não são iguais',
    email: 'O campo {field} não possui um email válido',
    required: 'O campo {field} é requerido',
    strength: 'Senha é muito fraca, por favor torne ela mais forte'
  },
  fields: {
    confirmPassword: 'Confirme à Senha',
    confirmUserName: 'Confirme o Email',
    firstName: 'Nome',
    lastName: 'Sobrenome',
    password: 'Senha',
    userName: 'Email'
  },
  actions: {
    forget: 'Recuperar senha',
    createAccount: 'Criar Conta',
    backLogin: 'Voltar para o Login',
    login: 'Acessar',
    logout: 'Sair',
    register: 'Registrar',
    devboard: 'Painel do Desenvolvedor'
  },
  login: {
    title: 'Acessar o Sistema'
  },
  http: {
    generic: 'Algo de errado aconteceu',
    badRequest: 'Não foi possivel realizar esta ação, por favor revise os campos',
    unauthorized: 'Você não está logado',
    forbidden: 'Você não está autorizado a realizar esta ação',
    unprocessable: '@:http.badRequest',
    serverError: 'Ocorreu um erro inesperado na API',
    serviceUnavailable: 'Ocorreu um erro na API, possivelmente ao tentar acessar um serviço externo'
  },
  locale: {
    title: 'Idioma',
    ptbr: 'Português',
    enus: 'Inglês'
  },
  register: {
    title: 'Registrar uma nova conta',
    success: 'Conta criada, você já pode logar'
  }
}
