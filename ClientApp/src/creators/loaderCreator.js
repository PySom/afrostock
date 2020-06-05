import { actionTypes } from "../actiontypes/constants";

//actions
export const setLoader = (data) => ({
  type: actionTypes.getLoadStatus,
  data,
});
