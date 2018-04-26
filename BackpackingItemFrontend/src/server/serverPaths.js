

export const SERVER_PATHS = {
    api: 'api',
    product: 'products',
    category: 'category',
    subCategories: 'subcategories',
    getLastestProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getlatestproducts?numberOfProduct=${number}`,
    getSpecialProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getspecialproducts?numberOfProduct=${number}`,
    getProductsByCategory: (categoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsByCategory/${categoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=1&PageSize=${pageSize}`,
    getProductsBySubCategory: (subCategoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsBySubCategory/${subCategoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=1&PageSize=${pageSize}`,

}