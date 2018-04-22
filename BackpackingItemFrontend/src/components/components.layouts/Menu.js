import React from 'react';
import {MenuHeader,MainMenu,SubMenu} from './index';
import {Divider} from 'antd';

export default class Menu extends React.Component{
    constructor(props){
        super(props);
        this.state={};
    }
    render(){
        return (
            <div className="Menu">
                <MenuHeader/>
                <MainMenu/>
                <SubMenu/>
                <Divider className="mt-0 mb-0"/>
            </div>
        )
    }
}