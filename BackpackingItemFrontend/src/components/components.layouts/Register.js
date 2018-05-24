import React from 'react';
import {Modal, Button, Tabs, Icon, Input, Radio} from 'antd';
import 'react-datepicker/dist/react-datepicker.css';
import DatePicker from 'react-datepicker';
import moment from 'moment';
import {postAccountRegister} from "../../server/serverActions";
const initialState={
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    gender: 1,
    birthday: null,
    errors: null

};

export default class Register extends React.Component {
    constructor(props) {
        super(props);
        this.state =initialState;
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
    handleChange = (date) => {
        console.log(date)
        this.setState({
            birthday: date
        });
    }
    onClickRegister = () => {
        if (this.state.birthday == null) {
            this.setState({
                errors: [
                    {
                        errorCode: 0,
                        "errorMessage": "Vui lòng chọn ngày sinh",

                    }
                ]
            })
            return;
        }
        postAccountRegister(this.state.email, this.state.password, this.state.firstName,
            this.state.lastName, this.state.gender, this.state.birthday.format("YYYY-MM-DD")).then(res => {
            if (res.status == 200) {
                // Dang ki thanh cong
                this.props.changeTab("1");
                this.setState(initialState);
            }
            else {
                this.setState({
                    errors: res.data.errors,
                })
            }
        });
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
                <Button onClick={this.onClickRegister} className="mt-3" type="primary">Đăng kí</Button>


            </div>
        )
    }
}