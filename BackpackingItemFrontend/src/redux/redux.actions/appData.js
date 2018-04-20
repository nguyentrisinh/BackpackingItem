// import * as Types from "../constants/app";
// import * as Api from "../actions/api";
// import {MAX_PAGE} from "../constants/apiPath";
//
//
// export const getUserInfo = (token) => {
//     return function (dispatch) {
//         Api.getUserInfo(token).then(res => {
//             if (res.data.errors === null) {
//                 dispatch({
//                     type: Types.GET_USER_INFO,
//                     serverData: res
//                 })
//                 dispatch(doLogin(true));
//             }
//             else {
//                 dispatch(doLogin(false));
//
//             }
//         })
//     }
//
// }
//
//
//
