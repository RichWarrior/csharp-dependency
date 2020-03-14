const _key = "csharp-dependency-lang";

export const getLang = () => {
  return window.localStorage.getItem(_key);
};

export const saveLang = language => {
  window.localStorage.setItem(_key, language);
};

export const destroyLang = () => {
  window.localStorage.removeItem(_key);
};
export default { getLang, saveLang, destroyLang };