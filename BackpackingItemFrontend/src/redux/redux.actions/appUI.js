import * as Types from "../redux.consts/appUI";


export const clickMenu = (id) => {
    return function (dispatch) {
        dispatch({
            type:Types.CLICK_MENU,
            id
        })
    }

}



