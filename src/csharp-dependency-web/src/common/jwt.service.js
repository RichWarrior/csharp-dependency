const _key = "csharp-dependency-token";

export const getToken = () => {
  return window.localStorage.getItem(_key);
};

export const saveToken = token => {
  window.localStorage.setItem(_key, token);
};

export const destroyToken = () => {
  window.localStorage.removeItem(_key);
};
export default { getToken, saveToken, destroyToken };