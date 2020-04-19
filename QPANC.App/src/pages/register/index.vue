<template>
  <div id="page-register">
    <h5 class="q-my-md">{{$t('register.title')}}</h5>
    <q-separator></q-separator>
    <q-form ref="form" class="row q-col-gutter-sm">
      <div class="col col-12">
        <q-input v-model="userName" :label="$t('fields.userName')" :rules="validation.userName" @blur="validationArgs.userName.server = true"></q-input>
      </div>
      <div class="col col-12">
        <q-input v-model="confirmUserName" :label="$t('fields.confirmUserName')" :rules="validation.confirmUserName"></q-input>
      </div>
      <div class="col col-12">
        <q-input v-model="firstName" :label="$t('fields.firstName')" :rules="validation.firstName"></q-input>
      </div>
      <div class="col col-12">
        <q-input v-model="lastName" :label="$t('fields.lastName')" :rules="validation.lastName"></q-input>
      </div>
      <div class="col col-12">
        <q-input type="password" v-model="password" :label="$t('fields.password')" :rules="validation.password" @blur="validationArgs.password.server = true"></q-input>
        <q-linear-progress size="5px" stripe :value="strength" :color="strColor" ></q-linear-progress>
      </div>
      <div class="col col-12">
        <q-input type="password" v-model="confirmPassword" :label="$t('fields.confirmPassword')" :rules="validation.confirmPassword"></q-input>
      </div>
      <div class="col col-5">
        <q-btn class="full-width" color="secondary" :label="$t('actions.backLogin')" to="/login"></q-btn>
      </div>
      <div class="col col-7">
        <q-btn class="full-width" color="positive" :label="$t('actions.register')" @click="register"></q-btn>
      </div>
    </q-form>
  </div>
</template>

<script src="./index.js"></script>
<style src="./index.sass" lang="sass"></style>
