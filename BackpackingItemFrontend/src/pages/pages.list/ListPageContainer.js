import React from 'react';
import {Anchor, Breadcrumb, Card, Col, Radio, Row, Slider} from 'antd';
import {Product,Path,SideBar} from '../../components/components.layouts/index';
import ListPage from './ListPage';
import {getListProducts} from "../../redux/redux.actions/listData";
import {connect} from 'react-redux';
import {ITEM_PER_PAGE} from "../../server/serverConfig";
import {ORDER_CHOICES} from "../../server/serverConfig";


class ListPageContainer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {};
    }

    renderListPage = () =>{
        return (
            <ListPage/>
        )
    }

    componentWillMount = () =>{
        const {params}=this.props.match;
        const {getListProducts} =this.props;
        getListProducts(params.categorySlug,0,ORDER_CHOICES.NoOrder,params.subCategorySlug)
    }

    render() {
        return (
            <div className="ListPage container">
                <Row gutter={10}>
                   <Path/>

                    <Col span={5}>
                       <SideBar/>
                    </Col>


                    <Col span={19}>
                        {
                            this.renderListPage()
                        }

                    </Col>

                </Row>


            </div>
        )
    }
}

export default connect(null,{getListProducts})(ListPageContainer)