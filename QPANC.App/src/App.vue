<template>
  <div id="q-app">
    <router-view />
  </div>
</template>

<script>
export default {
  name: 'App',
  watch: {
    '$q.dark.isActive' () {
      this.$brand.input.props.filled = this.$q.dark.isActive
      this.$brand.input.props.outlined = !this.$q.dark.isActive
    }
  }
}
</script>
