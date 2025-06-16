const { FlatCompat } = require('@eslint/eslintrc');
const compat = new FlatCompat({ baseDirectory: __dirname });
const eslintrc = require('./.eslintrc.js');

module.exports = [
  {
    ignores: ['build/**', 'config/**', 'dist/**', '*.js', 'test/unit/coverage/**'],
  },
  ...compat.config(eslintrc),
];
