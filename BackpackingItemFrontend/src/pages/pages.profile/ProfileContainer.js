import React from 'react';
import {Tabs} from 'antd';
import Profile from './Profile';
import {connect} from 'react-redux';
import {Loading} from '../../components/components.layouts/index'


class ProfileContainer extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    renderProfile = () => {
        const {userInfo} = this.props;
        if (userInfo==null) {
            return (
                <Loading/>
            )
        }
        return (
            <Profile userInfo={userInfo}/>
        )
    }
    render(){
        return (
            <div className="pt-5">
                <div className="container">
                    <Tabs style={{paddingBottom:200}} tabPosition="left">
                        <Tabs.TabPane tab="Thông tin cá nhân" key="1">
                            {
                                this.renderProfile()
                            }
                        </Tabs.TabPane>
                        <Tabs.TabPane tab="Danh sách đơn hàng" key="2">Danh sách đơn hàng</Tabs.TabPane>
                        {/*<Tabs.TabPane tab="Tab 3" key="3">Content of Tab 3</Tabs.TabPane>*/}
                    </Tabs>
                </div>
            </div>
        )
    }
}

const mapStateToProps = state =>    {
    return {
        userInfo:state.app.userInfo
    }
}

export default connect(mapStateToProps)(ProfileContainer)