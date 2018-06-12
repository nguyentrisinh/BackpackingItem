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

export const getOrderCurrent = (data) => {
    return function (dispatch) {
        dispatch({
            type: Types.GET_ORDER_CURRENT,
            data
        })
    }
}

export const addToCart = (data,quantity)=>{
    return function(dispatch){
       dispatch({
           type: Types.ADD_TO_CART,
           data,
           quantity
       })
    }
}

export const removeFromCart = (variantId)=>{
    return function(dispatch){
        dispatch({
            type: Types.REMOVE_FROM_CART,
            variantId
        })
    }
}



