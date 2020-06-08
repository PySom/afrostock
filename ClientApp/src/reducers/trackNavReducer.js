import { actionTypes } from "../actiontypes/constants";
let url = document.documentURI.slice(document.baseURI.length - 1);

export const trackNavReducer = (state = url, action) => {
  switch (action.type) {
    case actionTypes.navUrl:
      return action.data;
    default:
      return state;
  }
};
