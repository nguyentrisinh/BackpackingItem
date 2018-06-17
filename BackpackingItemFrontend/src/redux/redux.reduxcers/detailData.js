import * as Types from "../redux.consts/detailData";
import update from "react-addons-update";
// update(state, {$merge: {userInfo: res}})
const initialState = {
    product:{
        isLoading:true,
        data:null
    }
}

export default function detailData(state = initialState, action) {

    switch (action.type) {
        case Types.GET_PRODUCT:
            return update(state, {
                product: {
                    $set: {
                        isLoading: action.isLoading,
                        data: action.data
                    }
                }
            });


        default:
            return state
    }
}