import React from 'react';
import {Popover, Card, List, Button, InputNumber, Icon, Avatar, Tag} from 'antd'
import {numberFormat} from "../../utils/utils";
import {withCookies} from 'react-cookie';
import {withRouter} from 'react-router'
import {getVariant} from "../../server/serverActions";
import {addToCart, removeFromCart} from "../../redux/redux.actions/appData";
import {CartDetail} from '../../components/components.layouts/index'
import {DOMAIN} from "../../server/serverConfig";
import {connect} from 'react-redux';

class Cart extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    onClickCash = ()=>{
        this.props.history.push('/order');
    }

    componentWillMount = () => {
        const {cookies} = this.props;
        const myCookies = cookies.getAll()
        for (let key in myCookies) {
            if (key.includes("product_")) {
                getVariant(key.split("_")[1]).then(res => {
                    if (res.status == 200) {
                        if (res.data.errors == null) {
                            const {data} = res.data;
                            this.props.addToCart(data, myCookies[key])
                        }
                    }
                })
                    .catch(err => console.log(err))
            }
        }
    }

    render() {
        return (
            <Popover placement="bottom" trigger="click" title="Giỏ hàng" content={
                <CartDetail onClickCash={this.onClickCash}/>
            }>
                <Button type="primary" size="large" shape="circle" icon="shopping-cart"/>
            </Popover>
        )
    }
}

const mapStateToProps = state => {
    return {
        cart: state.app.cart
    }
}

export default withRouter(connect(mapStateToProps, {addToCart, removeFromCart})(withCookies(Cart)))