import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TANDUNG Admin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5000/',
    redirectUri: baseUrl,
    clientId: 'TANDUNG_Admin',
    responseType: 'code',
    scope: 'offline_access TANDUNG.Admin',
    requireHttps: true,
    dummyClientSecret:'abcd*123'
  },
  apis: {
    default: {
      url: 'https://localhost:5001',
      rootNamespace: 'TANDUNG.Admin',
    },
  },
} as Environment;
