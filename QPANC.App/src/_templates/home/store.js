import { factory } from './node_modules/@toby.mosque/utils'

class HomePageModel {
}

const options = {
  model: HomePageModel
}

export default factory.store({
  options,
  actions: {
    async initialize ({ state }, { route, next }) {
    }
  }
})

export { options, HomePageModel }
