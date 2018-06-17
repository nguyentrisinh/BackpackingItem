import * as Types from "../redux.consts/appUI";


export const clickMenu = (item) => {
    return function (dispatch) {
        dispatch({
            type: Types.CLICK_MENU,
            item
        })
    }

}
export const clickModalUser = (value) => {
    return function (dispatch) {
        dispatch({
            type: Types.CLICK_MODAL_USER,
            value
        })
    }

}



