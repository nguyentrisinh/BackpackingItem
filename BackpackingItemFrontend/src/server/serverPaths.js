

export const SERVER_PATHS = {
    api: 'api',
    product: 'products',
    category: 'category',
    subCategories: 'subcategories',
    account:'account',
    variant:'variant',
    city:'city',
    order:'order',
    getLastestProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getlatestproducts?numberOfProduct=${number}`,
    getSpecialProducts: (number) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/getspecialproducts?numberOfProduct=${number}`,
    getProductsByCategory: (categoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsByCategory/${categoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=${pageNumber}&PageSize=${pageSize}`,
    getProductsBySubCategory: (subCategoryId,pageNumber,orderChoices,minPrice,maxPrice,pageSize) => `${SERVER_PATHS.api}/${SERVER_PATHS.product}/GetProductsBySubCategory/${subCategoryId}?OrderChoice=${orderChoices}&PriceMin=${minPrice}&PriceMax=${maxPrice}&PageNumber=${pageNumber}&PageSize=${pageSize}`,
    getProduct:(productId)=>`${SERVER_PATHS.api}/${SERVER_PATHS.product}/${productId}`,
    postAccountRegister: ()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/customerregister`,
    postAccountLogin:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/login`,
    getAccountCurrent:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/getcurrent`,
    putAccountUpdateCurrent:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.account}/updatecurrent`,
    getVariant:(variantId)=>`${SERVER_PATHS.api}/${SERVER_PATHS.variant}/${variantId}`,
    getCityAll:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.city}/getall`,
    getCityId:(cityId)=>`${SERVER_PATHS.api}/${SERVER_PATHS.city}/${cityId}`,
    postOrderCreate:()=>`${SERVER_PATHS.api}/${SERVER_PATHS.order}/createorder`,
    getOrderCurrent:(pageNumber,pageSize)=>`${SERVER_PATHS.api}/${SERVER_PATHS.order}/GetOrderCurrent?PageNumber=${pageNumber}&PageSize=${pageSize}`
}