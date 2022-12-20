import config from '../configs'

export const poll = async (callback:() => Promise<any>) => {
  let pollCount = 0
  const handler = async () => {
    pollCount++
    const result = await callback()
    if (result || pollCount === config.maxPollingTimeInSeconds) {
      clearInterval(intervalId)
    }
  }
  const intervalId = setInterval(handler, config.pollingIntervalInMs)
}
