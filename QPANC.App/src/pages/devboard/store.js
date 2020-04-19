import { factory } from '@toby.mosque/utils'

class DevboardPageModel {
}

const options = {
  model: DevboardPageModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    }
  }
})

export { options, DevboardPageModel }
