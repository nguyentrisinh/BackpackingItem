import React from 'react';
import { Modal, Button, Tabs,Icon } from 'antd';
import {Login,Register } from './index';
import {connect} from 'react-redux';
import {clickModalUser} from "../../redux/redux.actions/appUI";

class ModalUser extends React.Component{
    constructor(props){
        super(props);
        this.state={
            activeKey:"1"
        }
    }
    handleOk = () =>{

    }

    handleCancel = () =>{
        this.props.clickModalUser(false);
    }

    changeTab = (activeKey) =>{
        this.setState({
            activeKey
        })
    }

    onChangeTab = (activeKey) =>{
        this.setState({
            activeKey
        })
    }

    render(){
        return (
            <Modal
                style={{overflow:'hidden',paddingBottom:'200px'}}
                // wrapClassName="ModalUser"
                // title="Basic Modal"
                visible={this.props.isOpenModalUser}
                onOk={this.handleOk}
                footer={null}
                onCancel={this.handleCancel}
            >
                <Tabs
                    onChange = {this.onChangeTab}
                    style={{overflow:'visible'}}
                    activeKey={this.state.activeKey}
                    // tabBarExtraContent={operations}
                >
                    <Tabs.TabPane tab="Đăng nhập" key="1">
                        <Login/>
                    </Tabs.TabPane>
                    <Tabs.TabPane tab="Đăng kí" key="2">
                        <Register changeTab={this.changeTab}/>
                    </Tabs.TabPane>
                </Tabs>
            </Modal>
        )
    }
}

const mapStateToProps = state =>    {
    return {
        isOpenModalUser:state.appUI.isOpenModalUser
    }
}

export default connect(mapStateToProps,{clickModalUser})(ModalUser)