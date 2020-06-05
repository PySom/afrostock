import { combineReducers } from "redux";
import { visibleReducer } from "../reducers/viisibleSearchReducer";
import { loggedInStatusReducer } from "../components/Register/Register";
import { collectReducer } from "../reducers/collectsReducer";
import { loaderReducer } from "../reducers/loaderReducer";

export const rootReducer = combineReducers({
  searchVisibility: visibleReducer,
  loggedInStatus: loggedInStatusReducer,
  collects: collectReducer,
  loadar: loaderReducer,
});
