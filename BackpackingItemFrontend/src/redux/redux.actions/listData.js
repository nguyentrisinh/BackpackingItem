import * as Types from "../redux.consts/listData";
import * as SERVER_ACTIONS from '../../server/serverActions'
import {ITEM_PER_PAGE, MAP_CATEGORY} from "../../server/serverConfig";
// import {MAX_PAGE} from "../constants/apiPath";


export const getListProducts = (isReset, categorySlug, pageNumber, orderChoices, subCategorySlug = null, minPrice = '', maxPrice = '', pageSize = ITEM_PER_PAGE) => {
    return (dispatch) => {
        dispatch({
            type: Types.IS_LOADING_MORE
        });
        let category = MAP_CATEGORY.find(o => o.link == categorySlug);
        if (category) {
            if (subCategorySlug) {
                let subCategory = category.children.find(o => o.link == subCategorySlug);
                if (subCategory) {
                    SERVER_ACTIONS.getProductBySubCategory(subCategory.id, pageNumber, orderChoices, minPrice, maxPrice, pageSize).then(res => {
                        console.log(res)
                        if (res.data.errors == null) {
                            dispatch({
                                type: Types.GET_LIST_PRODUCTS,
                                isReset,
                                isLoading: false,
                                data: res.data.data,
                                isLoadingMore: false,
                                order: orderChoices,
                                minPrice,
                                maxPrice,
                                currentPage:pageNumber
                            })
                        }
                        else {
                            dispatch({
                                type: Types.GET_LIST_PRODUCTS,
                                isReset,
                                isLoading: false,
                                data: null,
                                isLoadingMore: false,
                                order: orderChoices,
                                minPrice,
                                maxPrice,
                                currentPage:pageNumber
                            })
                        }
                    })
                }
                else {
                    dispatch({
                        type: Types.GET_LIST_PRODUCTS,
                        isReset,
                        isLoading: false,
                        data: null,
                        isLoadingMore: false,
                        order: orderChoices,
                        minPrice,
                        maxPrice,
                        currentPage:pageNumber
                    });
                }
            }
            else {
                SERVER_ACTIONS.getProductsByCategory(category.id, pageNumber, orderChoices, minPrice, maxPrice, pageSize).then(res => {
                    if (res.data.errors == null) {
                        dispatch({
                            type: Types.GET_LIST_PRODUCTS,
                            isReset,
                            isLoading: false,
                            data: res.data.data,
                            isLoadingMore: false,
                            order: orderChoices,
                            minPrice,
                            maxPrice,
                            currentPage:pageNumber
                        })
                    }
                    else {
                        dispatch({
                            type: Types.GET_LIST_PRODUCTS,
                            isReset,
                            isLoading: false,
                            data: null,
                            isLoadingMore: false,
                            order: orderChoices,
                            minPrice,
                            maxPrice,
                            currentPage:pageNumber
                        })
                    }
                })
            }

        }
        else {
            dispatch({
                type: Types.GET_LIST_PRODUCTS,
                isReset,
                isLoading: false,
                data: null,
                isLoadingMore: false
            });
        }

    }

}

export const resetListProducts = () => {
    return (dispatch) => {
        dispatch({
            type: Types.RESET_LIST_PRODUCTS
        })
    }
}


