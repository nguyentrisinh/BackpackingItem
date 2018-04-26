import * as Types from "../redux.consts/detailData";
import * as SERVER_ACTIONS from '../../server/serverActions'
// import {MAX_PAGE} from "../constants/apiPath";


export const getProduct = (productId) => {
    return (dispatch) => {
        dispatch({
            type: Types.GET_PRODUCT,
            isLoading: true,
            data: null
        })
        SERVER_ACTIONS.getProduct(productId).then(res => {
            if (res.data.errors == null) {
                dispatch({
                    type: Types.GET_PRODUCT,
                    isLoading: false,
                    data: res.data.data
                })
            }
            else {
                dispatch({
                    type: Types.GET_PRODUCT,
                    isLoading: false,
                    data: null
                })
            }
        })
    }

}



