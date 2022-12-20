<template>
  <v-dialog :width="dialogSize" persistent v-model="dialog">
    <v-form ref="form" v-model="valid">
      <v-card>
        <v-card-title v-text="headerLabel" />
        <v-card-text>
          <v-text-field :label="nicknameLabel" outlined v-model="nickname" :rules="nicknameRules" />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" v-text="btnLabel" @click="onBtnPress" />
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import { nameIsRequired } from '../../validators'
import config from '../../configs'
const { smallDialogSize } = config

export default Vue.extend({
  name: 'NicknameDialog',

  data: () => ({
    dialog: true,
    valid: false,
    dialogSize: smallDialogSize,
    nickname: '',
    nicknameLabel: 'Nickname',
    nicknameRules: [nameIsRequired],
    headerLabel: 'Choose your display name',
    btnLabel: 'Continue to game'
  }),
  methods: {
    validateForm (): void {
      const form = this.$refs.form as Vue & { validate: any }
      form.validate()
    },
    onBtnPress (): void {
      this.validateForm()
      if (!this.valid) return
      this.dialog = false
      this.$store.dispatch('setUser', this.nickname)
    }
  }
})
</script>
