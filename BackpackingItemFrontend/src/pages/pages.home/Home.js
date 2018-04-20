import React from 'react';
import {BackgroundSlider, IntroCategory, Label,ListProduct,SelfIntro} from '../../components/components.layouts/index'
import {getStaticImage} from "../../utils/utils";
import {connect} from 'react-redux';
import {getLastestProducts,getSpecialProducts} from "../../redux/redux.actions/homeData";

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
        this.props.getLastestProducts(4);
        this.props.getSpecialProducts(4);
    }

    render() {
        return (
           <div>
               <div className="Home">
                   <img className="Home-coverImg" src={getStaticImage("Artboard.png")} alt=""/>
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

                       </div>

                   </div>

               </div>
               <BackgroundSlider/>
           </div>

        )
    }
}

const mapStateToProps = state =>    {
    return {
        lastestProducts:state.homeData.lastestProducts,
        specialProducts:state.homeData.specialProducts
    }
}

export default connect(mapStateToProps,{getLastestProducts,getSpecialProducts})(Home)