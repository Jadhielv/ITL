const { FlatCompat } = require('@eslint/eslintrc');
const compat = new FlatCompat({ baseDirectory: __dirname });
const eslintrc = require('./.eslintrc.js');

module.exports = [
  {
    ignores: ['build/**', 'config/**', 'dist/**', '*.js', 'test/unit/coverage/**'],
  },
  ...compat.config(eslintrc).map((config) => ({
    ...config,
    files: ['**/*.{js,vue}'],
  })),
  {
    files: ['test/unit/**/*.js'],
    languageOptions: {
      globals: {
        afterAll: 'readonly',
        afterEach: 'readonly',
        beforeAll: 'readonly',
        beforeEach: 'readonly',
        describe: 'readonly',
        expect: 'readonly',
        it: 'readonly',
        jest: 'readonly',
        test: 'readonly',
      },
    },
  },
];
