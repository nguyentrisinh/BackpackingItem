import React from 'react';
import {Tabs, Collapse, Spin} from 'antd';
import Profile from './Profile';
import {connect} from 'react-redux';
import {Loading, OrderInfo} from '../../components/components.layouts/index';
import {withCookies} from 'react-cookie';
import {getOrderCurrent} from "../../redux/redux.actions/appData";
import {getOrderCurrent as svrGetOrderCurrent} from "../../server/serverActions";
import moment from 'moment';
import {numberFormat} from "../../utils/utils";

const Panel = Collapse.Panel;


class ProfileContainer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
        const token = this.props.cookies.get('token');
        svrGetOrderCurrent(token, '', '100').then(res => {
            if (res.status == 200) {
                if (res.data.errors == null) {
                    this.props.getOrderCurrent(res.data.data)
                }
            }
        })
            .catch(err => {
                console.log(err)
            })
    }

    renderProfile = () => {
        const {userInfo} = this.props;
        if (userInfo == null) {
            return (
                <Loading/>
            )
        }
        return (
            <Profile userInfo={userInfo}/>
        )
    }
    renderDonHang = () => {
        if (this.props.currentOrder == null) {
            return (
                <Loading/>

            )
        }
        return this.props.currentOrder.content.map(item => {
            return (

                <Panel header={<div><span
                    className="text-success">#{item.id} -</span><span>{moment(item.datetime).format("DD-MM-YYYY")} - </span>
                    <span>
                    {numberFormat(item.totalPrice.toString(), ',')}
                </span>
                </div>} key={item.id}>
                    <OrderInfo data={item}/>
                </Panel>
            )
        })
    }

    render() {
        return (
            <div className="pt-5">
                <div className="container">
                    <Tabs defaultActiveKey="1" style={{paddingBottom: 200}} tabPosition="left">
                        <Tabs.TabPane tab="Thông tin cá nhân" key="1">
                            {
                                this.renderProfile()
                            }
                        </Tabs.TabPane>
                        <Tabs.TabPane tab="Danh sách đơn hàng" key="2">
                            <Collapse defaultActiveKey={['1']}>
                                {
                                    this.renderDonHang()
                                }
                            </Collapse>
                        </Tabs.TabPane>


                    </Tabs>
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        userInfo: state.app.userInfo,
        currentOrder: state.app.currentOrder
    }
}

export default connect(mapStateToProps, {getOrderCurrent})(withCookies(ProfileContainer))