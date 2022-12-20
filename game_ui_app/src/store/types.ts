import { RoundResult } from '@/api/types'

export interface RootState {
    error: string,
    gameId: string,
    roundNumber: number,
    currentPlayers: string[],
    gameWinner: string,
    nickname?: string|null,
    playerIdentifier?: string|null,
    roundResults: RoundResult
  }
