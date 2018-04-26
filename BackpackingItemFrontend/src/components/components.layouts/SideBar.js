import React from 'react';
import {Card,Radio,Acnhor,Slider,Anchor} from'antd';

import {ORDER_CHOICES} from "../../server/serverConfig";
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import {numberFormat} from "../../utils/utils";

const radioStyle = {
    display: 'block',
    height: '30px',
    lineHeight: '30px',
};

class SideBar extends React.Component{
    constructor(props){
        super(props);
    }
    onChange = e =>{
        this.props.onChangeOrder(e.target.value)
    }

    onChangePrice = value =>{
        this.props.onChangePrice(value[0],value[1]);
    }
    render(){
        return (
            <Anchor offsetTop={172}>
                <Card title="Sắp xếp" className={'mb-2'}>
                    <Radio.Group value={this.props.listProducts.order} onChange={this.onChange} options={ORDER_CHOICES}/>

                </Card>
                <Card title="Lọc theo giá">
                    <Slider tipFormatter={(value)=>numberFormat(value.toString(),',')+' đ'} min={0} max={10000000} step={100000} range defaultValue={[0,10000000]} onChange={this.onChangePrice}/>

                </Card>
            </Anchor>
        )
    }
}

const mapStateToProps = state =>    {
    return {
        listProducts:state.listData.listProducts
    }
}

SideBar.propTypes = {
    onChangeOrder:PropTypes.func.isRequired,
    onChangePrice:PropTypes.func.isRequired
}

export default connect(mapStateToProps)(SideBar)