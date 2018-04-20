import {AXIOS} from "./serverConfig";
import {SERVER_PATHS} from "./serverPaths";

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