export const nameIsRequired = (value: any) => !!value || 'Please tell us your nickname'
export const roundIsRequired = (value: any) => (+value) !== 0 || 'You need to play at least one round, buddy!'
