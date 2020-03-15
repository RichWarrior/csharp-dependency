const _key = "csharp-dependency-user";

export const getUser = () => {
  return window.localStorage.getItem(_key);
};

export const saveUser = user => {
  window.localStorage.setItem(_key, user);
};

export const destroyUser = () => {
  window.localStorage.removeItem(_key);
};
export default { getUser, saveUser, destroyUser };