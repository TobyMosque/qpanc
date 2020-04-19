<template>
  <q-page id="page-home" class="flex flex-center">
    Home <br/>
    <template v-if="isDeveloper">
      <q-btn to="/devboard" color="primary" :label="$t('actions.devboard')" />
    </template>
  </q-page>
</template>s

<script src="./index.js"></script>
<style src="./index.sass" lang="sass"></style>
