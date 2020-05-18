import axios from "axios";
import ls from "../local/ourLocalStorage";
const user = JSON.parse(localStorage.getItem("user"));
axios.defaults.headers.common = {
  Authorization: `Bearer ${(user && user.token) || ""}`,
};

const baseUrl = "/api/";

const verifyItem = (item) => {
  if (Array.isArray(item)) {
    return item.length;
  }
  return item;
};

const checkItemInLs = (name) => {
  //get intended item from local storage
  const item = ls.getItemInLs(name);
  return new Promise((resolve, reject) => {
    item && verifyItem(item.item) ? resolve(item.item) : reject(item.item);
  });
};

const getAll = (url, name) => {
  const item = checkItemInLs(name);
  //check if we have that item, if we do return it, otherwise, make an api call
  return item
    .then((data) => {
      return data;
    })
    .catch((e) => {
      return axios
        .get(baseUrl + url)
        .then((response) => {
          if (name) ls.persistItemInLS(name, response.data, 1);
          return response.data;
        })
        .catch((err) => {
          throw new Error(err.response.data);
        });
    });
};

const getItemWithId = (url, name) => {
  const item = checkItemInLs(name);
  return item
    .then((data) => {
      return data;
    })
    .catch((e) => {
      return axios
        .get(baseUrl + url)
        .then((response) => {
          if (name) ls.persistItemInLS(name, response.data, 1);
          return response.data;
        })
        .catch((err) => {
          throw new Error(err.response.data);
        });
    });
};

const updateWithId = (url, newObject, name) => {
  const item = checkItemInLs(name);
  return item
    .then((data) => data)
    .catch((e) => {
      return axios
        .put(baseUrl + url, newObject)
        .then((response) => {
          ls.persistItemInLS(name, response.data, 1);
          return response.data;
        })
        .catch((err) => {
          throw new Error(err.response.data);
        });
    });
};

const create = (url, newObject, name) => {
  // console.log(axios.defaults.headers, user);
  const item = checkItemInLs(name);
  return item
    .then((data) => data)
    .catch((e) => {
      return axios
        .post(baseUrl + url, newObject)
        .then((response) => {
          ls.persistItemInLS(name, response.data, 1);
          return response.data;
        })
        .catch((err) => {
          console.log(err.response);
          throw new Error(err.response.data.message);
        });
    });
};

const deleteWithId = (url, name) =>
  axios
    .delete(baseUrl + url)
    .then((response) => {
      ls.removeItemFromLS(name);
      return response.data;
    })
    .catch((err) => {
      throw new Error(err.response.data);
    });

export default {
  getAll,
  getItemWithId,
  updateWithId,
  create,
  deleteWithId,
};
