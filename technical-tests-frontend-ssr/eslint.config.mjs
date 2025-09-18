import globals from 'globals';
import pluginJs from '@eslint/js';
import tseslint from 'typescript-eslint';

import angularPlugin from '@angular-eslint/eslint-plugin';
import angularTemplatePlugin from '@angular-eslint/eslint-plugin-template';
import angularTemplateParser from '@angular-eslint/template-parser';

export default [
  // JS/TS config
  { files: ['**/*.{js,mjs,cjs,ts}'], ignores: ['**/*.html'] },
  { languageOptions: { globals: { ...globals.browser, ...globals.node } } },
  pluginJs.configs.recommended,
  ...tseslint.configs.strict,
  {
    plugins: { '@angular-eslint': angularPlugin },
    rules: {
      semi: ['error', 'always'],
      '@typescript-eslint/explicit-member-accessibility': 'error',
      '@typescript-eslint/explicit-function-return-type': 'error',
      '@typescript-eslint/adjacent-overload-signatures': 'error',
      '@typescript-eslint/member-ordering': [
        'error',
        {
          default: {
            memberTypes: [
              'private-static-field',
              'public-static-field',
              'protected-static-field',
              'private-instance-field',
              'public-instance-field',
              'protected-instance-field',
              'constructor',
              'public-method',
              'protected-method',
              'private-method',
              'public-get',
              'protected-get',
              'private-get',
            ],
            order: 'alphabetically',
          },
        },
      ],
      //'@angular-eslint/component-class-suffix': 'error',
      '@angular-eslint/directive-class-suffix': 'error',
      '@angular-eslint/no-empty-lifecycle-method': 'warn',
      '@angular-eslint/use-lifecycle-interface': 'warn',
      '@typescript-eslint/no-empty-function': 'error',
    },
  },
  // Angular template config
  {
    files: ['**/*.html'],
    languageOptions: { parser: angularTemplateParser },
    plugins: { '@angular-eslint/template': angularTemplatePlugin },
    rules: {
      // Reglas recomendadas para templates
      '@angular-eslint/template/no-negated-async': 'error',
      '@angular-eslint/template/banana-in-box': 'error',
      '@angular-eslint/template/eqeqeq': 'error',
    },
  },
];
