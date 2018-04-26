import * as Types from "../redux.consts/listData";
import update from "react-addons-update";
// update(state, {$merge: {userInfo: res}})
const initialState = {
    listProducts: {
        isLoading: true,
        data: null
    },
}

export default function listData(state = initialState, action) {

    switch (action.type) {
        case Types.GET_LIST_PRODUCTS:
            return update(state, {
                listProducts: {
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