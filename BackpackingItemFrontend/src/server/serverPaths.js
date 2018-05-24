

export const SERVER_PATHS = {
    api: 'api',
    product: 'products',
    category: 'category',
    subCategories: 'subcategories',
    account:'account',
    getLastestProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getlatestproducts?numberOfProduct=${number}`,
    getSpecialProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getspecialproducts?numberOfProduct=${number}`,
    getProductsByCategory: (categoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsByCategory/${categoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=${pageNumber}&PageSize=${pageSize}`,
    getProductsBySubCategory: (subCategoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsBySubCategory/${subCategoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=${pageNumber}&PageSize=${pageSize}`,
    getProduct:(productId)=>`${SERVER_PATHS.api}/${SERVER_PATHS.product}/${productId}`,
    postAccountRegister: ()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/customerregister`,
    postAccountLogin:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/login`
}