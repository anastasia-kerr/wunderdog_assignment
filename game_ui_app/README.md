# Rock Paper Scissors MVP solution

## Important notes about the solution
- I chose to use material design icons and vuetify - for ease of dev really, but in real life scenario it's probbaly an overkill
- axios error checking is global to console.log at the moment because of ease of dev, but should be persisted to some sort of logging service
- I wrote my own polling method (api/helper.ts) to deal with live UI updates for the user, i.e. waiting for player to join, or waiting for round results. I originally planned on using SignalR but it is a bit drastic for such a small problem

## Technologies
- Vuetify
- Vue 2.X.X
- Vue-Router
- Vuex
- Typescript
- Material Deign Icons
- Jest

## Project setup
```
npm install
depending on your node version you may encounter a version clash error, override the issue with flag -f, this will force npm to ignore the error
```

### Customize configuration
In order for the game to function properly you need to have the Back End running seperately. Once the Api is running update the .env file with the correct base url end point


### Compiles and hot-reloads for development
```
npm run serve
```

### Run your unit tests
```
npm run test:unit
```

### Lints and fixes files
```
npm run lint
```