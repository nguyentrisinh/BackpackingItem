import React from 'react';
import {connect} from 'react-redux';
import {Loading} from '../../components/components.layouts/index'
import {getProduct} from "../../redux/redux.actions/detailData";
import DetailPage from './DetailPage';

 class DetailPageContainer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentWillMount = () =>{
        const {params}= this.props.match;
        const {getProduct} = this.props;
        getProduct(params.productId);
    }
    render() {
        const {product} = this.props;
       if (product.isLoading){
           return (
               <Loading/>
           )
       }
       if (typeof product.data !== 'object'){
           return(
               <div className="text-center">
                   Lỗi
               </div>
           )
       }
       return (
           <DetailPage productData = {product.data}/>
       )
    }
}

const mapStateToProps = state =>    {
    return {
        product:state.detailData.product
    }
}


export default connect (mapStateToProps,{getProduct})(DetailPageContainer)