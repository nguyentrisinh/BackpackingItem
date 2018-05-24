import * as Types from "../redux.consts/appData";
// import * as Api from "../actions/api";
// import {MAX_PAGE} from "../constants/apiPath";


export const getUserInfo = (data) => {
    return function (dispatch) {
        dispatch({
            type: Types.GET_USER_INFO,
            data
        })


    }

}



