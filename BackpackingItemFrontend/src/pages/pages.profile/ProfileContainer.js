import React from 'react';
import {Tabs} from 'antd';
import Profile from './Profile';


export default class ProfileContainer extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div className="pt-5">
                <div className="container">
                    <Tabs tabPosition="left">
                        <Tabs.TabPane tab="Thông tin cá nhân" key="1">
                            <Profile/>
                        </Tabs.TabPane>
                        <Tabs.TabPane tab="Danh sách đơn hàng" key="2">Danh sách đơn hàng</Tabs.TabPane>
                        {/*<Tabs.TabPane tab="Tab 3" key="3">Content of Tab 3</Tabs.TabPane>*/}
                    </Tabs>
                </div>
            </div>
        )
    }
}