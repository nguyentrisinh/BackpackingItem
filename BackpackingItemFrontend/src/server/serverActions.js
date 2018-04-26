import {AXIOS} from "./serverConfig";
import {SERVER_PATHS} from "./serverPaths";
import {ITEM_PER_PAGE} from "./serverConfig";

export const getLastestProducts = (number) => {
    return AXIOS.get(SERVER_PATHS.getLastestProducts(number))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getSpecialProducts = (number) => {
    return AXIOS.get(SERVER_PATHS.getSpecialProducts(number))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getProductsByCategory = (categoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => {
    return AXIOS.get(SERVER_PATHS.getProductsByCategory(categoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getProductBySubCategory = (subCategoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => {
    return AXIOS.get(SERVER_PATHS.getProductsBySubCategory(subCategoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getProduct = (productId) => {
    return AXIOS.get(SERVER_PATHS.getProduct(productId))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}