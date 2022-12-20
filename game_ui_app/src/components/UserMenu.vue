<template>
  <div class="text-center">
    <v-menu offset-y>
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="secondary" text v-bind="attrs" v-on="on" v-text="nickname" />
      </template>
      <v-list>
        <v-list-item>
          <v-btn id="leave_button" text color="primary" @click="signOut">{{ signOutLabel }}</v-btn>
        </v-list-item>
      </v-list>
    </v-menu>
    <nickname-dialog id="nickname_dialog" v-if="showNicknameDialog" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import NicknameDialog from './dialogs/NicknameDialog.vue'
import { mapState } from 'vuex'

export default Vue.extend({
  name: 'UserMenu',
  components: { NicknameDialog },
  data: () => ({
    signOutLabel: 'Leave game'
  }),
  computed: {
    ...mapState([
      'nickname'
    ]),
    showNicknameDialog (): boolean {
      return !this.nickname
    }
  },
  methods: {
    async signOut (): Promise<void> {
      await this.$store.dispatch('leaveGame')
      this.$router.push({ name: 'home' })
    }
  }
})
</script>
