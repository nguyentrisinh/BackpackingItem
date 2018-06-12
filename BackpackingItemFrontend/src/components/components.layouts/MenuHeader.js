import React from 'react';
import {getStaticImage, numberFormat} from '../../utils/utils';
import {Divider, Input, Icon, Dropdown, Menu} from 'antd';
import {Cart} from '../components.layouts/index';
import {Link} from 'react-router-dom';
import {clickModalUser} from "../../redux/redux.actions/appUI";
import {connect} from 'react-redux';
import {withCookies} from 'react-cookie';
import {getUserInfo} from "../../redux/redux.actions/appData";

class MenuHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    onClickLogin = () => {
        this.props.clickModalUser(true)
    }

    onClickDropdown = ({key}) => {
        if (key == 2) {
            const {cookies} = this.props;
            cookies.remove('token');
            this.props.getUserInfo(null)
        }
    }

    render() {
        return (
            <div className="MenuHeader">
                <div className="MenuHeader-wrap container">
                    <Link to={'/'} className="MenuHeader-logo">
                        <img src={getStaticImage("logo.png")} alt=""/>
                    </Link>
                    <div className="MenuHeader-search">
                        <Input.Search
                            className="MenuHeader-searchInput"
                            placeholder="input search text"
                            onSearch={value => console.log(value)}
                            enterButton
                        />
                        {/*<input type="text" className="MenuHeader-searchInput"/>*/}
                    </div>
                    <div className="MenuHeader-cart mr-3">
                        <Cart/>
                    </div>

                    <div className="MenuHeader-user">
                        {this.props.userInfo ?
                            <Dropdown trigger={['click']} overlay={<Menu onClick={this.onClickDropdown}>
                                <Menu.Item key={1}>
                                    <Link to="/profile">
                                        Vào trang cá nhân
                                    </Link>
                                    {/*<a target="_blank" rel="noopener noreferrer" href="http://www.alipay.com/">Vào</a>*/}
                                </Menu.Item>
                                <Menu.Item key={2}>
                                    <a> Đăng xuất</a>
                                </Menu.Item>
                            </Menu>}>
                                <a className="ant-dropdown-link" href="#">
                                    {(this.props.userInfo.firstName + this.props.userInfo.lastName) || this.props.userInfo.username}
                                    <Icon type="down"/>
                                </a>
                            </Dropdown> : <div onClick={this.onClickLogin} className="MenuHeader-link">
                                Đăng nhâp
                            </div>
                        }
                        {/*{*/}
                        {/*this.props.userInfo?<Link to="/profile">*/}
                        {/*{(this.props.userInfo.firstName + this.props.userInfo.lastName) || this.props.userInfo.username}*/}
                        {/*</Link>: <div onClick={this.onClickLogin} className="MenuHeader-link">*/}
                        {/*Đăng nhâp*/}
                        {/*</div>*/}
                        {/*}*/}
                    </div>

                </div>
                <div className="container">
                    <Divider className={'mt-0 mb-0'}/>
                </div>

            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        userInfo: state.app.userInfo
    }
}
export default connect(mapStateToProps, {clickModalUser, getUserInfo})(withCookies(MenuHeader))