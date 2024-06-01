import { ResourceKey, createInstance } from "i18next";
import { initReactI18next } from "react-i18next/initReactI18next";
import { getOptions } from "./settings";
import HttpBackend, { HttpBackendOptions } from "i18next-http-backend";
import axios from "axios";
import https from "https";

const initI18next = async (lng: string, ns: string) => {
  const i18nInstance = createInstance();
  await i18nInstance
    .use(initReactI18next)
    .use(HttpBackend)
    .init<HttpBackendOptions>({
      backend: {
        request: async (options, url, data, callback) => {
          try {
            const httpsAgent = new https.Agent({
                rejectUnauthorized: false
            });
            const result = await axios.get<ResourceKey>(`https:/localhost:7035/api/translations/${lng}`, {httpsAgent});
            console.log(result.data);
            callback(null, {
              data: result.data,
              status: 200,
            });
          } catch (e) {
            console.error(e);
            callback(null, {
              data: {},
              status: 500,
            });
          }
        }
      },
      ...getOptions(lng, ns),
    });
  return i18nInstance;
};

export async function useTranslation(
  lng: string,
  ns: string = "translations",
  options: { keyPrefix?: string } = {}
) {
  const i18nextInstance = await initI18next(lng, ns);
  return {
    t: i18nextInstance.getFixedT(
      lng,
      Array.isArray(ns) ? ns[0] : ns,
      options.keyPrefix
    ),
    i18n: i18nextInstance,
  };
}
