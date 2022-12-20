import axios from 'axios'
import { CreateGameResponse, GetCurrentRoundPlayersResponse, GetGameResultResponse, GetRoundResultResponse, JoinCurrentRoundResponse, LeaveCurrentRoundResponse, PlaceGestureResponse } from './types'

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
export async function createGame (payload : {nickname:string, numberOfRounds:number}):Promise<number> {
  const { data } = await axios.post<CreateGameResponse>(
    'api/Games',
    payload
  )
  return data.result.id
}
export async function joinCurrentRound (payload : { gameId:string, playerNickname:string, playerIdentifier:string}):Promise<number> {
  const { data } = await axios.put<JoinCurrentRoundResponse>(
    `api/Games/${payload.gameId}/join`,
    payload
  )
  return data.result.roundNumber
}
export async function getCurrentRoundPlayers (gameId:string):Promise<string[]> {
  const { data } = await axios.get<GetCurrentRoundPlayersResponse>(
    `api/Games/${gameId}/players`
  )
  return data.result.players
}
export async function getRoundResults (gameId:string, round:number):Promise<GetRoundResultResponse> {
  const { data } = await axios.get<GetRoundResultResponse>(
    `api/Games/${gameId}/result/${round}`
  )
  return data
}
export async function getGameResult (gameId:string):Promise<GetGameResultResponse> {
  const { data } = await axios.get<GetGameResultResponse>(
    `api/Games/${gameId}/result/`
  )
  return data
}
export async function leaveCurrentRound (payload : { gameId:string, playerIdentifier:string}):Promise<number> {
  const { data } = await axios.put<LeaveCurrentRoundResponse>(
    `api/Games/${payload.gameId}/leave`,
    payload
  )
  return data.result.roundNumber
}
export async function placeGesture (payload : { gameId:string, playerNickname:string, playerIdentifier:string, roundGesture:number}):Promise<number> {
  const { data } = await axios.post<PlaceGestureResponse>(
    `api/Games/${payload.gameId}/gesture`,
    payload
  )
  return data.result.id
}
