import * as Types from "../redux.consts/listData";
import * as SERVER_ACTIONS from '../../server/serverActions'
import {ITEM_PER_PAGE,MAP_CATEGORY} from "../../server/serverConfig";
// import {MAX_PAGE} from "../constants/apiPath";


export const getListProducts = (categorySlug,pageNumber,orderChoices,subCategorySlug=null,minPrice='',maxPrice='',pageSize=ITEM_PER_PAGE) => {
    return (dispatch) => {
        // dispatch({
        //     type: Types.GET_LIST_PRODUCTS,
        //     isLoading: true,
        //     data: null
        // });
        let category = MAP_CATEGORY.find(o=>o.link==categorySlug);
        if (category){
            if (subCategorySlug){
               let subCategory = category.children.find(o=>o.link==subCategorySlug);
                if (subCategory){
                    SERVER_ACTIONS.getProductBySubCategory(subCategory.id,pageNumber,orderChoices,minPrice,maxPrice,pageSize).then(res => {
                        console.log(res)
                        if (res.data.errors == null) {
                            dispatch({
                                type: Types.GET_LIST_PRODUCTS,
                                isLoading: false,
                                data: res.data.data
                            })
                        }
                        else {
                            dispatch({
                                type: Types.GET_LIST_PRODUCTS,
                                isLoading: false,
                                data: null
                            })
                        }
                    })
                }
                else{
                    dispatch({
                        type: Types.GET_LIST_PRODUCTS,
                        isLoading: false,
                        data: null
                    });
                }
            }
            else{
                SERVER_ACTIONS.getProductsByCategory(category.id,pageNumber,orderChoices,minPrice,maxPrice,pageSize).then(res => {
                    if (res.data.errors == null) {
                        dispatch({
                            type: Types.GET_LIST_PRODUCTS,
                            isLoading: false,
                            data: res.data.data
                        })
                    }
                    else {
                        dispatch({
                            type: Types.GET_LIST_PRODUCTS,
                            isLoading: false,
                            data: null
                        })
                    }
                })
            }

        }
        else{
            dispatch({
                type: Types.GET_LIST_PRODUCTS,
                isLoading: false,
                data: null
            });
        }

    }

}


