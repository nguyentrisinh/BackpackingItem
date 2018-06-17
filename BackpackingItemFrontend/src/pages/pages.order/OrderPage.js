import {Steps, Input, Select, Form, Button, Card} from 'antd';
import React from 'react';
import {CartDetail} from '../../components/components.layouts/index'
import classNames from 'classnames';
import {getCityAll, getCityId} from "../../server/serverActions";
import {removeFromCart} from "../../redux/redux.actions/appData";
import {postOrderCreate} from "../../server/serverActions";
import {connect} from 'react-redux';
import {withCookies} from 'react-cookie';

const Step = Steps.Step;
const Option = Select.Option;
const FormItem = Form.Item;
const formItemLayout = {
    labelCol: {
        xs: {span: 24},
        sm: {span: 8},
    },
    wrapperCol: {
        xs: {span: 24},
        sm: {span: 16},
    },
}

var a = {
    "address": "dsa",
    "datetime": "2018-06-12T13:52:39.774Z",
    "districtId": "2",
    "orderDetails": [{"quantity": "1", "pricePerUnit": "1800000", "totalPrice": "1800000", "variantId": "8"}],
    "phone": "0946854",
    "receivePersonName": "dsad",
    "status": "null",
    "totalPrice": "1800000",
    "voucherId": "null"
}

class OrderPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            step: 0,
            city: [],
            cityId: null,
            district: [],
            districtId: null,
            receivePersonName: null,
            phone: null,
            address: null,
            orderId:null

        };
        getCityAll().then(res => {
            if (res.status == 200) {
                if (res.data.errors == null) {
                    this.setState({
                        city: res.data.data
                    })
                }
            }
        })
    }

    clearProducts = () =>{
        const {cookies} = this.props;
        const myCookies = cookies.getAll();
        for (let key in myCookies) {
            if (key.includes("product_")) {
                this.props.removeFromCart(key.split("_")[1]);
                cookies.remove(key);
            }
        }

    }

    nextStep = () => {
        this.setState({
            step: this.state.step + 1
        })
    }
    handleSubmit = (e) => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
            if (!err) {
                this.nextStep();
                this.setState({
                    receivePersonName: values.receivePersonName,
                    phone: values.phone,
                    districtId: values.districtId,
                    address: values.address
                })

            }
            else {
                return
            }
        });

    }
    renderCity = () => {
        return this.state.city.map(item => {
            return (
                <Option value={item.id}>{item.name}</Option>
            )
        })
    }
    onChangeDistrict = value => {
        this.setState({
            districtId: value
        })
    }
    onChangeCity = value => {
        getCityId(value).then(res => {
            if (res.status == 200) {
                if (res.data.errors == null) {
                    this.setState({
                        district: res.data.data.districts
                    })
                }
            }
        });
        this.setState({
            cityId: value
        })
    }
    renderDistrict = () => {
        return this.state.district.map(item => {
            return (
                <Option value={item.id}>{item.name}</Option>
            )
        })
    }

    serializeOrder = (totalPrice, receivePersonName, phone, districtId, address, orderDetails) => {
        return {
            datetime: new Date(),
            totalPrice: parseInt(totalPrice),
            address,
            receivePersonName,
            phone,
            status: 1,
            voucherId: null,
            districtId: parseInt(districtId),
            orderDetails
        }
    }
    createOrderDetails = () => {
        if (this.props.cart) {
            return this.props.cart.map(item => {
                return {
                    "quantity": parseInt(item.quantity),
                    "pricePerUnit": parseInt(item.officialPrice),
                    "totalPrice": parseInt(item.quantity) * parseInt(item.officialPrice),
                    "variantId": parseInt(item.id)
                }
            })
        }

    }

    calcTotalPrice = () => {
        let totalPrice = 0;
        this.props.cart.map(item => {
            totalPrice += parseInt(item.officialPrice) * parseInt(item.quantity);
        })
        return totalPrice
    }

    onClickCashOut = () => {
        const token = this.props.cookies.get('token');
        postOrderCreate(token, this.serializeOrder(this.calcTotalPrice(), this.state.receivePersonName, this.state.phone, this.state.districtId, this.state.address, this.createOrderDetails()))
            .then(res => {
                if (res.status==200){
                    this.nextStep();
                    this.setState({
                        orderId:res.data.data.id
                    });
                    this.clearProducts();
                }
                else{
                    alert("Đặt hàng thất bại")
                }
            })
    }

    render() {
        const {getFieldDecorator} = this.props.form;
        return (

            <div className="pt-5 container">
                <Steps current={this.state.step}>
                    <Step title="Thông tin giỏ hàng"/>
                    <Step title="Địa chỉ giao hàng"/>
                    <Step title="Đặt hàng"/>
                    <Step title="Hoàn thành"/>
                </Steps>
                <div className={classNames("Step", {"isShow": this.state.step == 0})}>
                    <div style={{margin: 'auto'}} className="w-75 pt-5">
                        <div className="text-center font-weight-bold Step-title">
                            Thông tin giỏ hàng
                        </div>
                        <CartDetail onClickCash={this.nextStep}/>
                    </div>
                </div>
                <div className={classNames("Step", {"isShow": this.state.step == 1})}>
                    <div style={{margin: 'auto'}} className="w-75 pt-5">
                        <div className="text-center font-weight-bold Step-title">
                            Địa chỉ giao hàng
                        </div>
                        <div className="pt-3">
                            <Form onSubmit={this.handleSubmit}>
                                <FormItem
                                    {...formItemLayout}
                                    label="Tên người nhận"
                                >
                                    {getFieldDecorator('receivePersonName', {
                                        rules: [{
                                            required: true, message: 'Vui lòng nhập tên người nhận',
                                        }],
                                    })(
                                        <Input/>
                                    )}
                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Số điện thoại người nhận"
                                >
                                    {getFieldDecorator('phone', {
                                        rules: [{
                                            required: true, message: 'Vui lòng nhập SĐT người nhận',
                                        }],
                                    })(
                                        <Input/>
                                    )}
                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Tỉnh/Thành phố"
                                >
                                    {getFieldDecorator('cityId', {
                                        rules: [{
                                            required: true, message: 'Vui lòng chọn tỉnh/thành phố',
                                        }],
                                    })(
                                        <Select onChange={this.onChangeCity}>
                                            {
                                                this.renderCity()
                                            }
                                        </Select>
                                    )}
                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Quận/Huyện"
                                >
                                    {getFieldDecorator('districtId', {
                                        rules: [{
                                            required: true, message: 'Vui lòng chọn quận/huyện',
                                        }],
                                    })(
                                        <Select onChange={this.onChangeDistrict}>
                                            {
                                                this.renderDistrict()
                                            }
                                        </Select>
                                    )}
                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Địa chỉ"
                                >
                                    {getFieldDecorator('address', {
                                        rules: [{
                                            required: true, message: 'Vui lòng nhập số nhà, tên đường ...',
                                        }],
                                    })(
                                        <Input/>
                                    )}
                                </FormItem>
                                <FormItem
                                    wrapperCol={{
                                        xs: {span: 24, offset: 0},
                                        sm: {span: 16, offset: 8},
                                    }}
                                >
                                    <Button type="primary" htmlType="submit">
                                        Tiếp theo
                                    </Button>
                                </FormItem>

                            </Form>
                        </div>
                    </div>
                </div>
                <div className={classNames("Step", {"isShow": this.state.step == 2})}>
                    <div style={{margin: 'auto'}} className="w-75 pt-5">
                        <div className="text-center font-weight-bold Step-title">
                            Kiểm tra đơn hàng
                        </div>
                        <Card title="Thông tin đơn hàng" bordered={false}>
                            <CartDetail ref={(cart) => this.cart = cart} hiddenButton={true} disableAllInput={true}
                                        onClickCash={this.nextStep}/>
                        </Card>
                        <Card title="Thông tin thanh toán" bordered={false}>
                            <Form>
                                <FormItem
                                    {...formItemLayout}
                                    label="Tên người nhận"
                                >
                                    <Input disabled={true} value={this.state.receivePersonName}/>
                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Số điện thoại người nhận"
                                >

                                    <Input disabled={true} value={this.state.phone}/>

                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Tỉnh/Thành phố"
                                >

                                    <Select disabled={true} value={this.state.cityId}>
                                        {
                                            this.renderCity()
                                        }
                                    </Select>

                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Quận/Huyện"
                                >

                                    <Select disabled={true} value={this.state.districtId}>
                                        {
                                            this.renderDistrict()
                                        }
                                    </Select>

                                </FormItem>
                                <FormItem
                                    {...formItemLayout}
                                    label="Địa chỉ"
                                >

                                    <Input value={this.state.address} disabled={true}/>

                                </FormItem>
                                <div className="text-danger font-weight-bold">
                                    * Thanh toán tiền mặt khi nhận hàng
                                </div>
                                <div className="float-right">
                                    <Button onClick={this.onClickCashOut} className="bg-success" type="primary">
                                        THANH TOÁN
                                    </Button>
                                </div>


                            </Form>
                        </Card>

                    </div>
                </div>
                <div className={classNames("Step", {"isShow": this.state.step == 3})}>
                    <div style={{margin: 'auto'}} className="w-75 pt-5">
                        <div className="pb-2 text-center font-weight-bold Step-title">
                            Đặt hàng thành công
                        </div>
                        <div className="text-center">
                            <span>Mã đơn hàng của bạn là: </span>
                            <span className="text-success">#{this.state.orderId}</span>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        cart: state.app.cart
    }
}

const OrderPageWrapped = Form.create()(OrderPage);
export default connect(mapStateToProps,{removeFromCart})(withCookies(OrderPageWrapped));

