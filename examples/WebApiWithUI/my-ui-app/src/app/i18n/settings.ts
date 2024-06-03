export const fallbackLng = "en-GB";
export const languages = [fallbackLng, "pl", "de", "fr", "ja", "es", "uk", "cs"];
export const defaultNS = "translation";
export const cookieName = "i18next";

export function getOptions(lng = fallbackLng, ns = defaultNS) {
  return {
    // debug: true,
    supportedLngs: languages,
    fallbackLng,
    lng,
    fallbackNS: defaultNS,
    defaultNS,
    ns,
  };
}
