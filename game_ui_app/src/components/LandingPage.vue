<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="display-2 text-center font-weight-bold mb-3">
          {{ header }}
        </h1>
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="red--text" v-text="subtitle" />
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col>
        <v-data-table dense disable-pagination
        hide-default-footer
      :headers="headers"
      :items="tasks"
      class="elevation-1"
    >
      <template v-slot:[`item.state`]="{ item }">
        <v-checkbox
      v-model="item.isOff"
      @change="(value)=>onSwitch(item)"
    ></v-checkbox>
      </template>
    </v-data-table>
      </v-col>
      <v-col cols="5" >  STATE is {{state}}</v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { getState, getTasks, setTaskState } from '@/api'
import { SystemTask } from '@/api/types'
import Vue from 'vue'

export default Vue.extend({
  name: 'LandingPage',

  data: () => ({
    header: "Welcome to Anastasiia Kerr's assignment",
    subtitle: 'Task monitoring',
    state: 'Unknown',
    headers: [
      {
        text: 'Task',
        align: 'start',
        sortable: true,
        value: 'title'
      },
      {
        text: 'Importance',
        align: 'start',
        sortable: true,
        value: 'importance'
      },
      { text: 'Is offline', value: 'state' }
    ],
    tasks: [] as SystemTask[]
  }),
  async created () {
    await this.setTasks()
    await this.setState()
  },
  methods: {
    async onSwitch (item: SystemTask) {
      await setTaskState({ taskId: item.id, isOff: item.isOff })
      await this.setTasks()
      await this.setState()
    },
    async setTasks () {
      this.tasks = await getTasks()
    },
    async setState () {
      this.state = await getState()
    }
  }
})
</script>
