import React from 'react';
import {IntroCategory, Label, ListProduct, SelfIntro,BackgroundSlider} from '../../components/components.layouts/index'
import {connect} from 'react-redux';
import {getLastestProducts, getSpecialProducts} from "../../redux/redux.actions/homeData";

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
        this.props.getLastestProducts(4);
        this.props.getSpecialProducts(4);
    }

    render() {
        return (
<div className="Home">
    <div className="Home-content">
        <div className=" container">
            <IntroCategory/>
            <Label title={"Sản phẩm mới"}/>
            <ListProduct data={this.props.lastestProducts}/>
            <SelfIntro/>
            <Label title={"Sản phẩm bán chạy"}/>
            <ListProduct data={this.props.specialProducts}/>
            {/*<Label title={"Sản phẩm đặc biệt"}/>*/}
            {/*<ListProduct/>*/}
            <BackgroundSlider/>
        </div>
    </div>
</div>

        )
    }
}

const mapStateToProps = state => {
    return {
        lastestProducts: state.homeData.lastestProducts,
        specialProducts: state.homeData.specialProducts
    }
}

export default connect(mapStateToProps, {getLastestProducts, getSpecialProducts})(Home)