import React from 'react';
import {postAccountLogin} from "../../server/serverActions";

import { Modal, Button, Tabs,Icon,Input } from 'antd';
import {withCookies} from 'react-cookie';
const initialState={username: null,password:null,errors:null}

class Login extends React.Component{
    constructor(props){
        super(props);
        this.state=initialState;
    }
    onChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    onClickLogin = () =>{
        if (this.state.username==null && this.state.password==null){
            this.setState({
                errors:[{
                    errorCode:0,
                    errorMessage:"Vui lòng nhập email và mật khẩu"
                }]
            })
            return;
        }
        postAccountLogin(this.state.username,this.state.password).then(res=>{
            if (res.status==200){
                this.setState(initialState);
                const {cookies}= this.props;
                cookies.set('token',res.data.data);
            }
            else{
                this.setState({
                    errors:res.data.errors
                })
            }
        })
    }

    renderErrors = () => {
        if (this.state.errors) {
            return (
                <div className="mt-3">
                    {
                        this.state.errors.map(item => {
                            return (
                                <div className="text-center text-danger">
                                    {item.errorMessage}
                                </div>

                            )
                        })
                    }
                </div>
            )
        }

    }
    render(){
        const { username,password } = this.state;
        return (
            <div className="mt-3 text-center">
                <div>
                    <Input
                        name="username"
                        className="mb-2 w-50"
                        placeholder="Tên đăng nhập"
                        prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />}
                        // suffix={suffix}
                        value={username}
                        onChange={this.onChange}
                        ref={node => this.usernameInput = node}
                    />
                    <br/>
                    <Input
                        type="password"
                        name="password"
                        className="mb-2 w-50"
                        placeholder="Mật khẩu"
                        prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />}
                        // suffix={suffix}
                        value={password}
                        onChange={this.onChange}
                        ref={node => this.usernameInput = node}
                    />
                </div>
                {
                    this.renderErrors()
                }

                <Button onClick={this.onClickLogin} className="mt-3" type="primary">Đăng nhập</Button>

            </div>
        )
    }
}

export default  withCookies(Login);