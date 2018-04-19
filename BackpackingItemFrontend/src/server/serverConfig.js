import axios from 'axios';

export const DOMAIN = "http://localhost:1502/";

export const MAP_CATEGORY = [
    {
        id:1,
        link:'mu-bao-hiem',
        name:"Mũ bảo hiểm",
        imageName:'helmets.png',
        children:[
            {
                id:1,
                link:'mu-bh-34',
                name:"Mũ bảo hiểm 3/4"
            },
            {
                id:2,
                link:'mu-bh-fullface',
                name:"Mũ bảo hiểm Fullface"
            },
            {
                id:3,
                link:'mu-bh-caocao',
                name:"Mũ bảo hiểm cào cào"
            }
        ]
    },
    {
        id:2,
        link:'quan-ao-phuot',
        name:"Quần áo phượt",
        imageName:'clothes.png',
        children:[
            {
                id:4,
                link:'phu-kien',
                name:"Đồ bảo hộ và phụ kiện xe máy Dainese"
            },
            {
                id:5,
                link:'ao-giap',
                name:"Aó giáp"
            }
        ]
    },
    {
        id:3,
        link:'gang-tay',
        name:'Găng tay',
        imageName:'others.png',
        children:[],
    },
    {
        id:4,
        link:'giay',
        name:'Giày',
        imageName:'sneakers.png',
        children:[],
    },
    {
        id:5,
        link:'balo-tui',
        name:"Balo-Túi",
        imageName:'bags.png',
        children:[],
    },
    {
        id:6,
        link:'thiet-bi-cong-nghe',
        name:'Thiết bị công nghệ',
        imageName:'devices.png',
        children:[],
    }

]

export const AXIOS = axios.create({
    baseURL: DOMAIN,
    // timeout: 1000,
    // headers: {'X-Custom-Header': 'foobar'}
});