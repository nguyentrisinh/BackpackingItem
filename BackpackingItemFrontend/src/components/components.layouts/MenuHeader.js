import React from 'react';
import {getStaticImage, numberFormat} from '../../utils/utils';
import {Divider, Input,Popover,Button,List,Avatar,Icon,Select,InputNumber,Card} from 'antd';
import {Link} from 'react-router-dom';
import {clickModalUser} from "../../redux/redux.actions/appUI";
import {connect} from 'react-redux';

class MenuHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    onClickLogin = () =>{
        this.props.clickModalUser(true)
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
                        <Popover placement="bottom"  trigger="click" title="Giỏ hàng" content={
                            <Card bodyStyle={{padding:0}} bordered={false} actions={[<div>{numberFormat('3500000',',') + ' VND'}</div>,<div className='color-green'>
                                <Icon className='mr-2' type="shopping-cart" />
                               ĐẶT HÀNG
                            </div>]}>
                                <List style={{width:'auto'}}
                                      className="p-0"
                                    // className="demo-loadmore-list"
                                    // loading={loading}
                                      itemLayout="horizontal"
                                    // loadMore={loadMore}
                                      dataSource={new Array(5).fill(0)}
                                      renderItem={item => (
                                          <List.Item actions={[<Button shape='circle' type='danger'>
                                              <Icon type="delete"></Icon>
                                          </Button>]}>
                                              <List.Item.Meta
                                                  className='mr-3'
                                                  avatar={<Avatar src="http://localhost:1502//StaticFiles/MyImages/agv-fluid-garda-white-italia-helmet-2-800x800.jpg" />}
                                                  title={<a href="https://ant.design">Áo giáp </a>}
                                                  description={numberFormat('3500000',',') + ' VND'}
                                              />
                                              <Select defaultValue="M">
                                                  <Select.Option value="S">S</Select.Option>
                                                  <Select.Option value="M">M</Select.Option>
                                                  <Select.Option value="L">L</Select.Option>
                                              </Select>
                                              <Select defaultValue="orange">
                                                  <Select.Option value="orange">Cam</Select.Option>
                                                  <Select.Option value="red">Đỏ</Select.Option>
                                                  <Select.Option value="yellow">Vàng</Select.Option>
                                              </Select>

                                              <InputNumber style={{width:'60px'}} min={1} defaultValue={2}></InputNumber>

                                          </List.Item>
                                      )}
                                />
                            </Card>
                            }>
                            <Button type="primary" size="large" shape="circle" icon="shopping-cart"/>
                        </Popover>
                        {/*<Popover placement="bottomLeft" content={"hihi"} title="Title">*/}
                            {/*<Button type="primary">Hover me</Button>*/}
                        {/*</Popover>*/}
                    </div>

                    <div className="MenuHeader-user">
                        {
                            this.props.userInfo?<Link to="/profile">
                                {this.props.userInfo.firstName}
                            </Link>: <div onClick={this.onClickLogin} className="MenuHeader-link">
                                Đăng nhâp
                            </div>
                        }
                    </div>

                </div>
                <div className="container">

                    <Divider className={'mt-0 mb-0'}/>
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
export default connect(mapStateToProps,{clickModalUser})(MenuHeader)