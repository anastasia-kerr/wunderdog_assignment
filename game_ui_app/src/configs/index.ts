import { Gestures } from '../types'

export default {
  url: '',
  smallDialogSize: 320,
  pollingIntervalInMs: 2000,
  maxPollingTimeInSeconds: 1200, // 20 mins
  availableGestures: [
    {
      text: 'Rock',
      value: Gestures.Rock,
      img: require('../assets/Rock.jpg')
    },
    {
      text: 'Paper',
      value: Gestures.Paper,
      img: require('../assets/Paper.jpg')
    },
    {
      text: 'Scissors',
      value: Gestures.Scissors,
      img: require('../assets/Scissors.jpg')
    }
  ]
}
