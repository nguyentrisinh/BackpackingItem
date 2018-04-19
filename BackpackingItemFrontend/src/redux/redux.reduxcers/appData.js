import * as Types from "../redux.consts/appData";
import update from "react-addons-update";
// update(state, {$merge: {userInfo: res}})
const initialState = {
    userInfo: null,
    isOpenModalLogin: false,
    isLogin: false,
    searchResult: {
        hasMore: false,
        nextPage: 1,
        data: [],
        isLoading: false
    }
}

export default function index(state = initialState, action) {

    switch (action.type) {
        // case Types.GET_USER_INFO:
        //
        //     return Object.assign({}, state, {userInfo: action.serverData})
        // // Object.assign({},state,{userInfo:action.serverData})
        // case Types.TOGGLE_MODAL_LOGIN:
        //     return Object.assign({}, state, {isOpenModalLogin: !state.isOpenModalLogin})
        //
        // case Types.DO_LOGIN:
        //     return Object.assign({}, state, {isLogin: action.isLogin});
        // case Types.RESET_SEARCH:
        //     return Object.assign({}, state, {searchResult: initialState.searchResult})
        // case Types.LOADING_SEARCH:
        //     let newSearchResult = update(state.searchResult, {$merge: {isLoading: true, hasMore: false}})
        //     return Object.assign({}, state, {searchResult: newSearchResult})
        // case Types.GET_SEARCH:
        //     if (action.serverData.errors === null) {
        //         return Object.assign({}, state, {
        //             searchResult: {
        //                 hasMore: action.serverData.data.has_more,
        //                 nextPage: state.searchResult.nextPage + 1,
        //                 data: [...state.searchResult.data.concat(action.serverData.data.films)],
        //                 isLoading: false
        //             }
        //         })
        //     }
        //     return state

        default:
            return state
    }
}