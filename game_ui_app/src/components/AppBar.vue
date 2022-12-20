<template>
  <v-app-bar>
    Round number: <b>{{ roundNumber }}</b>
    <v-spacer />
    <div v-if="currentPlayers.length===2">
    {{currentPlayers[0]}} <b>VS</b> {{currentPlayers[1]}}
    </div>
    <invite-player-dialog  v-else/>
    <v-spacer />
    <user-menu />
  </v-app-bar>
</template>

<script lang="ts">
import Vue from 'vue'
import UserMenu from './UserMenu.vue'
import InvitePlayerDialog from './dialogs/InvitePlayerDialog.vue'

import { mapState } from 'vuex'

export default Vue.extend({
  name: 'GameAppBar',
  components: { UserMenu, InvitePlayerDialog },
  computed: {
    ...mapState([
      'roundNumber',
      'currentPlayers'
    ])
  }
})
</script>
