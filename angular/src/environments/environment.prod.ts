import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TANDUNG',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44319/',
    redirectUri: baseUrl,
    clientId: 'TANDUNG_App',
    responseType: 'code',
    scope: 'offline_access TANDUNG',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44356',
      rootNamespace: 'ERP.TANDUNG',
    },
  },
} as Environment;
