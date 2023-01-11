import axios from 'axios'
import { GetStateResponse, GetTasksResponse, SystemTask } from './types'

function setUpInterceptors () {
  axios.interceptors.response.use(null, (error) => {
    if (error?.response?.data?.Errors) {
      console.log('Error while calling the api: ', JSON.stringify(error?.response?.data?.Errors))
    } else {
      console.log('unexpected error while calling the api: ', error)
    }
    return Promise.reject(error)
  })
}
export function setApiDefaults (url:string) {
  axios.defaults.baseURL = url
  setUpInterceptors()
}
export async function getTasks ():Promise<SystemTask[]> {
  const { data } = await axios.get<GetTasksResponse>(
    'api/System/tasks'
  )
  return data.result.tasks
}
export async function getState ():Promise<string> {
  const { data } = await axios.get<GetStateResponse>(
    'api/System'
  )
  return data.result.state
}

export async function setTaskState (payload : { taskId:string, isOff:boolean}):Promise<void> {
  await axios.put(`api/system/task/${payload.taskId}?isOff=${payload.isOff}`)
}
