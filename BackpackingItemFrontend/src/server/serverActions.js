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

export const postAccountRegister = (email,password,firstName,lastName,gender,birthday) => {
    return AXIOS.post(SERVER_PATHS.postAccountRegister(),
        {
            email,
            password,
            firstName,
            lastName,
            gender,
            birthday
        })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const postAccountLogin = (username,password) => {
    return AXIOS.post(SERVER_PATHS.postAccountLogin(),
        {
            username,
            password,
        })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getAccountCurrent = (token)=>{
    return AXIOS.get(SERVER_PATHS.getAccountCurrent(),
        {
            headers: {'Authorization': `Bearer ${token}`},
        })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const putAccountUpdateCurrent = (token,email,password,firstName,lastName,gender,birthday)=>{
    return AXIOS.put(SERVER_PATHS.putAccountUpdateCurrent(),
        {
            email,
            password,
            firstName,
            lastName,
            gender,
            birthday
        },{
        headers:{
            'Authorization':`Bearer ${token}`
        }
        })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getVariant = (variantId)=>{
    return AXIOS.get(SERVER_PATHS.getVariant(variantId))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getCityAll = ()=>{
    return AXIOS.get(SERVER_PATHS.getCityAll())
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}
const a= {
    "datetime": "2018-06-09T12:45:58.344Z",
    "totalPrice": 0,
    "address": "string",
    "receivePersonName": "string",
    "phone": "string",
    "status": 1,
    "voucherId": 0,
    "districtId": 0,
    "orderDetails": [
    {
        "quantity": 0,
        "pricePerUnit": 0,
        "totalPrice": 0,
        "variantId": 0
    }
]
}

export const getCityId = (cityId)=>{
    return AXIOS.get(SERVER_PATHS.getCityId(cityId))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const postOrderCreate = (token,data) =>{
    return AXIOS.post(SERVER_PATHS.postOrderCreate(),data,{
        headers: {'Authorization': `Bearer ${token}`},
    })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getOrderCurrent = (token,pageNumber,pageSize) =>{
    return AXIOS.get(SERVER_PATHS.getOrderCurrent(pageNumber,pageSize),{
        headers: {'Authorization': `Bearer ${token}`},
    })
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}
export const getDistrictId = (id) =>{
    return AXIOS.get(SERVER_PATHS.getDistrictId(id))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}
export const getOrderId = (id) =>{
    return AXIOS.get(SERVER_PATHS.getOrderId(id))
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}

export const getCategoryList = () =>{
    return AXIOS.get(SERVER_PATHS.getCategoryList())
        .then(res => {
            return res;
        })
        .catch(err => {
            return err.response
        })
}