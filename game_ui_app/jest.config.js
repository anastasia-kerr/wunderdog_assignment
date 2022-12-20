module.exports = {
  preset: '@vue/cli-plugin-unit-jest/presets/typescript-and-babel',
  snapshotSerializers: ['jest-serializer-vue'],
  moduleNameMapper: {
    axios: 'axios/dist/node/axios.cjs'
  }
}
