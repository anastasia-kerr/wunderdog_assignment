import { Gestures } from '@/types'

type BaseApiResponse<T> = {
    succeeded: boolean;
    errors: any[];
    result: T;
}
export type CreateGameResponse = BaseApiResponse<{
    id: number
}>
export type JoinCurrentRoundResponse = BaseApiResponse<{
    id: number;
    roundNumber: number
}>
export type GetCurrentRoundPlayersResponse = BaseApiResponse<{
    players: string[]
}>
export type RoundResult = {
    isDraw?: boolean;
    isLastRound?: boolean;
    loserGesture?: Gestures;
    loserNickname?: string;
    winnerGesture?: Gestures;
    winnerNickname?: string;
}
export type GetRoundResultResponse = BaseApiResponse<RoundResult>
export type GetGameResultResponse = BaseApiResponse<
{winnerNickname?: string}
>
export type LeaveCurrentRoundResponse = JoinCurrentRoundResponse
export type PlaceGestureResponse = JoinCurrentRoundResponse
