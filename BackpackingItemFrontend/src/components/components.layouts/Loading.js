import React from 'react';
import {Spin} from 'antd';

export default class Loading extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div style={{minHeight:'58vh',display:'flex',justifyContent:'center'}} className="Loading pt-5">
                <Spin size="large" />
            </div>

        )
    }
}