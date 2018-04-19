export const SERVER_PATHS = {
    api:'api',
    product:'products',
    category:'category',
    subCategories:'subcategories',
    getLastestProducts:(number)=> `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getlatestproducts?numberOfProduct=${number}`,
    getSpecialProducts:(number)=> `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getspecialproducts?numberOfProduct=${number}`

}