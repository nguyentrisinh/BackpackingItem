import React from 'react';
import {numberFormat} from "../../utils/utils";
import moment from "moment/moment";
import {Collapse, Card, Col, Row, List, Avatar, Tag} from 'antd';
import _ from 'lodash';
import update from 'react-addons-update';

import {getDistrictId, getOrderId, getVariant} from "../../server/serverActions";
import {Loading} from './index';
import {DOMAIN} from "../../server/serverConfig";
import {Link} from 'react-router-dom'


const Panel = Collapse.Panel;

export default class OrderInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            district: null,
            listOrder: props.data.orderDetails
        };
    }

    componentWillMount = () => {
        getDistrictId(this.props.data.districtId).then(res => {
            if (res.status == 200) {
                this.setState({
                    district: res.data.data
                });
                console.log(res.data.data)
            }
        })
        this.props.data.orderDetails.map(item => {
            getVariant(item.variantId).then(res => {
                if (res.status == 200) {
                    if (res.data.errors == null) {
                        const indexOrder = this.state.listOrder.findIndex(o => o.id == item.id);
                        if (indexOrder >= 0) {
                            this.setState(update(this.state, {
                                listOrder: {
                                    $splice: [[indexOrder, 1, update(this.state.listOrder[indexOrder], {
                                        $merge: {
                                            variant: res.data.data
                                        }
                                    })]]
                                }
                            }))
                        }
                    }
                }
            })


        })

    }

    renderOrderDetails = (item) => {
        if (item.variant) {
            return (
                <List.Item>
                    <List.Item.Meta
                        avatar={<Avatar src={DOMAIN + item.variant.images[0].imageUrl}/>}
                        title={
                            <div>
                                <Link to={`/${item.id}`}>{item.variant.product.name}</Link>
                                <span className="float-right">

                                <Tag>{item.variant.color.name}</Tag>
                                <Tag>{item.variant.size.name}</Tag>
                                </span>
                            </div>
                        }
                        description={<div>
                            <span>{numberFormat(item.pricePerUnit.toString(),',')}</span>
                            <span className="float-right">x {item.quantity}</span>

                        </div>}
                    />
                </List.Item>
            )
        }
        return (
            <Loading/>
        )


        // })
    }

    render() {
        const {data} = this.props;
        return (
            <div>
                <Card title="Thông tin người nhận">
                    <Row>
                        <Col span={4}>
                            Tên:
                        </Col>
                        <Col span={8}>
                            {data.receivePersonName}
                        </Col>
                    </Row>
                    <Row>
                        <Col span={4}>
                            Số điện thoại:
                        </Col>
                        <Col span={8}>
                            {data.phone}
                        </Col>
                    </Row>
                    <Row>
                        <Col span={4}>
                            Tỉnh/ Thành phố:
                        </Col>
                        <Col span={8}>
                            {_.has(this.state.district, "city.name") ? this.state.district.city.name : '...'}
                        </Col>
                    </Row>
                    <Row>
                        <Col span={4}>
                            Quận/ Huyện:
                        </Col>
                        <Col span={8}>
                            {_.has(this.state.district, "name") ? this.state.district.name : '...'}
                        </Col>
                    </Row>
                    <Row>
                        <Col span={4}>
                            Địa chỉ:
                        </Col>
                        <Col span={8}>
                            {data.address}
                        </Col>
                    </Row>
                </Card>
                <Card  actions={[<div>{
                    numberFormat(this.props.data.totalPrice.toString(),',')
                }</div>]} title="Thông tin đơn hàng">
                    <List
                        itemLayout="horizontal"
                        dataSource={this.state.listOrder}
                        renderItem={
                            this.renderOrderDetails
                        }
                    />
                    <div className="text-danger font-weight-bold">
                        * Thanh toán tiền mặt khi nhận hàng
                    </div>
                </Card>

            </div>
        )
    }
}