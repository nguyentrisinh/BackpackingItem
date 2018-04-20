import * as Types from "../redux.consts/homeData";
import * as SERVER_ACTIONS from '../../server/serverActions'
// import {MAX_PAGE} from "../constants/apiPath";


export const getLastestProducts = (number) => {
    return (dispatch) => {
        dispatch({
            type:Types.GET_LATEST_PRODUCT,
            isLoading:true,
            data:null
        })
        SERVER_ACTIONS.getLastestProducts(number).then(res=>{
            if (res.data.errors==null){
                dispatch({
                    type:Types.GET_LATEST_PRODUCT,
                    isLoading:false,
                    data:res.data.data
                })
            }
            else{
                dispatch({
                    type:Types.GET_LATEST_PRODUCT,
                    isLoading:false,
                    data:null
                })
            }
        })
    }

}

export const getSpecialProducts = (number) => {
    return (dispatch) => {
        dispatch({
            type:Types.GET_SPECIAL_PRODUCT,
            isLoading:true,
            data:null
        })
        SERVER_ACTIONS.getSpecialProducts(  number).then(res=>{
            if (res.data.errors==null){
                dispatch({
                    type:Types.GET_SPECIAL_PRODUCT,
                    isLoading:false,
                    data:res.data.data
                })
            }
            else{
                dispatch({
                    type:Types.GET_SPECIAL_PRODUCT,
                    isLoading:false,
                    data:null
                })
            }
        })
    }

}



