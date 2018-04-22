import React from 'react';
import {getStaticImage} from '../../utils/utils';
import {Divider} from 'antd';
import {Input} from 'antd';

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
                        <Input.Search
                            className="MenuHeader-searchInput"
                            placeholder="input search text"
                            onSearch={value => console.log(value)}
                            enterButton
                        />
                        {/*<input type="text" className="MenuHeader-searchInput"/>*/}
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
                <div className="container">

                    <Divider className={'mt-0 mb-0'}/>
                </div>

            </div>
        )
    }
}