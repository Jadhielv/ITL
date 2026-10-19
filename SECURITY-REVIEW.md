# Frontend dependency security review

**Review date:** 2026-10-19  
**Scope:** The two open Dependabot alerts in `kctest-frontend/package-lock.json`.

## Findings and remediation

| Alert | Package and affected version | Severity | Impact and dependency path | Remediation |
|---|---|---|---|---|
| [Dependabot #212](https://github.com/Jadhielv/ITL/security/dependabot/212) · [GHSA-rj75-hqrm-r3gf](https://github.com/advisories/GHSA-rj75-hqrm-r3gf) | `postcss-selector-parser` 6.1.5; affected versions `< 7.1.6` | Medium | Denial of service from quadratic parsing of crafted selectors. This is a development/build dependency used by the CSS processing chain, not a direct frontend runtime dependency. | Updated the root override to `^7.1.6`; the lockfile now resolves 7.1.6. |
| [Dependabot #215](https://github.com/Jadhielv/ITL/security/dependabot/215) · [GHSA-hp3w-g68c-fv3c](https://github.com/advisories/GHSA-hp3w-g68c-fv3c) | `sprintf-js` 1.0.3; affected versions `<= 1.1.3` | Medium | Denial of service from unbounded format precision. The package was brought in by the development-only chain `@istanbuljs/load-nyc-config` → `js-yaml` 3 → `argparse` 1.0.x. The advisory lists no patched `sprintf-js` version. | Replaced the `argparse` 1.x dependency with 2.0.1 using an npm override. This removes `sprintf-js` from the installed dependency graph instead of pinning another vulnerable `sprintf-js` release. |

Both alerts concern denial-of-service conditions in build/test tooling; the reviewed paths are not used to handle attacker-controlled application requests at runtime. The change does not suppress npm or Dependabot advisories. GitHub may continue to display the alerts until the updated lockfile is merged and Dependabot rescans the default branch.

## Verification

- `npm ci --prefix kctest-frontend --legacy-peer-deps` — passed.
- `npm run unit --prefix kctest-frontend -- --runInBand` — passed (2 suites, 2 tests).
- `npm explain --prefix kctest-frontend postcss-selector-parser` — resolves to 7.1.6.
- `npm explain --prefix kctest-frontend sprintf-js` — no dependency found.
- `node -e "const yaml=require('./kctest-frontend/node_modules/js-yaml'); console.log(yaml.load('a: 1'))"` and the `js-yaml` CLI help smoke test — passed with the overridden `argparse`.
- `npm audit --prefix kctest-frontend --json` — neither targeted package is present in the audit findings. The full frontend dependency graph still reports 50 other findings (7 moderate, 43 high), which are outside this focused review.
- `npm run build --prefix kctest-frontend` — did not pass: the existing Webpack config uses a `devtool` value rejected by Webpack 5's schema. The dependency remediation does not modify the Webpack configuration.

