import React from 'react';
import {DOMAIN} from "../../server/serverConfig";

export default class Product extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        const {data}= this.props;
        return (
            <div className="Product">
                <div className="Product-wrapImage">
                    <img className="Product-img" src={`${DOMAIN+data.imageUrl}`} alt=""/>
                </div>
                <div className="Product-price">
                    VND {data.basePrice}
                </div>
                <div className="Product-name">
                    {data.name}
                </div>
                <div className="Product-add">
                    Thêm vào giỏ
                </div>
            </div>
        )
    }
}