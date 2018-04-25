import React from 'react';
import {Card,Breadcrumb} from 'antd';


export default class Path extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <Card bordered={false}>
                <Breadcrumb>
                    <Breadcrumb.Item>Home</Breadcrumb.Item>
                    <Breadcrumb.Item><a href="">Application Center</a></Breadcrumb.Item>
                    <Breadcrumb.Item><a href="">Application List</a></Breadcrumb.Item>
                    <Breadcrumb.Item>An Application</Breadcrumb.Item>
                </Breadcrumb>
            </Card>
        )
    }
}