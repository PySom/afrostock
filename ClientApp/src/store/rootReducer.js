import { combineReducers } from 'redux'
import { visibleReducer } from '../components/SearchArea/SearchArea'

export const rootReducer = combineReducers({
    searchVisibility: visibleReducer,
})