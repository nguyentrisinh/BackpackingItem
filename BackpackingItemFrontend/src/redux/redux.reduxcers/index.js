import {combineReducers} from "redux";
import app from "./appData";
import appUI from './appUI'
import homeData from './homeData'

const rootReducer = combineReducers({
    app,
    appUI,
    homeData
})

export default rootReducer
