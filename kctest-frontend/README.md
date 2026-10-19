# kctest-frontend

The frontend scaffold uses Vue 3, Vue Router 4, Pinia, BootstrapVue Next, Bootstrap 5, and Webpack 5.

## Prerequisites

- Node.js 18 or later
- npm 9 or later
- The .NET 10 SDK to run the API locally

## Install and run

From the repository root, start the API in a terminal:

```bash
dotnet run --project Backend/src/ITL.API --launch-profile ITL.API
```

In another terminal, install dependencies and start the frontend:

```bash
cd kctest-frontend
npm ci
npm run dev
```

The frontend development server proxies `/api` requests to the local API at `https://localhost:5001`. The browser uses the frontend origin, so it does not need a hard-coded API hostname or cross-origin access. The API uses the InMemory database by default in Development. To use a different API address, set `API_PROXY_TARGET` before starting the frontend, for example:

```bash
API_PROXY_TARGET=https://localhost:7001 npm run dev
```

In Codespaces, start the frontend with `HOST=0.0.0.0 npm run dev` and open the forwarded frontend port 8080. Swagger is available on forwarded port 5000 at `/swagger/index.html`; the dev server derives the HMR WebSocket protocol and public host/port from the browser URL.

## Validate and build

Run these commands from `kctest-frontend`:

```bash
npm run lint
npm test
npm run build
```

To build with the bundle analyzer report enabled, run `npm run build --report`.
