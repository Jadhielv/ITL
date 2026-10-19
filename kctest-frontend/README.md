# kctest-frontend

> A Vue.js project

## Build Setup

``` bash
# install dependencies
npm install

# start the API first from the repository root
dotnet run --project Backend/src/ITL.API --launch-profile ITL.API

# in another terminal, serve with hot reload at localhost:8080
npm run dev

# build for production with minification
npm run build

# build for production and view the bundle analyzer report
npm run build --report

# run unit tests
npm run unit
```

The development server proxies `/api` requests to the local API at `https://localhost:5001`, so the browser uses the frontend origin and does not need a hard-coded API hostname or cross-origin access. The API uses the InMemory database by default in Development. If the API runs at a different address, set `API_PROXY_TARGET` before starting the frontend (for example, `API_PROXY_TARGET=https://localhost:7001 npm run dev`).

In Codespaces, start the frontend with `HOST=0.0.0.0 npm run dev` and open the forwarded frontend port 8080 to use the app. The API's Swagger UI is available on the forwarded port 5000 at `/swagger/index.html`. The dev server derives the HMR WebSocket protocol and public host/port from the browser URL; no Codespaces-specific hostname needs to be configured.

For a detailed explanation on how things work, check out the [guide](http://vuejs-templates.github.io/webpack/) and [docs for vue-loader](http://vuejs.github.io/vue-loader).
