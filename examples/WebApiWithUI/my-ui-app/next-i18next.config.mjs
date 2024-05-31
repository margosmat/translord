import HttpBackend from 'i18next-http-backend';

export const i18nConfig = {
    backend: {
      backendOptions: [{ expirationTime: 60 * 60 * 1000 }, { /* loadPath: 'https:// somewhere else' */ }], // 1 hour
      backends: [HttpBackend]
    },
    i18n: {
      defaultLocale: 'en',
      locales: ['en-US', 'de'],
      defaultLocale: 'en-US'
    },
  }