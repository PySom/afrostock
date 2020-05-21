import { combineReducers } from "redux";
import { visibleReducer } from "../components/SearchArea/SearchArea";
import { loggedInStatusReducer } from "../components/Register/Register";

export const rootReducer = combineReducers({
  searchVisibility: visibleReducer,
  loggedInStatus: loggedInStatusReducer,
});
