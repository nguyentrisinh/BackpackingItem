import React from 'react';
import {numberFormat} from "../../utils/utils";
import {withCookies} from 'react-cookie';
import {connect} from 'react-redux';
import PropTypes from 'prop-types';

import {Popover, Card, List, Button, InputNumber, Icon, Avatar, Tag} from 'antd'
import {DOMAIN} from "../../server/serverConfig";
import {getVariant} from "../../server/serverActions";
import {addToCart, removeFromCart} from "../../redux/redux.actions/appData";


class CartDetail extends React.Component {
    calcTotalPrice = () => {
        let totalPrice = 0;
        this.props.cart.map(item => {
            totalPrice += parseInt(item.officialPrice) * parseInt(item.quantity);
        })
        return totalPrice
    }

    constructor(props) {
        super(props);
        this.state = {};
    }

    onClickDelProduct = (item) => {
        const {cookies} = this.props;
        this.props.removeFromCart(item.id);
        cookies.remove(`product_${item.id}`, {
            path: '/'
        });
    }

    renderActions = () => {
        if (!this.props.hiddenButton) {
            return [<div>{numberFormat(
                this.calcTotalPrice().toString(), ',') + ' VND'}</div>,
                <div onClick={this.props.onClickCash} className='color-green'>
                    <Icon className='mr-2' type="shopping-cart"/>
                    {this.props.buttonText.toUpperCase()}
                </div>]


        }
        else {
            return [<div>{numberFormat(
                this.calcTotalPrice().toString(), ',') + ' VND'}</div>]
        }
    }

    getAllProducts = () =>{
        return this.props.cart
    }

    render() {
        return (
            <Card bodyStyle={{padding: 0}} bordered={false}
                  actions={this.renderActions()
                  }>
                <List style={{width: 'auto'}}
                      className="p-0"
                    // className="demo-loadmore-list"
                    // loading={loading}
                      itemLayout="horizontal"
                    // loadMore={loadMore}
                      dataSource={this.props.cart}
                      renderItem={item => (
                          <List.Item actions={[

                              <Button disabled={this.props.disableAllInput}
                                      onClick={this.onClickDelProduct.bind(this, item)} type="danger" shape="circle"
                                      icon="delete"/>
                          ]}>
                              <List.Item.Meta
                                  className='mr-3'
                                  avatar={<Avatar
                                      src={`${DOMAIN}/${item.images[0].imageUrl}`}/>}
                                  title={<a href="https://ant.design">Áo giáp </a>}
                                  description={numberFormat(item.officialPrice.toString(), ',') + ' VND'}
                              />
                              <Tag>{item.color.name}</Tag>
                              <Tag>{item.size.name}</Tag>
                              <InputNumber disabled={this.props.disableAllInput} style={{width: '60px'}} min={1}
                                           value={item.quantity}></InputNumber>

                          </List.Item>
                      )}
                />
            </Card>
        )
    }
}

CartDetail.propTypes = {
    disableAllInput: PropTypes.bool,
    onClickCash: PropTypes.func.isRequired,
    buttonText: PropTypes.string,
    hiddenButton: PropTypes.bool
}

CartDetail.defaultProps = {
    disableAllInput: false,
    buttonText: "Tiếp theo",
    hiddenButton: false
}

const mapStateToProps = state => {
    return {
        cart: state.app.cart
    }
}

export default connect(mapStateToProps, {addToCart, removeFromCart},null,{withRef:true})(withCookies(CartDetail))