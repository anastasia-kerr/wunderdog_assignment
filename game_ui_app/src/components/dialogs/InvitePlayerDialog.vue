<template>
  <v-dialog :width="dialogSize" v-model="dialog">
    <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="primary"
          dark
          v-bind="attrs"
          v-on="on"
          text
          v-text="headerLabel"
        >
        </v-btn>
      </template>
      <v-card>
        <v-card-title v-text="headerLabel" />
        <v-card-subtitle v-text="subheaderLabel"/>
        <v-card-text>
          <v-text-field :label="urlLabel" outlined readonly v-model="url" />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" v-text="btnLabel" @click="onBtnPress" />
        </v-card-actions>
      </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import config from '../../configs'
const { smallDialogSize } = config

export default Vue.extend({
  name: 'InvitePlayers',

  data: () => ({
    dialog: false,
    dialogSize: smallDialogSize,
    urlLabel: 'Game\'s url',
    headerLabel: 'Invite player',
    subheaderLabel: 'Send the link below to your friend to join the game',
    btnLabel: 'Copy invitation link'
  }),
  computed: {
    url ():string {
      return `${location.origin}/game/${this.$route.params.id}`
    }
  },
  methods: {
    onBtnPress (): void {
      navigator.clipboard.writeText(this.url)
      this.dialog = false
    }
  }
})
</script>
