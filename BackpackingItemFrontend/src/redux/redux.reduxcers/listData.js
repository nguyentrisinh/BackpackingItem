import * as Types from "../redux.consts/listData";
import update from "react-addons-update";
import {ORDER_CHOICES} from "../../server/serverConfig";
// update(state, {$merge: {userInfo: res}})
const initialState = {
    listProducts: {
        isLoading: true,
        data: null,
        currentPage:1,
        hasNextPage:false,
        isLoadingMore:false,
        order:ORDER_CHOICES.find(o=>o.id=='NoOrder').value,
        minPrice:'',
        maxPrice:''
    },
}

export default function listData(state = initialState, action) {

    switch (action.type) {
        case Types.GET_LIST_PRODUCTS:
            return update(state, {
                listProducts: {
                    $set: {
                        isLoading: action.isLoading,
                        data:action.isReset?action.data.content:(state.listProducts.data?state.listProducts.data.concat(action.data.content):action.data.content),
                        currentPage:action.isReset ?initialState.listProducts.currentPage:action.currentPage,
                        hasNextPage:action.data.hasNextPage,
                        order:action.order,
                        minPrice:action.minPrice,
                        maxPrice:action.maxPrice,

                    }
                }
            });
        case Types.IS_LOADING_MORE:
            return update(state,{
                listProducts:{
                    $merge:{
                        isLoadingMore:true
                    }
                }
            })
        case Types.RESET_LIST_PRODUCTS:
            return update(state,{
                listProducts:{
                    $set:initialState.listProducts
                }
            })
        default:
            return state
    }
}