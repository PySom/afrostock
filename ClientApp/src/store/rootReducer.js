import { combineReducers } from 'redux'
import loaderReducer from '../reducers/loaderReducer'
import userReducer from '../reducers/userReducer'

export const rootReducer = combineReducers({
    user: userReducer,
    loader: loaderReducer,
})