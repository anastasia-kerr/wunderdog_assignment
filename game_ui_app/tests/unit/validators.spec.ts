import { nameIsRequired, roundIsRequired } from '@/validators'

describe('validators', () => {
  describe('nameIsRequired', () => {
    it('When empty string gives a warninig', () => {
      expect(nameIsRequired('')).toBe('Please tell us your nickname')
    })

    it('When has value returns true', () => {
      expect(nameIsRequired('some text')).toBe(true)
    })
  })
  describe('roundIsRequired', () => {
    it.each([
      ['0'],
      ['']
    ])('When empty string gives a warninig', (value:string) => {
      expect(roundIsRequired(value)).toBe('You need to play at least one round, buddy!')
    })

    it('When has value returns true', () => {
      expect(roundIsRequired('1')).toBe(true)
    })
  })
})
