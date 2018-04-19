import React from 'react';
import {getStaticImage} from "../../utils/utils";

export default class SeftIntro extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className="SelfIntro">
                <div className="SelfIntro-item">
                    <div className="SelfIntro-icon">
                        <img src={getStaticImage("plane.png")} alt=""/>
                    </div>
                    <div className="SelfIntro-info">
                        <div className="SelfIntro-title">
                            GIAO HÀNG TẬN NƠI
                        </div>
                        <div className="SelfIntro-description">
                            Nhanh chóng, an toàn.
                        </div>
                    </div>
                </div>
                <div className="SelfIntro-item">
                    <div className="SelfIntro-icon">
                        <img src={getStaticImage("clock.png")} alt=""/>
                    </div>
                    <div className="SelfIntro-info">
                        <div className="SelfIntro-title">
                            ĐỔI TRẢ DỄ DÀNG
                        </div>
                        <div className="SelfIntro-description">
                            Trong vòng 3 ngày
                        </div>
                    </div>
                </div>
                <div className="SelfIntro-item">
                <div className="SelfIntro-icon">
                <img src={getStaticImage("phone.png")} alt=""/>
                </div>
                <div className="SelfIntro-info">
                <div className="SelfIntro-title">
                TƯ VẤN TẬN TÌNH
                </div>
                <div className="SelfIntro-description">
               Khách hàng là thượng đế
                </div>
                </div>
                </div>

            </div>
        )
    }
}