import React from 'react';
import {getStaticImage} from "../../utils/utils";

export default class  extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div className="IntroCategory">
                <a href="#" className="IntroCategory-wrap">
                <div className="IntroCategory-item">

                    <img src={getStaticImage("ads1.png")} alt=""/>
                </div>
                </a>
                <a href="#" className="IntroCategory-wrap">
                <div className="IntroCategory-item">
                    <img src={getStaticImage("ads2.jpg")} alt=""/>
                </div>
                </a>
                <a href="#" className="IntroCategory-wrap">
                <div className="IntroCategory-item">
                    <img src={getStaticImage("ads3.jpg")} alt=""/>
                </div>
                </a>
            </div>
            
        )
    }
}