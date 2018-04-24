import React from 'react';
import {Breadcrumb, Row, Card, Col, Select, InputNumber,Button,Icon,Radio} from 'antd';
import Swiper from 'react-id-swiper';
import {numberFormat} from "../../utils/utils";

export default class DetailPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className="DetailPage container">

                <Card bordered={false}>
                    <Breadcrumb>
                        <Breadcrumb.Item>Home</Breadcrumb.Item>
                        <Breadcrumb.Item><a href="">Application Center</a></Breadcrumb.Item>
                        <Breadcrumb.Item><a href="">Application List</a></Breadcrumb.Item>
                        <Breadcrumb.Item>An Application</Breadcrumb.Item>
                    </Breadcrumb>
                </Card>
                <Row>
                    <Col span={14}>
                        <div className="font-weight-bold DetailPage-productName mb-3">
                            NÓN BẢO HIỂM INDEX DÀNH CHO CÀO CÀO
                        </div>
                        <div className="DetailPage-productDescription mb-4">
                            - Hàng Thái Lan xách tay, số lượng có hạn
                            <br/>
                            - Kiểu nón Fullface bịt cằm chuyên dùng cho các Biker đi xe địa hình
                            <br/>
                            - Loại nón cào cào không kính, có mái che tăng-giảm được
                            <br/>
                            - Kiểu dáng cực ngầu
                            <br/>
                            - Bảo hành: 12 tháng
                            <br/>
                            - Đổi trả trong vòng 30 ngày
                            <br/>
                        </div>
                        <div className="DetailPage-actions">
                            <div className="DetailPage-actionItem mb-2">
                                <div className="DetailPage-actionLabel">
                                    Size:
                                </div>
                                <div className="DetailPage-actionSelect">
                                    {/*<Select defaultValue="M" style={{width: 120}}>*/}
                                        {/*<Select.Option value="S">S</Select.Option>*/}
                                        {/*<Select.Option value="M">M</Select.Option>*/}
                                        {/*<Select.Option value="L">L</Select.Option>*/}
                                        {/*<Select.Option value="XL">XL</Select.Option>*/}
                                    {/*</Select>*/}
                                    <Radio.Group onChange={this.handleSizeChange}>
                                        <Radio.Button value="S">S</Radio.Button>
                                        <Radio.Button value="M">M</Radio.Button>
                                        <Radio.Button value="L">L</Radio.Button>
                                    </Radio.Group>
                                </div>
                            </div>
                            <div className="DetailPage-actionItem mb-2">
                                <div className="DetailPage-actionLabel">
                                    Màu:
                                </div>
                                <div className="DetailPage-actionSelect">
                                    {/*<Select defaultValue="blue" style={{width: 120}}>*/}
                                        {/*<Select.Option value="blue">Xanh lam</Select.Option>*/}
                                        {/*<Select.Option value="red">Đỏ</Select.Option>*/}
                                        {/*<Select.Option value="green">Xanh lá</Select.Option>*/}
                                        {/*<Select.Option value="oảnge">Cam</Select.Option>*/}
                                    {/*</Select>*/}
                                    <Radio.Group onChange={this.handleSizeChange}>
                                        <Radio.Button value="red">Đỏ</Radio.Button>
                                        <Radio.Button value="orange">Cam</Radio.Button>
                                        <Radio.Button value="yellow">Vàng</Radio.Button>
                                        <Radio.Button value="green">Lục</Radio.Button>
                                        <Radio.Button value="blue">Lam</Radio.Button>
                                        <Radio.Button value="brown">Chàm</Radio.Button>
                                        <Radio.Button value="purple">Tím</Radio.Button>
                                    </Radio.Group>
                                </div>
                            </div>
                            <div className="DetailPage-actionItem mb-2">
                                <div className="DetailPage-actionLabel">
                                    Số lượng:
                                </div>
                                <div className="DetailPage-actionSelect">

                                    <InputNumber min={1} max={10} defaultValue={1}/>
                                </div>
                            </div>
                        </div>
                        <div className="mt-4 mb-3 font-weight-bold DetailPage-price">

                            Gía: {numberFormat('3500000',',')} VND
                        </div>
                        <div className="DetailPage-add">
                            <Button className="DetailPage-addButton" type="omitted" size='large'>
                                <Icon type="shopping-cart" />
                                Thêm vào giỏ
                            </Button>
                        </div>
                    </Col>
                    <Col span={10}>
                        <Card cover={<img src="http://placehold.it/800x800" alt=""/>} bordered={true} hoverable>

                            <Swiper pagination= {{
                                el: '.swiper-pagination',
                                type: 'bullets',
                                clickable: true
                            }}
                                    navigation={{
                                        nextEl: '.swiper-button-next',
                                        prevEl: '.swiper-button-prev'
                                    }}
                                    spaceBetween={10} slidesPerView={3}>
                                <div>
                                    <img src="http://placehold.it/200x200" alt=""/>
                                </div>
                                <div>
                                    <img src="http://placehold.it/200x200" alt=""/>
                                </div>
                                <div>
                                    <img src="http://placehold.it/200x200" alt=""/>
                                </div>
                                <div>
                                    <img src="http://placehold.it/200x200" alt=""/>
                                </div>
                                <div>
                                    <img src="http://placehold.it/200x200" alt=""/>
                                </div>

                            </Swiper>
                        </Card>

                    </Col>
                </Row>
            </div>
        )
    }
}