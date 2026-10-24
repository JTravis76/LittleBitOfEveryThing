/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_APP_NAME: string
  readonly VITE_API_URL: string
  readonly VITE_CLIENT_ID: string
  readonly VITE_TENANT_ID: string
  readonly VITE_MSAL_LOG_LEVEL: string
  // more env variables...
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
