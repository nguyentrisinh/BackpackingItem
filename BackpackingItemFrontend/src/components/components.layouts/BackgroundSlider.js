import React from 'react';
import Swiper from 'react-id-swiper';
import {connect} from 'react-redux';
import {clickMenu} from "../../redux/redux.actions/appUI";
import {getStaticImage} from "../../utils/utils";

class BackgroundSlider extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    onClick = () =>{
        this.props.clickMenu(null);
    }

    render() {
        return (
            <div  onClick={this.onClick}>
                <Swiper
                    autoplay
                    loop

                    // slideClass="BackgroundSlider-slide "
                    containerClass="BackgroundSlider">
                    <div>

                        <img className="BackgroundSlider-img" src={getStaticImage("banner1.jpeg")} alt=""/>
                    </div>
                    <div>

                        <img className="BackgroundSlider-img"  src={getStaticImage("banner2.jpg")} alt=""/>
                    </div>
                    <div>

                        <img className="BackgroundSlider-img"  src={getStaticImage("banner3.jpg")} alt=""/>
                    </div>
                    <div>

                        <img className="BackgroundSlider-img"  src={getStaticImage("banner4.jpg")} alt=""/>
                    </div>
                    <div>

                        <img className="BackgroundSlider-img"  src={getStaticImage("banner5.jpg")} alt=""/>
                    </div>
                    {/*<div>Slide 1</div>*/}
                    {/*<div>Slide 2</div>*/}
                    {/*<div>Slide 3</div>*/}
                    {/*<div>Slide 4</div>*/}
                    {/*<div>Slide 5</div>*/}
                </Swiper>
            </div>


        )
    }
}

export default connect(null,{clickMenu})(BackgroundSlider)