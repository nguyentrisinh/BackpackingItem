import {combineReducers} from "redux";
import app from "./appData";
import appUI from './appUI'
import homeData from './homeData';
import listData from './listData';
import detailData from './detailData'

const rootReducer = combineReducers({
    app,
    appUI,
    homeData,
    listData,
    detailData
})

export default rootReducer
