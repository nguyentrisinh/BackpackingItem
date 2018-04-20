import React from 'react';
import {getStaticImage} from '../../utils/utils';

export default class MenuHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className="MenuHeader">
                <div className="MenuHeader-wrap container">
                    <div className="MenuHeader-logo">
                        <img src={getStaticImage("logo.png")} alt=""/>
                    </div>
                    <div className="MenuHeader-search">
                        <input type="text" className="MenuHeader-searchInput"/>
                    </div>

                    <div className="MenuHeader-user">
                        <a href={"/"} className="MenuHeader-link">
                            Đăng nhâp
                        </a>
                        <div>&nbsp;/&nbsp;</div>
                        <a href={"/"} className="MenuHeader-link">
                            Đăng kí
                        </a>

                    </div>
                </div>

            </div>
        )
    }
}