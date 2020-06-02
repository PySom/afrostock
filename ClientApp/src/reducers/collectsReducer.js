import { actionTypes } from "../actiontypes/constants";
let initialState = JSON.parse(localStorage.getItem("collects"));

export const collectReducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.getCollect:
      return action.data;
    default:
      return state;
  }
};
