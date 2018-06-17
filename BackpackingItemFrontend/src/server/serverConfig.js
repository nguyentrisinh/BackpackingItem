import axios from 'axios';

export const DOMAIN = "http://localhost:1502/";

export const MAP_CATEGORY = [
    {
        id: 1,
        link: 'mu-bao-hiem',
        name: "Mũ bảo hiểm",
        imageName: 'helmets.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 1,
                link: 'mu-bh-34',
                name: "Mũ bảo hiểm 3/4"
            },
            {
                id: 24,
                link: 'mu-bh-fullface',
                name: "Mũ bảo hiểm Fullface"
            },
            {
                id: 25,
                link: 'mu-bh-caocao',
                name: "Mũ bảo hiểm cào cào"
            },
            {
                id: 26,
                link: 'kinh-gan-mu-bh',
                name: "Kính gắn mũ bảo hiểm"
            }
        ]
    },
    {
        id: 2,
        link: 'quan-ao-phuot',
        name: "Quần áo phượt",
        imageName: 'clothes.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 27,
                link: 'ao-bao-ho',
                name: "Áo bảo hộ"
            },
            {
                id: 28,
                link: 'quan-bao-ho',
                name: "Quần bảo hộ"
            },
            {
                id: 29,
                link: 'quan-ao-chong-tham-nuoc',
                name: "Quần áo chống thấm nước"
            },
            {
                id: 30,
                link: 'quan-ao-nhanh-kho',
                name: "Quần áo nhanh khô"
            },
            {
                id: 31,
                link: 'quan-ao-khac',
                name: "Quần áo khác"
            }
        ]
    },
    {
        id: 3,
        link: 'phu-kien-phuot',
        name: 'Phụ kiện phượt',
        imageName: 'others.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 33,
                link: 'giap-tay-chan',
                name: "Giáp tay chân"
            },
            {
                id: 34,
                link: 'ong-tay-chong-nang',
                name: "Ống tay chống nắng"
            },
            {
                id: 35,
                link: 'khan',
                name: "Khăn"
            },
            {
                id: 36,
                link: 'day-ao-phan-quang',
                name: "Dây áo phản quang"
            },
            {
                id: 37,
                link: 'mu',
                name: "Mũ"
            },
            {
                id: 42,
                link: 'gang-tay',
                name: "Găng-tay"
            },
            {
                id: 38,
                link: 'phu-kien-khac',
                name: "Phụ kiện khác"
            }
        ],
    },
    {
        id: 4,
        link: 'cong-cu-phuot',
        name: 'Công cụ phượt',
        imageName: 'sneakers.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 2,
                link: 'dao-da-nang',
                name: "Dao đa năng"
            },
            {
                id: 3,
                link: 'moc',
                name: "Móc"
            },
            {
                id: 22,
                link: 'ong-nhom',
                name: "Ống nhòm"
            },
            {
                id: 23,
                link: 'thuy-xuong-kayak',
                name: "Thuyền - xuồng - kayak"
            },
            {
                id: 32,
                link: 'thung-binh',
                name: "Thùng - Bình"
            },
            {
                id: 39,
                link: 'leu',
                name: "Lều"
            },
            {
                id: 40,
                link: 'tui-ngu',
                name: "Túi ngủ"
            },
            {
                id: 41,
                link: 'do-sinh-ton-cuu-sinh',
                name: "Đồ sinh tồn - cứu sinh"
            },
            {
                id: 44,
                link: 'khac',
                name: "Công cụ khác"
            },
        ],
    },
    {
        id: 5,
        link: 'giay',
        name: "Giày",
        imageName: 'sneakers.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 4,
                link: 'giay-di-phuot',
                name: "Giày đi phượt"
            },
            {
                id: 5,
                link: 'giay-leo-nui',
                name: "Giày leo núi"
            },
            {
                id: 6,
                link: 'giay-loi-suoi',
                name: "Giày lội suối"
            },
            {
                id: 7,
                link: 'giay-di-xe-motor',
                name: "Giày đi xe - motor"
            },
            {
                id: 8,
                link: 'sandal-phuot',
                name: "Sandal phượt"
            },
            {
                id: 9,
                link: 'tat-vo-phu-kien-khac',
                name: "Tất - Phụ kiện khác"
            },
        ],
    },
    {
        id: 6,
        link: 'balo-tui',
        name: 'Balo - Túi',
        imageName: 'bags.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 11,
                link: 'balo-laptop',
                name: "Balo laptop"
            },
            {
                id: 20,
                link: 'balo-may-anh',
                name: "Balo máy ảnh"
            },
            {
                id: 12,
                link: 'balo-leo-nui',
                name: "Balo leo núi"
            },
            {
                id: 13,
                link: 'tui-gac-xe',
                name: "Túi gác xe"
            },
            {
                id: 14,
                link: 'vali-keo-hanh-ly',
                name: "Vali kéo - Hành lý"
            },
            {
                id: 15,
                link: 'balo-khac',
                name: "Balo-khác"
            },
            {
                id: 16,
                link: 'phu-kien-balo',
                name: "Phụ kiện balo"
            },

        ],
    },
    {
        id: 7,
        link: 'thiet-bi-cong-nghe',
        name: 'Thiết bị công nghệ',
        imageName: 'devices.png',
        children: [
            {
                id: '',
                link: '',
                name: "Tất cả"
            },
            {
                id: 17,
                link: 'camera-hanh-trinh',
                name: "Camera hành trình"
            },
            {
                id: 18,
                link: 'may-anh',
                name: "Máy ảnh"
            },
            {
                id: 19,
                link: 'den-pin',
                name: "Đèn pin"
            },
            {
                id: 21,
                link: 'pin-may-sac',
                name: "Pin - máy sạc"
            },
            {
                id: 43,
                link: 'thiet-bi-khac',
                name: "Thiết bị khác"
            },
        ],
    }

]

export const AXIOS = axios.create({
    baseURL: DOMAIN,
    // timeout: 1000,
    // headers: {'X-Custom-Header': 'foobar'}
});

export const ORDER_CHOICES =[{
    label:'Không sắp xếp',
    value:0,
    id:'NoOrder'
},
    {
        label:'Mới nhất',
        value:1,
        id:'IsNew'
    },
    {
        label:'Gía tăng dần',
        value:2,
        id:'PriceOrder'
    },
    {
        label:'Giá giảm dần',
        value:3,
        id:'PriceDescOrder'
    },
    {
        label:'Từ A-Z',
        value:4,
        id:'NameOrder'
    },
    {
        label:'Từ Z-A',
        value:5,
        id:'NameDescOrder'
    },


]

export const ITEM_PER_PAGE= 4;