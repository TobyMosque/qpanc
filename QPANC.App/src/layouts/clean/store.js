import { factory } from '@toby.mosque/utils'

class CleanLayoutModel {
}

const options = {
  model: CleanLayoutModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    }
  }
})

export { options, CleanLayoutModel }
