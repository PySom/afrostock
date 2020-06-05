import { actionTypes } from "../actiontypes/constants";

export const loaderReducer = (state = true, action) => {
  switch (action.type) {
    case actionTypes.getLoadStatus:
      return action.data;
    default:
      return state;
  }
};
