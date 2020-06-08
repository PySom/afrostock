import { actionTypes } from "../actiontypes/constants";

//actions
export const getNavUrl = (data) => ({
  type: actionTypes.navUrl,
  data,
});
