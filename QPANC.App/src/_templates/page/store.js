import { factory } from '@toby.mosque/utils'

class _TemplatePageModel {
}

const options = {
  model: _TemplatePageModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    }
  }
})

export { options, _TemplatePageModel }
