<template>
  <q-layout id="layout-clean" view="lHh Lpr lFf" class="bg-main">
    <q-page-container>
      <q-page class="row">
        <div class="col flex flex-center relative-position layout-auth">
          <img alt="Quasar logo" class="absolute-center" src="~assets/quasar-logo-full.svg">
        </div>
        <div class="col col-auto shadow-up-2 page-container relative-position bg-content">
          <div class="page-form q-pa-xl absolute-center">
            <router-view />
          </div>
        </div>
      </q-page>
    </q-page-container>
    <locale-switch></locale-switch>
  </q-layout>
</template>

<script src="./index.js"></script>
<style src="./index.sass" lang="sass"></style>
