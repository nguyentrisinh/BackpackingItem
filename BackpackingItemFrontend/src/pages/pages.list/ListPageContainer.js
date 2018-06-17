import React from 'react';
import {Anchor, Breadcrumb, Card, Col, Radio, Row, Slider} from 'antd';
import {Product, Path, SideBar, Loading} from '../../components/components.layouts/index';
import ListPage from './ListPage';
import {getListProducts, resetListProducts} from "../../redux/redux.actions/listData";
import {connect} from 'react-redux';
import {ITEM_PER_PAGE} from "../../server/serverConfig";
import {ORDER_CHOICES} from "../../server/serverConfig";


class ListPageContainer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {};
    }

    renderListPage = () => {
        const {listProducts} = this.props;
        if (listProducts.isLoading) {
            return (
                <Loading/>
            )
        }
        if (!Array.isArray(listProducts.data)) {
            return (
                <div className="text-center">
                    Lỗi
                </div>
            )
        }
        if (listProducts.data.length == 0) {
            return (
                <div className="text-center">
                    Không có sản phẩm
                </div>
            )

        }
        return (
            <ListPage onClickLoadMoreButton={this.onClickLoadMoreButton} listProducts={this.props.listProducts}/>
        )
    }

    onClickLoadMoreButton = () => {
        const {listProducts} = this.props;
        const {params} = this.props.match;
        this.getData(false, params.categorySlug, listProducts.currentPage +1, listProducts.order, params.subCategorySlug, listProducts.minPrice, listProducts.maxPrice);
    }

    getData = (isReset, categorySlug, pageNumber, orderChoices, subCategorySlug, minPrice, maxPrice) => {
        const {getListProducts} = this.props;
        getListProducts(isReset, categorySlug, pageNumber, orderChoices, subCategorySlug, minPrice, maxPrice);
    }

    componentWillReceiveProps = nextProps => {
        if (this.props.match.params !== nextProps.match.params) {
            const {listProducts} = nextProps;
            const {params} = nextProps.match;
            this.getData(true, params.categorySlug, 1, listProducts.order, params.subCategorySlug, listProducts.minPrice, listProducts.maxPrice);
        }
    }

    onChangeOrder = (orderValue) =>{
        const {listProducts} = this.props;
        const {params} = this.props.match;
        this.getData(true, params.categorySlug, 1, orderValue, params.subCategorySlug, listProducts.minPrice, listProducts.maxPrice);
    }

    onChangePrice = (minPrice,maxPrice) =>{
        const {listProducts} = this.props;
        const {params} = this.props.match;
        this.getData(true, params.categorySlug, 1, listProducts.order, params.subCategorySlug, minPrice, maxPrice);
    }


    componentWillMount = () => {
        const {listProducts} = this.props;
        const {params} = this.props.match;
        this.getData(false, params.categorySlug, listProducts.currentPage, listProducts.order, params.subCategorySlug, listProducts.minPrice, listProducts.maxPrice);
    }

    componentWillUnmount = () =>{
        this.props.resetListProducts();
    }

    render() {
        return (
            <div className="ListPage container">
                <Row gutter={10}>
                    <Path/>

                    <Col span={5}>
                        <SideBar onChangePrice={this.onChangePrice} onChangeOrder={this.onChangeOrder}/>
                    </Col>


                    <Col className="text-center" span={19}>
                        {
                            this.renderListPage()
                        }

                    </Col>

                </Row>


            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        listProducts: state.listData.listProducts
    }
}

export default connect(mapStateToProps, {getListProducts, resetListProducts})(ListPageContainer)