import { actionTypes } from "../actiontypes/constants";

export const visibleReducer = (state = true, action) => {
  switch (action.type) {
    case actionTypes.visible:
      return action.data;
    default:
      return state;
  }
};
