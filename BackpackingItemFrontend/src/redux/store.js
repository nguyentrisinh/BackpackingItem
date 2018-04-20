import reducer from './redux.reduxcers/index';
import {applyMiddleware, createStore} from "redux";
import reduxThunk from "redux-thunk";


const createStoreWithMiddleware = applyMiddleware(reduxThunk)(createStore);
export default createStoreWithMiddleware(reducer, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());


