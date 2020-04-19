const validations = {}
validations.required = ({ self, field }) => {
  return function required (val) {
    return !!val || self.$t('validations.required', {
      field: self.$t(`fields.${field}`)
    })
  }
}

const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
validations.email = ({ self, field }) => {
  return function required (val) {
    return emailRegex.test(val) || self.$t('validations.email', {
      field: self.$t(`fields.${field}`)
    })
  }
}

validations.server = ({ self, field }) => {
  return function server (val) {
    return self.validationArgs[field].server
  }
}

validations.compare = ({ self, field }) => {
  return function required (val) {
    const property = self.validationArgs[field].compare
    return val === self[property] || self.$t('validations.compare', {
      field: self.$t(`fields.${field}`),
      other: self.$t(`fields.${property}`)
    })
  }
}

validations.strength = ({ self, field }) => {
  return function required (val) {
    const property = self.validationArgs[field].strength
    const strength = self[property]
    return strength >= 0.6 || self.$t('validations.strength', {
      field: self.$t(`fields.${field}`)
    })
  }
}

export default function validation (self, context) {
  const _validations = {}
  for (const field in context) {
    const _rules = []
    const rules = context[field]
    const names = Object.keys(validations)
    for (const rule of names) {
      if (rules.includes(rule)) {
        _rules.push(validations[rule]({ self, field }))
      }
    }
    _validations[field] = _rules
  }
  _validations.resetServer = function reset () {
    for (const field in context) {
      if (field in self.validationArgs && 'server' in self.validationArgs[field]) {
        self.validationArgs[field].server = true
      }
    }
  }
  return _validations
}
