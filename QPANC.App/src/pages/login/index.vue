<template>
  <div id="page-login">
    <h5 class="q-my-md">{{$t('login.title')}}</h5>
    <q-separator></q-separator>
    <q-form ref="form" class="row q-col-gutter-sm">
      <div class="col col-12">
        <q-input v-model="userName" :label="$t('fields.userName')" :rules="validation.userName" @blur="validationArgs.userName.server = true"></q-input>
      </div>
      <div class="col col-12">
        <q-input type="password" v-model="password" :label="$t('fields.password')" :rules="validation.password" @blur="validationArgs.password.server = true"></q-input>
      </div>
      <div class="col col-5">
        <q-btn class="full-width" flat color="primary" :label="$t('actions.forget')" @click="forget"></q-btn>
      </div>
      <div class="col col-7">
      </div>
      <div class="col col-5">
        <q-btn class="full-width" color="secondary" :label="$t('actions.createAccount')" to="/register"></q-btn>
      </div>
      <div class="col col-7">
        <q-btn class="full-width" color="positive" :label="$t('actions.login')" @click="login"></q-btn>
      </div>
    </q-form>
  </div>
</template>

<script src="./index.js"></script>
<style src="./index.sass" lang="sass"></style>
