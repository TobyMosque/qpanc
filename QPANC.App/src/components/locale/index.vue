<template>
  <q-page-sticky position="bottom-right" class="component-locale" :offset="[18, 18]">
    <q-fab v-model="fab" :label="$t('locale.title')" external-label vertical-actions-align="right" :icon="icon" direction="up" >
      <q-fab-action label-position="left" icon="img:statics/flags/pt-br.svg" @click="set('pt-br')" :label="$t('locale.ptbr')" external-label />
      <q-fab-action label-position="left" icon="img:statics/flags/en-us.svg" @click="set('en-us')" :label="$t('locale.enus')" external-label />
    </q-fab>
  </q-page-sticky>
</template>

<script src="./index.js"></script>
<style src="./index.sass" lang="sass"></style>
