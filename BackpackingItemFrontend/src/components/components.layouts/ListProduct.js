import React from 'react';
import {Product} from '../../components/components.layouts/index'
export default class ListProduct extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }

    renderProduct = () =>{
       return this.props.data.data.map(item=>{
            return (
                <div className="ListProduct-item">
                    <Product data={item}/>
                </div>
            )
        })
    }
    render(){
        if (this.props.data.isLoading){
            return (
                <div className="ListProduct">
                    Đang tải
                </div>
            )
        }
        if (Array.isArray(this.props.data.data)){
            if (this.props.data.data.length>0){
                return (
                    <div className="ListProduct">
                        {
                            this.renderProduct()
                        }
                    </div>
                )
            }
        }
        return (
            <div className="ListProduct">
                Lỗi
            </div>
        )

    }
}