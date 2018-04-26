import * as Types from "../redux.consts/appUI";


export const clickMenu = (item) => {
    return function (dispatch) {
        dispatch({
            type: Types.CLICK_MENU,
            item
        })
    }

}



