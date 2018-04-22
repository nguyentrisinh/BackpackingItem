import React from 'react';
import {Row, Col, Layout, Breadcrumb, Card, Radio, Slider, Anchor} from 'antd';
import {Product} from '../../components/components.layouts/index'

const product = {
    basePrice: 2300000,
    description: "Mũ bảo hiểm cào cào Offroad LS2 FAST MX437 là dòng nón 3/4 được thiết kế đi trong đô thị, thành phố. Là dòng nón 3/4 của AGV có 2 kính, thích hợp đi cả ngày lẫn đêm. ↵ĐẶC TÍNH KỸ THUẬT ↵1/ Lớp shell vỏ nón AGV Fluid được tổng hợp theo công nghệ HIR-TH (nhựa tổng hợp). Lớp mút xốp EPS 3 mật độ được thiết kế theo 4 lớp lót khác nhau. ↵2/ Hệ thống thông gió IVS (Ventilation System) thông gió với lỗ thông hơi phía trước dẫn khí luồng xuyên qua đầu người lái. Lượng không khí lưu thông qua trung tâm nón được đặt ở vị trí tối ưu để người đội luôn cảm thấy thoải mái, dễ chịu, không bị nóng. Tất cả các lỗ thông hơi đều có thể với cần gạt đóng mở. ↵3/ AGV Fluid có nội thất bên trong nón AGV Fluid có thể tháo rời vệ sinh giặt dễ dàng. ↵4/ Nón AGV Fluid giúp giảm bớt gió và tiếng ồn khi chạy tốc độ cao. ↵5/ Tấm kính chắn gió bên ngoài và kính phụ chống nắng mặt trời tích hợp bên trong đều chống tia UV, có thể tháo lắp mà không cần sử dụng các dụng cụ. ↵Model Fluid với kiểu dáng gọn nhẹ, tích hợp kính chống nắng bên trong cực kì tiện lợi để anh chị em đi trong thành phố, đi gần cũng như đi dạo mát."
    , id: 5,
    imageUrl: "/StaticFiles/MyImages/mu-fullface-cao-cao-offroad-ls2-mx437-1-800x800.jpg",
    name: "Mũ bảo hiểm cào cào Offroad LS2 FAST MX437",
    returnInformation: "7 Ngày",
    subCategoryId: 3,
    supplierId: 6,
    warrantyInfomation: "12 Tháng"
}

export default class ListPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    renderProduct = () => {
        return new Array(20).fill(product).map(item => {
            return (
                <Col className="mb-2" span={6}>
                    <Product data={item}></Product>
                </Col>
            )
        });
    }

    render() {
        const radioStyle = {
            display: 'block',
            height: '30px',
            lineHeight: '30px',
        };
        return (
            <div className="ListPage container">
                <Row gutter={10}>
                    <Card bordered={false}>
                        <Breadcrumb>
                            <Breadcrumb.Item>Home</Breadcrumb.Item>
                            <Breadcrumb.Item><a href="">Application Center</a></Breadcrumb.Item>
                            <Breadcrumb.Item><a href="">Application List</a></Breadcrumb.Item>
                            <Breadcrumb.Item>An Application</Breadcrumb.Item>
                        </Breadcrumb>
                    </Card>

                        <Col span={5}>
                            <Anchor offsetTop={172}>
                            <Card title="Sắp xếp" className={'mb-2'}>
                                <Radio.Group>
                                    <Radio style={radioStyle} value={1}>Nổi bật</Radio>
                                    <Radio style={radioStyle} value={2}>Mới</Radio>
                                    <Radio style={radioStyle} value={3}>Bán chạy</Radio>
                                    <Radio style={radioStyle} value={4}>Khuyến mãi</Radio>
                                    <Radio style={radioStyle} value={5}>Giá từ thấp đến cao</Radio>
                                    <Radio style={radioStyle} value={6}>Giá từ cao đến thấp</Radio>
                                    <Radio style={radioStyle} value={7}>Tên từ A-Z</Radio>
                                    <Radio style={radioStyle} value={8}>Tên từ Z-A</Radio>
                                </Radio.Group>

                            </Card>
                            <Card title="Lọc theo giá">
                                <Slider range defaultValue={[20, 50]}/>

                            </Card>
                            </Anchor>
                        </Col>


                    <Col span={19}>
                        <Row gutter={10}>
                            {
                                this.renderProduct()
                            }
                        </Row>

                    </Col>

                </Row>


            </div>
        )
    }
}