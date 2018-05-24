import React from 'react';
import {Radio, Icon, Input, Button} from 'antd';
import DatePicker from 'react-datepicker';
import moment from 'moment';
import {putAccountUpdateCurrent} from "../../server/serverActions";
import {withCookies} from 'react-cookie';
import {getUserInfo} from "../../redux/redux.actions/appData";
import {connect} from 'react-redux';

import 'react-datepicker/dist/react-datepicker.css';

class Profile extends React.Component {
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
    constructor(props) {
        super(props);
        const {userInfo} = props;
        this.state = {
            email: userInfo.email,
            password: '********',
            firstName: userInfo.firstName,
            lastName: userInfo.lastName,
            gender: userInfo.gender,
            birthday: moment(userInfo.birthday),
            errors: null
        };
        var date = moment('2018/05/24');
        console.log(date)
    }

    handleChange = (date) => {
        this.setState({
            birthday: date
        });
    }
    onChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        })
    }
    onChangeGender = (e) => {
        this.setState({
            gender: e.target.value,
        });
    }
    onClickSave = () => {
        const {cookies}= this.props;
        if (cookies.get('token')){
            putAccountUpdateCurrent(cookies.get('token'),this.state.email, this.state.password, this.state.firstName, this.state.lastName, this.state.gender, this.state.birthday.format("YYYY-MM-DD"))
                .then(res=>{
                    if (res.status==200){
                        this.props.getUserInfo(res.data.data);
                    }
                    else{
                        this.setState({
                            errors: res.data.errors,
                        })
                    }
                });
        }

    }

    componentWillReceiveProps = nextProps =>{
        const {userInfo}= nextProps;
        this.setState({
            email: userInfo.email,
            password: '********',
            firstName: userInfo.firstName,
            lastName: userInfo.lastName,
            gender: userInfo.gender,
            birthday: moment(userInfo.birthday),
            errors: null
        })
    }

    render() {
        return (
            <div className="text-center mt-3 mb-3">
                <Input
                    name="email"
                    className="mb-2 w-75"
                    placeholder="Email"
                    prefix={<Icon type="mail" style={{color: 'rgba(0,0,0,.25)'}}/>}
                    // suffix={suffix}
                    value={this.state.email}
                    onChange={this.onChange}
                    ref={node => this.userNameInput = node}
                />
                <br/>
                <Input
                    disabled={true}
                    name="password"
                    className="mb-2 w-75"
                    type="password"
                    placeholder="Mật khẩu"
                    prefix={<Icon type="lock" style={{color: 'rgba(0,0,0,.25)'}}/>}
                    // suffix={suffix}
                    value={this.state.password}
                    onChange={this.onChange}
                />

                <br/>
                <Input
                    name="firstName"
                    className="mb-2 w-75"
                    placeholder="Họ và tên lót"
                    prefix={<Icon type="contacts" style={{color: 'rgba(0,0,0,.25)'}}/>}
                    // suffix={suffix}
                    value={this.state.firstName}
                    onChange={this.onChange}
                />
                <br/>
                <Input
                    name="lastName"
                    className="mb-2 w-75"
                    placeholder="Tên"
                    prefix={<Icon type="contacts" style={{color: 'rgba(0,0,0,.25)'}}/>}
                    // suffix={suffix}
                    value={this.state.lastName}
                    onChange={this.onChange}
                />
                <br/>
                <Radio.Group className="w-75 text-left mb-2" onChange={this.onChangeGender} value={this.state.gender}>
                    <Radio value={1}>Nam</Radio>
                    <Radio value={2}>Nữ</Radio>
                </Radio.Group>
                <DatePicker
                    showMonthDropdown
                    showYearDropdown
                    customInput={<Input
                        name="birthday"
                        className="mb-2 w-75"
                        placeholder="Ngày sinh"
                        prefix={<Icon type="calendar" style={{color: 'rgba(0,0,0,.25)'}}/>}
                        // suffix={suffix}
                        // value={this.state.birthday}
                        // onChange={this.onChange}
                    />}
                    dateFormat="DD/MM/YYYY"
                    selected={this.state.birthday}
                    onChange={this.handleChange}
                />
                {
                    this.renderErrors()
                }
                <Button onClick={this.onClickSave} className="mt-3" type="primary">Lưu</Button>


            </div>
        )
    }
}

export default connect(null,{getUserInfo})(withCookies(Profile))